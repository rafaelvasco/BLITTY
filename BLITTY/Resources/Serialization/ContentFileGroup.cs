using System.Text.Json.Serialization;

namespace BLITTY;

public class ContentFileGroup
{
    [JsonPropertyName("images")]
    public List<ImageResInfo>? Images { get; set; }

    [JsonPropertyName("shaders")]
    public List<ShaderResInfo>? Shaders { get; set; }

    [JsonPropertyName("text_files")]
    public List<CommonResInfo>? TextFiles { get; set; }

    [JsonPropertyName("fonts")]
    public List<FontResInfo>? Fonts { get; set; }

    [JsonPropertyName("atlases")]
    public List<AtlasResInfo>? Atlases { get; set; }

    [JsonPropertyName("sounds")]
    public List<SoundResInfo> Sounds { get; set; }

}
