using MessagePack;

namespace BLITTY;

[MessagePackObject]
public struct ContentPak
{
    [Key(0)]
    public string Name { get; set; }

    [Key(1)]
    public Dictionary<string, ImageSerializableData> Images { get; set; }

    //public Dictionary<string, TextureAtlasData> Atlases { get; set; }

    [Key(2)]
    public Dictionary<string, ShaderSerializableData> Shaders { get; set; }

    //public Dictionary<string, FontData> Fonts { get; set; }

    //public Dictionary<string, TextFileData> TextFiles { get; set; }

    [Key(3)]
    public Dictionary<string, SoundSerializableData> Sounds { get; set; }

    [Key(4)]
    public int TotalResourcesCount { get; set; }

    public ContentPak(string name)
    {
        Name = name;
        Images = new Dictionary<string, ImageSerializableData>();
        Shaders = new Dictionary<string, ShaderSerializableData>();
        Sounds = new Dictionary<string, SoundSerializableData>();
        //Fonts = new Dictionary<string, FontData>();
        //TextFiles = new Dictionary<string, TextFileData>();
        //Atlases = new Dictionary<string, TextureAtlasData>();
        TotalResourcesCount = 0;
    }

}
