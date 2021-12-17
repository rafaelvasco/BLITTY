using static BLITTY.Native.BGFX;

namespace BLITTY;

public unsafe class IndexBuffer : RenderResource
{
    internal BGFX_IndexBufferHandle Handle { get; private set; }

    internal IndexBuffer(string id, BGFX_IndexBufferHandle handle) : base(id)
    {
        Handle = handle;
    }

    protected override void Free()
    {
        Graphics.DestroyIndexBuffer(this);
    }
}
