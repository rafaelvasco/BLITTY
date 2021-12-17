using static BLITTY.Native.BGFX;

namespace BLITTY;

public unsafe class DynamicVertexBuffer : RenderResource
{
    internal BGFX_DynamicVertexBufferHandle Handle { get; private set; }

    internal DynamicVertexBuffer(string id, BGFX_DynamicVertexBufferHandle handle) : base(id)
    {
        Handle = handle;
    }

    public void Update(int startVertex, Span<VertexPCT> vertices)
    {
        Graphics.UpdateDynamicVertexBuffer(this, startVertex, vertices);
    }

    protected override void Free()
    {
        Graphics.DestroyDynamicVertexBuffer(this);
    }
}
