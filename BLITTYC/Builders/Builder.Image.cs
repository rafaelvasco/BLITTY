
using BLITTY;
namespace BLITTYC;

public static partial class Builder
{
    public static ImageSerializableData BuildImage(string id, string relativePath)
    {
        using var file = File.OpenRead(Loader.GetFullResourcePath(relativePath));

        var image = ImageDataIO.LoadImageData(file);

        var result = new ImageSerializableData()
        {
            Id = id,
            Data = image.Data,
            Width = image.Width,
            Height = image.Height
        };

        return result;
    }
}
