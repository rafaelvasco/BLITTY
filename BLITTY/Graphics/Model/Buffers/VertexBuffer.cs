using static BLITTY.Native.BGFX;

namespace BLITTY;

public unsafe class VertexBuffer : RenderResource
{
    internal BGFX_VertexBufferHandle Handle { get; private set; }

    internal VertexBuffer(string id, BGFX_VertexBufferHandle handle) : base(id)
    {
        Handle = handle;
    }

    protected override void Free()
    {
        Graphics.DestroyVertexBuffer(this);
    }
}
