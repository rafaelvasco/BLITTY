using static BLITTY.Native.BGFX;

namespace BLITTY;

public static unsafe partial class Graphics
{
    internal static Texture2D CreateTexture2D(string id, Pixmap pixmap, bool tiled, TextureFilter filter)
    {
        var samplerFlags = CalculateSamplerFlags(tiled, filter);

        var handle = BGFX_CreateTexture2D(pixmap.Width, pixmap.Height, false, 0, BGFX_TextureFormat.BGRA8, samplerFlags, BGFX_GetMemoryBufferReference<byte>(pixmap.Data));

        var texture = new Texture2D(id, handle, pixmap, samplerFlags);

        return texture;
    }

    internal static void UpdateTexture2D(Texture2D texture, Pixmap pixmap, int targetX = 0, int targetY = 0, int targetW = 0, int targetH = 0)
    {
        var data = BGFX_GetMemoryBufferReference<byte>(pixmap.Data);

        if (targetW == 0)
        {
            targetW = texture.Width;
        }

        if (targetH == 0)
        {
            targetH = texture.Height;
        }

        BGFX_UpdateTexture2D(texture.Handle, 0, 0, (ushort)targetX, (ushort)targetY, (ushort)targetW, (ushort)targetH, data, (ushort)pixmap.Stride);
    }

    internal static void DisposeTexture2D(Texture2D texture2D)
    {
        BGFX_DestroyTexture(texture2D.Handle);
    }

    internal static BGFX_SamplerFlags CalculateSamplerFlags(bool tiled, TextureFilter filter)
    {
        var samplerFlags = BGFX_SamplerFlags.None;

        if (!tiled) samplerFlags = BGFX_SamplerFlags.UClamp | BGFX_SamplerFlags.VClamp;

        switch (filter)
        {
            case TextureFilter.NearestNeighbor:
                samplerFlags |= BGFX_SamplerFlags.Point;
                break;
        }

        return samplerFlags;
    }
}
