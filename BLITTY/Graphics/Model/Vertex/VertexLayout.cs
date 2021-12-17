using System.Runtime.CompilerServices;
using static BLITTY.Native.BGFX;

namespace BLITTY;

public unsafe struct VertexLayout
{
    internal BGFX_VertexLayout Handle;

    public void Begin()
    {
        BGFX_VertexLayoutBegin((BGFX_VertexLayout*)Unsafe.AsPointer(ref Handle), BGFX_GetRendererType());
    }

    public void Add(VertexAttribute attribute, VertexAttributeType type, int num, bool normalized, bool asInt)
    {
        BGFX_VertexLayoutAdd((BGFX_VertexLayout*)Unsafe.AsPointer(ref Handle), (BGFX_Attrib)attribute, (byte)num, (BGFX_AttribType)type, normalized, asInt);
    }

    public void End()
    {
        BGFX_VertexLayoutEnd((BGFX_VertexLayout*)Unsafe.AsPointer(ref Handle));
    }
}
