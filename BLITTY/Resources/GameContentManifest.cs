using System.Text.Json.Serialization;

namespace BLITTY;

public class GameContentManifest
{
    [JsonPropertyName("resources")]
    public Dictionary<string, ContentFileGroup>? Resources { get; set; }
}
