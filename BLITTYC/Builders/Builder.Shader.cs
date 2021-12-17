using BLITTY;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace BLITTYC;

public struct ShaderCompileResult
{
    public readonly byte[] VsBytes;
    public readonly byte[] FsBytes;
    public readonly string[] Samplers;
    public readonly string[] Params;

    public ShaderCompileResult(byte[] vsBytes, byte[] fsBytes, string[] samplers, string[] @params)
    {
        this.VsBytes = vsBytes;
        this.FsBytes = fsBytes;
        this.Samplers = samplers;
        this.Params = @params;
    }
}

public static partial class Builder
{
    static class ShaderCompiler
    {
        private const string CompilerPath = "Shader\\shaderc.exe";
        private const string IncludePath = "Shader";
        private const string SamplerRegexVar = "sampler";
        private const string SamplerRegex = @"SAMPLER2D\s*\(\s*(?<sampler>\w+)\s*\,\s*(?<index>\d+)\s*\)\s*\;";
        private const string ParamRegexVar = "param";
        private const string VecParamRegex = @"uniform\s+vec4\s+(?<param>\w+)\s*\;";

        private const string VsD3DArgs = "--platform windows -p vs_5_0 -O 3 --type vertex -f $path -o $output -i $include";
        private const string FsD3DArgs = "--platform windows -p ps_5_0 -O 3 --type fragment -f $path -o $output -i $include";

        private const string VsGLArgs = "--platform $platform -p 440 --type vertex -f $path -o $output -i $include";
        private const string FsGLArgs = "--platform $platform -p 440 --type fragment -f $path -o $output -i $include";

        private const string PlatformWinArgs = "windows";
        private const string PlatformLinuxArgs = "linux";
        private const string PlatformMacArgs = "osx";

        public static ShaderCompileResult Compile(GraphicsBackend backend, string vsSrcPath, string fsSrcPath)
        {
            Process? procVs;
            Process? procFs;

            string tempVsBinOutput = string.Empty;
            string tempFsBinOutput = string.Empty;

            var vsBuildResult = new StringBuilder();
            var fsBuildResult = new StringBuilder();

            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (currentDir == null)
            {
                throw new ApplicationException("Failed to compile Shader: Could not get current executing assembly directory");
            }

            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = CompilerPath,
                WorkingDirectory = currentDir
            };

            string platform;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                platform = PlatformWinArgs;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                platform = PlatformLinuxArgs;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                platform = PlatformMacArgs;
            }
            else
            {
                throw new ApplicationException("Unsupported platform for compiling shaders.");
            }


            try
            {

                StringBuilder vsArgs = backend switch
                {
                    GraphicsBackend.Direct3D => new StringBuilder(VsD3DArgs.Replace("$path", vsSrcPath)),
                    GraphicsBackend.OpenGL => new StringBuilder(VsGLArgs.Replace("$path", vsSrcPath).Replace("$platform", platform)),
                    _ => throw new ApplicationException($"Unrecognized shader backend: {backend}"),
                };

                tempVsBinOutput = Path.Combine(Path.GetTempPath(), $"{backend}_" + Path.GetFileNameWithoutExtension(vsSrcPath) + ".bin");

                vsArgs = vsArgs.Replace("$output", tempVsBinOutput);

                vsArgs = vsArgs.Replace("$include", Path.Combine(currentDir, IncludePath));

                processInfo.Arguments = vsArgs.ToString();

                procVs = Process.Start(processInfo);

                procVs?.WaitForExit();

                var output = procVs?.ExitCode ?? -1;

                if (output != 0 && output != -1)
                {
                    using var reader = procVs?.StandardError;

                    if (reader != null)
                    {
                        vsBuildResult.AppendLine(reader.ReadToEnd());
                    }
                }

            }
            catch (Exception e)
            {
                vsBuildResult.AppendLine();
                vsBuildResult.AppendLine(e.Message);
            }

