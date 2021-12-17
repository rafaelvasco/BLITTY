
namespace BLITTY;

public static partial class Loader
{
    public static ContentPak LoadPak(byte[] data)
    {
        var pak = Serializer.Read<ContentPak>(data);

        return pak;
    }

    public static ContentPak LoadPak(string pakPath)
    {
        var fileBytes = File.ReadAllBytes(pakPath);

        return LoadPak(fileBytes);
    }
}
