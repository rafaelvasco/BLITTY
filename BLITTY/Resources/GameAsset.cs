using BLITTY.MemoryManagement;

namespace BLITTY;

public abstract class GameAsset : DisposableResource
{
    public string Id { get; private set; }

    public string? PakId { get; internal set; }

    protected GameAsset(string id)
    {
        Id = id;
    }
}
