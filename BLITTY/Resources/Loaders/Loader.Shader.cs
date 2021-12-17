namespace BLITTY;

public static partial class Loader
{
    public static Shader LoadShader(ShaderSerializableData shaderData)
    {
        var shader = Graphics.CreateShader(shaderData.Id, shaderData.VertexShader, shaderData.FragmentShader, shaderData.Samplers, shaderData.Params);

        return shader;
    }
}
