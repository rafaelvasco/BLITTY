namespace BLITTY;

public static partial class Loader
{
    public static Texture2D LoadTexture2D(ImageSerializableData image)
    {
        var id = image.Id!;

        var pixmap = new Pixmap(id, image.Data, image.Width, image.Height);

        var texture = Graphics.CreateTexture2D(id, pixmap, tiled: false, filter: TextureFilter.NearestNeighbor);

        return texture;
    }
}
