using BLITTY;

namespace BLITTYC;

public static partial class Builder
{
    public static void BuildGame(string contentFullPath, GameContentManifest manifest)
    {
        Console.WriteLine("Building Game...");

        Loader.RootPath = contentFullPath;

        var paks = BuildGameContent(manifest);

        foreach (var pak in paks)
        {
            using var pakFile = File.Create(Path.Combine(contentFullPath, pak.Name + ".pak"));

            Serializer.Write(pakFile, pak);

            Console.WriteLine($"Wrote Pak {pak.Name}");
        }
    }

    private static List<ContentPak> BuildGameContent(GameContentManifest manifest)
    {
        var resourceGroups = manifest.Resources;

        if (resourceGroups == null)
        {
            throw new ApplicationException("Invalid GameContentManifest: Resources Map is Empty.");
        }

        var results = new List<ContentPak>();

        foreach (var (groupKey, group) in resourceGroups)
        {
            Console.WriteLine($"Building Pak: {groupKey}");

            var pak = new ContentPak(groupKey);

            if (group.Images != null)
            {
                foreach (var imageInfo in group.Images)
                {
                    if (imageInfo.Id == null || imageInfo.Path == null)
                    {
                        throw new ApplicationException($"Invalid Image Info ({imageInfo.Id}) on GameContentManifest");
                    }

                    var imageData = BuildImage(imageInfo.Id, imageInfo.Path);

                    pak.Images.Add(imageData.Id!, imageData);

                    pak.TotalResourcesCount++;

                    Console.WriteLine($"Added Image: {imageInfo.Id}");
                }
            }

            if (group.Shaders != null)
            {
                foreach (var shaderInfo in group.Shaders)
                {
                    if (shaderInfo.Id == null || shaderInfo.VsPath == null || shaderInfo.FsPath == null || shaderInfo.Backends == null)
                    {
                        throw new ApplicationException($"Invalid Shader Info ({shaderInfo.Id}) on GameContentManifest");
                    }

                    foreach (var backend in shaderInfo.Backends)
                    {
                        var id = $"{shaderInfo.Id}_{backend}";

                        var graphicsBackend = (GraphicsBackend)Enum.Parse(typeof(GraphicsBackend), backend);

                        var shaderData = BuildShader(id, graphicsBackend, shaderInfo.VsPath, shaderInfo.FsPath);

                        pak.Shaders.Add(id, shaderData);

                        pak.TotalResourcesCount++;

                        Console.WriteLine($"Added Shader: {id}, Backend: {shaderData.Backend}");
                    }
                }
            }

            if (group.Sounds != null)
            {
                foreach (var soundInfo in group.Sounds)
                {
                    if (soundInfo.Id == null || soundInfo.Path == null)
                    {
                        throw new ApplicationException($"Invalid Sound Info ({soundInfo.Id}) on AppResourcesManifest");
                    }

                    var soundData = BuildSound(soundInfo.Id, soundInfo.Streamed, soundInfo.Path);

                    pak.Sounds.Add(soundData.Id, soundData);

                    pak.TotalResourcesCount++;

                    Console.WriteLine($"Added Sound: {soundInfo.Id}");
                }
            }

            results.Add(pak);
        }

        return results;
    }
}
