using static BLITTY.Native.BGFX;

namespace BLITTY;

public enum TextureFilter
{
    NearestNeighbor,
    Linear
}


public class Texture2D : GameAsset, IEquatable<Texture2D>
{
    internal BGFX_TextureHandle Handle { get; private set; }

    internal BGFX_SamplerFlags SamplerFlags { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public Pixmap Pixmap { get; }

    public const uint PixelSizeInBytes = sizeof(byte) * 4;

    public bool Tiled
    {
        get => (SamplerFlags & BGFX_SamplerFlags.UClamp) == 0 && (SamplerFlags & BGFX_SamplerFlags.VClamp) == 0;
        set => SamplerFlags = Graphics.CalculateSamplerFlags(value, Filter);
    }

    public TextureFilter Filter
    {
        get => (SamplerFlags & BGFX_SamplerFlags.Point) == 0 ? TextureFilter.NearestNeighbor : TextureFilter.Linear;
        set => SamplerFlags = Graphics.CalculateSamplerFlags(Tiled, value);
    }


    internal Texture2D(string id, BGFX_TextureHandle handle, Pixmap pixmap, BGFX_SamplerFlags flags) : base(id)
    {
        Pixmap = pixmap;
        Handle = handle;
        Width = pixmap.Width;
        Height = pixmap.Height;
        SamplerFlags = flags;
    }

    internal void ReloadPixels()
    {
        if (Pixmap == null)
        {
            return;
        }

        Graphics.UpdateTexture2D(this, Pixmap);
    }

    protected override void FreeUnmanaged()
    {
        if (Handle.Valid)
        {
            Graphics.DisposeTexture2D(this);
        }
    }

    public bool Equals(Texture2D? other)
    {
        return other != null && Handle.idx == other.Handle.idx;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        return Equals(obj as Texture2D);
    }

    public override int GetHashCode()
    {
        return Handle.idx.GetHashCode();
    }
}
