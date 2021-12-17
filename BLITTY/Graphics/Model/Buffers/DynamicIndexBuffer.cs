using static BLITTY.Native.BGFX;

namespace BLITTY;

public unsafe class DynamicIndexBuffer : RenderResource
{
    internal BGFX_DynamicIndexBufferHandle Handle { get; private set; }

    internal DynamicIndexBuffer(string id, BGFX_DynamicIndexBufferHandle handle) : base(id)
    {
        Handle = handle;
    }

    public void Update(int startIndex, Span<ushort> indices)
    {
        Graphics.UpdateDynamicIndexBuffer(this, startIndex, indices);
    }

    protected override void Free()
    {
        Graphics.DestroyDynamicIndexBuffer(this);
    }
}
