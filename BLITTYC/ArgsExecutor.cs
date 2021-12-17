using BLITTY;
using PowerArgs;

namespace BLITTYC;

public struct BuildActionArgs
{
    [ArgRequired(PromptIfMissing = true), ArgDescription("Game Folder"), ArgPosition(1), ArgShortcut("-g")]
    public string? GameFolder { get; set; }
}

public struct ParsePakArgs
{
    [ArgRequired(PromptIfMissing = true), ArgDescription("PAK Path"), ArgPosition(1), ArgShortcut("-p")]
    public string? PakPath { get; set; }
}


[ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
internal class ArgsExecutor
{
    [HelpHook, ArgShortcut("-?"), ArgDescription("Shows Usage Options")]
    public bool Help { get; set; }

    [ArgActionMethod, ArgDescription("Builds Game Assets"), ArgShortcut("b")]
    public void Build(BuildActionArgs args)
    {

        var gameFolderArg = args.GameFolder!;

        try
        {
            var contentFullPath = Path.Combine(gameFolderArg, Content.ContentFolder);

            if (!Directory.Exists(contentFullPath))
            {
                throw new ApplicationException("Could not find Content folder");
            }

            var contentManifest = Loader.LoadContentManifest(contentFullPath);

            Builder.BuildGame(contentFullPath, contentManifest);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [ArgActionMethod, ArgDescription("Parse Asset Pak"), ArgShortcut("pk")]
    public void ParsePak(ParsePakArgs args)
    {
        var pakPath = args.PakPath!;

        try
        {
            var pak = Loader.LoadPak(pakPath);

            Console.WriteLine($"Pak Name: {pak.Name}");
            Console.WriteLine($"Assets Count: {pak.TotalResourcesCount}");

            if (pak.Images != null)
            {
                Console.WriteLine("Images:");

                foreach (var item in pak.Images)
                {
                    Console.WriteLine($"Key: {item.Key}");
                    Console.WriteLine($"Width: {item.Value.Width}");
                    Console.WriteLine($"Height: {item.Value.Height}");
                    Console.WriteLine($"Data Bytes Length: {item.Value.Data.Length}");
                }
            }

            if (pak.Shaders != null)
            {
                Console.WriteLine("Shaders:");

                foreach (var item in pak.Shaders)
                {
                    Console.WriteLine($"Key: {item.Key}");
                    Console.WriteLine($"Data VS Bytes Length: {item.Value.VertexShader.Length}");
                    Console.WriteLine($"Data FS Bytes Length: {item.Value.FragmentShader.Length}");

                    Console.WriteLine($"Backend: {item.Value.Backend}");

                    if (item.Value.Samplers != null)
                    {
                        Console.WriteLine("Samplers:");
                        foreach (var sampler in item.Value.Samplers)
                        {
                            Console.WriteLine(sampler);
                        }
                    }
                    if (item.Value.Params != null)
                    {
                        Console.WriteLine("Params:");
                        foreach (var param in item.Value.Params)
                        {
                            Console.WriteLine(param);
                        }
                    }
                }
            }

            if (pak.Sounds != null)
            {
                Console.WriteLine("Sounds:");

                foreach (var item in pak.Sounds)
                {
                    Console.WriteLine($"Key: {item.Key}");
                    Console.WriteLine($"Data Length: {item.Value.Data.Length}");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
