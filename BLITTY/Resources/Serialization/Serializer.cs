using MessagePack;

namespace BLITTY;

public static class Serializer
{
    private static MessagePackSerializerOptions _options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);

    public static void Write<T>(Stream stream, T data)
    {
        MessagePackSerializer.Serialize(stream, data, _options);
    }

    public static T Read<T>(ReadOnlyMemory<byte> data)
    {
        return MessagePackSerializer.Deserialize<T>(data, _options);
    }
}
