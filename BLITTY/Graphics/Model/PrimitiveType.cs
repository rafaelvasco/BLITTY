using static BLITTY.Native.BGFX;

namespace BLITTY;

public enum PrimitiveType : ulong
{
    Triangles = 0x0,
    TriangleStrip = BGFX_StateFlags.PtTristrip,
    Lines = BGFX_StateFlags.PtLines,
    LineStrip = BGFX_StateFlags.PtLinestrip,
    Points = BGFX_StateFlags.PtPoints
}
