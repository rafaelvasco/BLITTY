using BLITTY;

namespace BLITTYC;

public static partial class Builder
{
    public static SoundSerializableData BuildSound(string id, bool streamed, string relativePath)
    {
        var bytes = File.ReadAllBytes(Loader.GetFullResourcePath(relativePath));

        var result = new SoundSerializableData()
        {
            Id = id,
            Data = bytes,
            Stream = streamed
        };

        return result;
    }
}