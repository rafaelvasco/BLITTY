using static BLITTY.Native.BGFX;

namespace BLITTY;

public static unsafe partial class Graphics
{
    internal static Shader CreateShader(string id, byte[] vertexSource, byte[] fragSource, string[] samplers, string[] parameters)
    {
        static BGFX_ShaderHandle CreateShaderSource(byte[] bytes)
        {
            BGFX_ShaderHandle handle;
            var data = BGFX_AllocGraphicsMemoryBuffer<byte>(bytes);
            handle = BGFX_CreateShader(data);
            return handle;
        }

        if (vertexSource.Length == 0 || fragSource.Length == 0)
        {
            throw new ArgumentException("Cannot create a Shader with Empty sources");
        }

        var vertexShader = CreateShaderSource(vertexSource);
        var fragShader = CreateShaderSource(fragSource);

        if (!vertexShader.Valid || !fragShader.Valid)
        {
            throw new ApplicationException("Could not load shader sources");
        }

        var program = BGFX_CreateShaderProgram(vertexShader, fragShader, _destroyShaders: true);

        var shaderSamples = new ShaderSampler[samplers.Length];

        for (int i = 0; i < samplers.Length; ++i)
        {
            var samplerHandle = BGFX_CreateShaderUniform(samplers[i], BGFX_UniformType.Sampler, 1);
            shaderSamples[i] = new ShaderSampler(samplerHandle);
        }

        var shaderParameters = new ShaderParameter[parameters.Length];

        for (int i = 0; i < parameters.Length; ++i)
        {
            var parameterHandle = BGFX_CreateShaderUniform(parameters[i], BGFX_UniformType.Vec4, 4);
            shaderParameters[i] = new ShaderParameter(parameterHandle, parameters[i]);
        }

        var shader = new Shader(id, program, shaderSamples, shaderParameters);

        return shader;
    }

    internal static void DisposeShader(Shader shader)
    {
        if (shader.Samplers != null)
        {
            for (int i = 0; i < shader.Samplers.Length; ++i)
            {
                BGFX_DestroyShaderUniform(shader.Samplers[i].Handle);
            }
        }

        if (shader.Parameters != null)
        {
            for (int i = 0; i < shader.Parameters.Length; ++i)
            {
                BGFX_DestroyShaderUniform(shader.Parameters[i].Handle);
            }
        }
    }

    internal static void SubmitShader(Shader shader)
    {
        for (int i = 0; i <= shader.Samplers.Length; ++i)
        {
            var sampler = shader.Samplers[i];

            if (sampler.Texture != null)
            {
                BGFX_SetTexture((byte)i, sampler.Handle, sampler.Texture.Handle, sampler.Texture.SamplerFlags);
            }
            else
            {
                throw new ApplicationException("Submitting shader with empty sampler texture");
            }

        }

        if (shader.Parameters == null)
        {
            return;
        }

        for (int i = 0; i < shader.Parameters.Length; ++i)
        {
            var p = shader.Parameters[i];

            if (p.Constant)
            {
                if (p.SubmitedOnce)
                {
                    continue;
                }

                p.SubmitedOnce = true;

            }

            var val = p.Value;

            BGFX_SetShaderUniform(shader.Samplers[i].Handle, &val, 1);
        }
    }
}
