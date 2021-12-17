namespace BLITTY;

public class GameConfig
{
    public string? Title { get; set; }

    public int WindowWidth { get; set; }

    public int WindowHeight { get; set; }

    public bool Fullscreen { get; set; }

    public bool EnableAudio { get; set; }

    public string[]? PreloadPaks { get; set; }
}
