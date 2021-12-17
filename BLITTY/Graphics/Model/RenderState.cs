using static BLITTY.Native.BGFX;


namespace BLITTY;

public enum BlendMode : ulong
{
    Solid,
    Mask,
    Add,
    Alpha,
    AlphaPre,
    Multiply,
    Light,
    Invert
}

public struct RenderState
{
    public static RenderState Default => new(BlendMode.Alpha);

    internal BGFX_StateFlags State { get; private set; }

    private readonly BGFX_StateFlags _base = BGFX_StateFlags.WriteRgb |
        BGFX_StateFlags.WriteA |
        BGFX_StateFlags.WriteZ |
        BGFX_StateFlags.DepthTestLequal |
        BGFX_StateFlags.Msaa;

    private BGFX_StateFlags _blendState;

    public RenderState(BlendMode mode)
    {
        _blendState = BGFX_StateFlags.None;
        State = _base;
        SetBlendMode(mode);
    }

    public void SetBlendMode(BlendMode mode)
    {
        switch (mode)
        {
            case BlendMode.Solid:
                _blendState = 0x0;
                break;
            case BlendMode.Mask:
                _blendState = BGFX_StateFlags.BlendAlphaToCoverage;
                break;
            case BlendMode.Add:
                _blendState = BGFX_STATE_BLEND_FUNC_SEPARATE(BGFX_StateFlags.BlendSrcAlpha, BGFX_StateFlags.BlendOne, BGFX_StateFlags.BlendOne, BGFX_StateFlags.BlendOne);
                break;
            case BlendMode.Alpha:
                _blendState = BGFX_STATE_BLEND_FUNC_SEPARATE(BGFX_StateFlags.BlendSrcAlpha, BGFX_StateFlags.BlendInvSrcAlpha, BGFX_StateFlags.BlendOne, BGFX_StateFlags.BlendInvSrcAlpha);
                break;
            case BlendMode.AlphaPre:
                _blendState = BGFX_STATE_BLEND_FUNC(BGFX_StateFlags.BlendOne, BGFX_StateFlags.BlendInvSrcAlpha);
                break;
            case BlendMode.Multiply:
                _blendState = BGFX_STATE_BLEND_FUNC(BGFX_StateFlags.BlendDstColor, BGFX_StateFlags.BlendZero);
                break;
            case BlendMode.Light:
                _blendState = BGFX_STATE_BLEND_FUNC_SEPARATE(BGFX_StateFlags.BlendDstColor, BGFX_StateFlags.BlendOne, BGFX_StateFlags.BlendZero, BGFX_StateFlags.BlendOne);
                break;
            case BlendMode.Invert:
                _blendState = BGFX_STATE_BLEND_FUNC(BGFX_StateFlags.BlendInvDstColor, BGFX_StateFlags.BlendInvSrcColor);
                break;
        }

        State = _base | _blendState;

    }
}
