using MessagePack;

namespace BLITTY;

[MessagePackObject]
public struct Vec2SerializableData
{
    [Key(0)]
    public float X { get; set; }

    [Key(1)]
    public float Y { get; set; }

    public Vec2SerializableData(float x, float y)
    {
        X = x;
        Y = y;
    }
}
