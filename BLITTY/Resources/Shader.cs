using System.Numerics;
using System.Runtime.CompilerServices;
using static BLITTY.Native.BGFX;

namespace BLITTY;

public class ShaderParameter
{
    internal BGFX_UniformHandle Handle { get; private set; }

    public string Name { get; private set; }

    public bool Constant { get; set; } = false;

    internal bool SubmitedOnce;

    public Vector4 Value => _value;

    private Vector4 _value;


    internal ShaderParameter(BGFX_UniformHandle handle, string name)
    {
        Name = name;
        Handle = handle;
    }

    public void SetValue(float v)
    {
        _value.X = v;
    }

    public void SetValue(Vector2 v)
    {
        _value.X = v.X;
        _value.Y = v.Y;
    }

    public void SetValue(Vector3 v)
    {
        _value.X = v.X;
        _value.Y = v.Y;
        _value.Z = v.Z;
    }

    public void SetValue(Vector4 v)
    {
        _value = v;
    }

    public void SetValue(Color color)
    {
        _value.X = color.Rf;
        _value.Y = color.Gf;
        _value.Z = color.Bf;
        _value.W = color.Af;
    }
}

public class ShaderSampler
{
    internal BGFX_UniformHandle Handle { get; private set; }

    public Texture2D? Texture { get; internal set; }

    internal ShaderSampler(BGFX_UniformHandle handle)
    {
        Handle = handle;
        Texture = null;
    }
}

public class Shader : GameAsset
{
    internal BGFX_ProgramHandle Handle { get; private set; }

    internal ShaderSampler[] Samplers { get; private set; }

    internal ShaderParameter[] Parameters { get; private set; }

    private readonly Dictionary<string, int> _paramsMap;

    private int _textureIndex;


    internal Shader(string id, BGFX_ProgramHandle handle, ShaderSampler[] samplers, ShaderParameter[] parameters) : base(id)
    {
        Handle = handle;
        Samplers = samplers;
        Parameters = parameters;

        _paramsMap = new Dictionary<string, int>();

        for (int i = 0; i < parameters.Length; ++i)
        {
            _paramsMap.Add(parameters[i].Name, i);
        }
    }

    internal void SetTexture(int slot, Texture2D texture)
    {
        slot = Math.Max(slot, 0);

        Samplers[slot].Texture = texture;

        if (slot > _textureIndex)
        {
            _textureIndex = slot;
        }
    }

    internal unsafe void Apply()
    {

        if (_textureIndex == 0 && Samplers[0].Texture != null)
        {
            BGFX_SetTexture(0, Samplers[0].Handle, Samplers[0].Texture!.Handle, Samplers[0].Texture!.SamplerFlags);
        }
        else if (_textureIndex > 0)
        {
            for (int i = 0; i < _textureIndex; ++i)
            {
                if (Samplers[i].Texture != null)
                {
                    var texture = Samplers[i].Texture;
                    BGFX_SetTexture((byte)i, Samplers[i].Handle, texture!.Handle, texture.SamplerFlags);
                }
            }
        }

        if (Parameters == null)
        {
            return;
        }

        for (int i = 0; i < Parameters.Length; ++i)
        {
            var p = Parameters[i];

            if (p.Constant)
            {
                if (p.SubmitedOnce)
                {
                    continue;
                }

                p.SubmitedOnce = true;
            }

            var val = p.Value;

            BGFX_SetShaderUniform(p.Handle, Unsafe.AsPointer(ref val), (ushort)1);
        }
    }

    public ShaderParameter? GetParam(string name)
    {
        return _paramsMap.TryGetValue(name, out var index) ? Parameters[index] : null;
    }

    protected override void FreeUnmanaged()
    {
        if (!Handle.Valid)
        {
            return;
        }

        Graphics.DisposeShader(this);
    }
}
