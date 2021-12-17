using MessagePack;

namespace BLITTY;

[MessagePackObject]
public struct SoundSerializableData
{
    [Key(0)]
    public string Id { get; set; }

    [Key(1)]
    public byte[] Data { get; set; }

    [Key(2)]
    public bool Stream { get; set; }
}
