
using MessagePack;

namespace BLITTY;

[MessagePackObject]
public struct RectSerializableData
{
    [Key(0)]
    public int X { get; set; }

    [Key(1)]
    public int Y { get; set; }

    [Key(2)]
    public int W { get; set; }

    [Key(3)]
    public int H { get; set; }

    public RectSerializableData(int x, int y, int w, int h)
    {
        X = x;
        Y = y;
        W = w;
        H = h;
    }
}
