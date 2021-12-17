using static BLITTY.Native.BGFX;

namespace BLITTY;

public enum VertexAttribute
{
    Position = BGFX_Attrib.Position,
    Color0 = BGFX_Attrib.Color0,
    Color1 = BGFX_Attrib.Color1,
    Color2 = BGFX_Attrib.Color2,
    Texture0 = BGFX_Attrib.TexCoord0,
    Texture1 = BGFX_Attrib.TexCoord1,
    Texture2 = BGFX_Attrib.TexCoord2
}
