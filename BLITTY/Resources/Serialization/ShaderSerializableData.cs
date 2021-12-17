﻿using MessagePack;

namespace BLITTY;

[MessagePackObject]
public struct ShaderSerializableData
{
    [Key(0)]
    public string Id { get; set; }

    [Key(1)]
    public byte[] VertexShader { get; set; }

    [Key(2)]
    public byte[] FragmentShader { get; set; }

    [Key(3)]
    public string[] Samplers { get; set; }

    [Key(4)]
    public string[] Params { get; set; }

    [Key(5)]
    public GraphicsBackend Backend { get; set; }
}
