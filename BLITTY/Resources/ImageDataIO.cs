
using BLITTY.Native;

namespace BLITTY;

public struct ImageData
{
    public byte[] Data;
    public int Width;
    public int Height;
}

public static class ImageDataIO
{

    public static ImageData LoadImageData(Stream stream)
    {
        var image = STB_ImageResult.FromStream(stream, STB_ColorComponents.RedGreenBlueAlpha);

        if (image.Data == null)
        {
            throw new ApplicationException("Failed to Load ImageData");
        }

        return new ImageData
        {
            Data = image.Data,
            Width = image.Width,
            Height = image.Height
        };
    }

    public static void SaveImageData(byte[] data, Stream output)
    {
        throw new NotImplementedException();
    }
}
