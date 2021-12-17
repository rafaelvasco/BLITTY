using BLITTY.Native;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BLITTY;

public class Pixmap : GameAsset
{
    public int Width { get; private set; }

    public int Height { get; private set; }

    public byte[] Data => _pixelData;

    public int SizeBytes { get; }

    public int Stride => Width * 4;

    private byte[] _pixelData;

    internal Pixmap(string id, byte[] srcData, int width, int height) : base(id)
    {
        Width = width;
        Height = height;
        SizeBytes = srcData.Length;

        _pixelData = GC.AllocateArray<byte>(srcData.Length, pinned: true);

        Unsafe.CopyBlockUnaligned(ref _pixelData[0], ref srcData[0], (uint)SizeBytes);

        ConvertToEngineRepresentation();

    }

    internal Pixmap(string id, int width, int height) : base(id)
    {
        Width = width;
        Height = height;
        SizeBytes = width * height;

        int length = width * height * 4;

        _pixelData = new byte[length];
    }

    public unsafe void SaveToFile(string path)
    {
        using var stream = File.OpenWrite(path);

        Span<byte> pixelDataCopy = stackalloc byte[_pixelData.Length];

        ConvertPixelDataToExportFormat(ref pixelDataCopy);

        var image_writer = new ImageWriter();
        image_writer.WritePng(_pixelData, Width, Height, STB_ColorComponents.RedGreenBlueAlpha, stream);
    }

    private unsafe void ConvertPixelDataToExportFormat(ref Span<byte> pixels)
    {
        fixed (byte* ptr = pixels)
        {
            var len = pixels.Length - 4;
            for (int i = 0; i <= len; i += 4)
            {
                byte b = *(ptr + i);
                byte g = *(ptr + i + 1);
                byte r = *(ptr + i + 2);
                byte a = *(ptr + i + 3);

                *(ptr + i) = r;
                *(ptr + i + 1) = g;
                *(ptr + i + 2) = b;
                *(ptr + i + 3) = a;
            }
        }
    }

    private unsafe void ConvertToEngineRepresentation(bool premultiplyAlpha = false)
    {
        var pd = _pixelData;

        fixed (byte* p = &MemoryMarshal.GetArrayDataReference(pd))
        {
            var len = pd.Length - 4;
            for (int i = 0; i <= len; i += 4)
            {
                byte r = *(p + i);
                byte g = *(p + i + 1);
                byte b = *(p + i + 2);
                byte a = *(p + i + 3);

                if (!premultiplyAlpha)
                {
                    *(p + i) = b;
                    *(p + i + 1) = g;
                    *(p + i + 2) = r;
                }
                else
                {
                    *(p + i) = (byte)((b * a) / 255);
                    *(p + i + 1) = (byte)((g * a) / 255);
                    *(p + i + 2) = (byte)(r * a / 255);
                }

                *(p + i + 3) = a;
            }
        }
    }
}
