using System.Text.Json;

namespace BLITTY;

public static partial class Loader
{
    private const string GameConfigFile = "config.json";

    public static string RootPath { get; set; } = Content.ContentFolder;

    public static string GetFullResourcePath(string relativePath)
    {
        string fullPath = Path.Combine(RootPath, relativePath);

        if (Platform.RunningPlatform == RunningPlatform.Windows)
        {
            fullPath = fullPath.Replace('\\', '/');
        }

        return fullPath;
    }

    public static GameConfig LoadGameConfig()
    {
        var jsonFile = File.ReadAllText(GameConfigFile);

        GameConfig? config = JsonSerializer.Deserialize<GameConfig>(jsonFile);

        if (config == null)
        {
            throw new ApplicationException("Couldn't deserialize config.json file");
        }

        return config;
    }

    public static GameContentManifest LoadContentManifest(string contentFolder)
    {
        var jsonGameContentManifest = File.ReadAllText(Path.Combine(contentFolder, Content.ContentManifestFileName));

        GameContentManifest? manifest = JsonSerializer.Deserialize<GameContentManifest>(jsonGameContentManifest);

        if (manifest == null)
        {
            throw new ApplicationException("Could not load Content Manifest");
        }

        return manifest;
    }
}
