using BLITTY.Audio;

namespace BLITTY;

public static class Content
{
    public const string ContentManifestFileName = "content.json";

    public const string ContentFolder = "Content";

    private static Dictionary<string, GameAsset>? _loadedResources;
    private static Dictionary<string, GameAsset>? _runTimeResources;

    internal static void Init(GameConfig config)
    {
        _loadedResources = new Dictionary<string, GameAsset>();
        _runTimeResources = new Dictionary<string, GameAsset>();

        LoadContentPack("base");

        if (config.PreloadPaks != null)
        {
            foreach (var pakName in config.PreloadPaks)
            {
                LoadContentPack(pakName);
            }
        }
    }

    public static Texture2D GetLogo()
    {
        return Get<Texture2D>("Logo");
    }

    public static Texture2D GetLogoSmall()
    {
        return Get<Texture2D>("LogoSmall");
    }

    public static T Get<T>(string resourceId) where T : GameAsset
    {
        if (_loadedResources == null)
        {
            throw new ApplicationException("Trying to call Get before Content manager is initialized.");
        }

        if (_loadedResources.TryGetValue(resourceId, out var resource))
        {
            return (T)resource;
        }

        throw new Exception($"Can't find resource with ID: {resourceId}");
    }

    public static Texture2D CreateTexture2D(string id, Pixmap pixmap, bool tiled = false, TextureFilter filter = TextureFilter.NearestNeighbor)
    {
        var texture = Graphics.CreateTexture2D(id, pixmap, tiled, filter);

        RegisterRuntimeLoaded(texture);

        return texture;
    }

    public static unsafe Texture2D CreateTexture2D(string id, int width, int height, bool tiled = false, TextureFilter filter = TextureFilter.NearestNeighbor)
    {
        var pixmap = new Pixmap(id + "_pixmap", width, height);

        var texture = Graphics.CreateTexture2D(id, pixmap, tiled, filter);

        RegisterRuntimeLoaded(texture);

        return texture;
    }

    public static Pixmap CreatePixmap(string id, int width, int height)
    {
        var pixmap = new Pixmap(id, width, height);

        RegisterRuntimeLoaded(pixmap);

        return pixmap;
    }

    public static void LoadContentPack(string pakName)
    {
        var pakPath = Path.Combine(ContentFolder,
            !pakName.Contains(".pak") ? pakName + ".pak" : pakName);

        ContentPak pak = Loader.LoadPak(pakPath);

        if (pak.TotalResourcesCount == 0)
        {
            return;
        }

        if (_loadedResources == null)
        {
            throw new ApplicationException("Trying to call LoadContentPack before Content Manager is initialized.");
        }

        if (pak.Images != null)
        {
            foreach (var (imageKey, imageData) in pak.Images)
            {
                if (imageData.Id == null && imageData.Data == null)
                {
                    throw new ApplicationException("Invalid SImage Object on ResourcePak: Id or Data are Empty.");
                }

                Texture2D texture = Loader.LoadTexture2D(imageData);

                texture.PakId = pak.Name;

                _loadedResources.Add(texture.Id, texture);
            }
        }

        //if (pak.Atlases != null)
        //{
        //    foreach (var (atlasKey, atlasData) in pak.Atlases)
        //    {
        //        TextureAtlas atlas = ResourceLoader.LoadAtlas(atlasData);
        //        _loadedResources.Add(atlas.Id, atlas);
        //        _pakMap[pakName][res_name_map_idx++] = atlasKey;
        //    }
        //}

        //if (pak.Fonts != null)
        //{
        //    foreach (var (fontKey, fontData) in pak.Fonts)
        //    {
        //        TextureFont font = ResourceLoader.LoadFont(fontData);
        //        _loadedResources.Add(font.Id, font);
        //        _pakMap[pakName][res_name_map_idx++] = fontKey;
        //    }
        //}

        if (pak.Shaders != null)
        {
            foreach (var (shaderKey, shaderProgramData) in pak.Shaders)
            {
                if (shaderProgramData.Backend != Graphics.GraphicsBackend)
                {
                    continue;
                }

                Shader shader = Loader.LoadShader(shaderProgramData);
                shader.PakId = pak.Name;
                _loadedResources.Add(shaderKey.Replace($"_{shaderProgramData.Backend}", ""), shader);
            }
        }

        if (pak.Sounds != null)
        {
            foreach(var (soundKey, soundData) in pak.Sounds)
            {
                Sound sound = Loader.LoadSound(soundData);
                sound.PakId = pak.Name;
                _loadedResources.Add(soundKey, sound);
            }
        }

        //if (pak.TextFiles != null)
        //{
        //    foreach (var (txtKey, textFileData) in pak.TextFiles)
        //    {
        //        TextFile text_file = ResourceLoader.LoadTextFile(textFileData);
        //        _loadedResources.Add(text_file.Id, text_file);
        //        _pakMap[pakName][res_name_map_idx++] = txtKey;
        //    }
        //}
    }

    internal static void RegisterRuntimeLoaded(GameAsset resource)
    {
        if (_runTimeResources == null)
        {
            throw new ApplicationException("Trying to register runtime loaded asset before Content Manager is initialized.");
        }

        _runTimeResources.Add(resource.Id, resource);
    }

    public static void Free(GameAsset resourceFree)
    {
        if (_loadedResources == null || _runTimeResources == null)
        {
            throw new ApplicationException("Trying to free resource before Content Manager is initialized.");
        }

        if (_loadedResources.TryGetValue(resourceFree.Id, out var resource1))
        {
            resource1.Dispose();
            _loadedResources.Remove(resource1.Id);
            return;
        }

        if (_runTimeResources.TryGetValue(resourceFree.Id, out var resource2))
        {
            resource2.Dispose();
            _runTimeResources.Remove(resource2.Id);
            return;
        }
    }

    internal static void Free()
    {
        Console.WriteLine("Freeing Content...");

        if (_loadedResources == null || _runTimeResources == null)
        {
            throw new ApplicationException("Trying to free resources before Content Manager is initialized.");
        }

        foreach (var (_, resource) in _loadedResources)
        {
            Console.WriteLine($"Freeing {resource.Id}");

            resource.Dispose();
        }

        foreach (var (_, resource) in _runTimeResources)
        {
            Console.WriteLine($"Freeing {resource.Id}");

            resource.Dispose();
        }

        _loadedResources.Clear();
        _runTimeResources.Clear();
    }

}
