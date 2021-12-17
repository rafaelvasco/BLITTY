using static BLITTY.Native.BGFX;

namespace BLITTY;

public struct TransientVertexBuffer
{
    internal BGFX_TransientVertexBuffer Handle;

    internal TransientVertexBuffer(BGFX_TransientVertexBuffer handle)
    {
        Handle = handle;
    }
}