            try
            {
                StringBuilder fsArgs = backend switch
                {
                    GraphicsBackend.Direct3D => new StringBuilder(FsD3DArgs.Replace("$path", fsSrcPath)),
                    GraphicsBackend.OpenGL => new StringBuilder(FsGLArgs.Replace("$path", fsSrcPath).Replace("$platform", platform)),
                    _ => throw new ApplicationException($"Unrecognized shader backend: {backend}"),
                };

                fsArgs = fsArgs.Replace("$path", fsSrcPath);

                tempFsBinOutput = Path.Combine(Path.GetTempPath(), $"{backend}_" + Path.GetFileNameWithoutExtension(fsSrcPath) + ".bin");

                fsArgs = fsArgs.Replace("$output", tempFsBinOutput);

                fsArgs = fsArgs.Replace("$include", Path.Combine(currentDir, IncludePath));

                processInfo.Arguments = fsArgs.ToString();

                procFs = Process.Start(processInfo);

                procFs?.WaitForExit();

                var output = procFs?.ExitCode ?? -1;

                if (output != 0 && output != -1)
                {
                    using var reader = procFs?.StandardError;

                    if (reader != null)
                    {
                        fsBuildResult.AppendLine(reader.ReadToEnd());
                    }
                }

            }
            catch (Exception e)
            {
                fsBuildResult.AppendLine();
                fsBuildResult.AppendLine(e.Message);
            }

            bool vsOk = File.Exists(tempVsBinOutput);
            bool fsOk = File.Exists(tempFsBinOutput);

            if (vsOk && fsOk)
            {
                var vsBytes = File.ReadAllBytes(tempVsBinOutput);
                var fsBytes = File.ReadAllBytes(tempFsBinOutput);

                var fsStream = File.OpenRead(fsSrcPath);

                ParseUniforms(fsStream, out var samplers, out var @params);

                var result = new ShaderCompileResult(vsBytes, fsBytes, samplers, @params);

                File.Delete(tempVsBinOutput);
                File.Delete(tempFsBinOutput);

                return result;
            }
            else
            {
                if (vsOk)
                {
                    File.Delete(tempVsBinOutput);
                }

                if (fsOk)
                {
                    File.Delete(tempFsBinOutput);
                }

                if (!vsOk)
                {
                    throw new Exception("Error building vertex shader on " + vsSrcPath + " : " + vsBuildResult);
                }

                throw new Exception("Error building fragment shader on " + fsSrcPath + " : " + fsBuildResult);
            }
        }

        public static void ParseUniforms(Stream fsStream, out string[] samplers, out string[] parameters)
        {
            string? line;

            Regex sampler_regex = new Regex(SamplerRegex);
            Regex param_regex = new Regex(VecParamRegex);

            var samplers_list = new List<string>();
            var params_list = new List<string>();

            using (var reader = new StreamReader(fsStream))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    Match sampler_match = sampler_regex.Match(line);

                    if (sampler_match.Success)
                    {
                        string sampler_name = sampler_match.Groups[SamplerRegexVar].Value;
                        samplers_list.Add(sampler_name);
                    }
                    else
                    {
                        Match param_match = param_regex.Match(line);

                        if (param_match.Success)
                        {
                            string param_name = param_match.Groups[ParamRegexVar].Value;

                            params_list.Add(param_name);
                        }
                    }
                }
            }

            samplers = samplers_list.Count > 0 ? samplers_list.ToArray() : Array.Empty<string>();

            parameters = params_list.Count > 0 ? params_list.ToArray() : Array.Empty<string>();
        }
    }

    public static ShaderSerializableData BuildShader(string id, GraphicsBackend backend, string vsRelativePath, string fsRelativePath)
    {
        var fullPathVs = Loader.GetFullResourcePath(vsRelativePath);
        var fullPathFs = Loader.GetFullResourcePath(fsRelativePath);

        var resultCompile = ShaderCompiler.Compile(backend, fullPathVs, fullPathFs);

        var result = new ShaderSerializableData()
        {
            Id = id,
            VertexShader = resultCompile.VsBytes,
            FragmentShader = resultCompile.FsBytes,
            Samplers = resultCompile.Samplers,
            Params = resultCompile.Params,
            Backend = backend
        };

        return result;
    }
}
