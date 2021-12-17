using BLITTY.Audio;
using System.Runtime;
using System.Text.Json;

namespace BLITTY;

#pragma warning disable CS8618

public partial class Game : IDisposable
{
    private static Game _instance;

    private bool _running;

    private const int MinGameWidth = 320;

    private const int MinGameHeight = 240;

    private Scene? _current_scene;

    private readonly GameConfig _config;

    private static bool _active;

    public static double FrameRate { get; set; } = 60;

    public static bool UnlockFrameRate { get; set; } = true;

    public static (int Width, int Height) WindowSize
    {
        get => Platform.GetWindowSize();
        set
        {
            var (width, height) = Platform.GetWindowSize();
            if (value.Width == width && value.Height == height)
            {
                return;
            }

            Platform.SetWindowSize(value.Width, value.Height);
        }
    }

    public static string Title
    {
        get => Platform.GetWindowTitle();
        set => Platform.SetWindowTitle(value);
    }

    public static bool Resizable
    {
        get => (Platform.GetWindowFlags() & Platform.WindowFlags.Resizable) != 0;
        set
        {
            Platform.SetWindowResizable(value);
        }
    }

    public static bool Borderless
    {
        get => (Platform.GetWindowFlags() & Platform.WindowFlags.Borderless) != 0;
        set
        {
            Platform.SetWindowBorderless(value);
        }
    }

    public static bool Fullscreen
    {
        get => Platform.IsFullscreen();
        set
        {
            if (Platform.IsFullscreen() == value)
            {
                return;
            }

            Platform.SetWindowFullscreen(value);
        }
    }

    public static bool ShowCursor
    {
        get => Platform.CursorVisible();
        set => Platform.ShowCursor(value);
    }

    public void Dispose()
    {
        Console.WriteLine("BLITTY is shutting down...");

        GC.SuppressFinalize(this);

        Content.Free();

        Graphics.Shutdown();

        FMODAudio.Unload();

        Platform.Shutdown();
    }

    public Game()
    {
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        _instance = this;

        InitGameLoopVariables();

        _config = ProcessGameConfig();

        Platform.Init(_config);
        Platform.OnQuit += Platform_OnQuit;
        Platform.WindowResized += Platform_WindowResized;
        Platform.Minimized += Platform_Minimized;
        Platform.Restored += Platform_Restored;

        Graphics.Init(_config.WindowWidth, _config.WindowHeight);

        Content.Init(_config);

        Graphics.LoadDefaultResources();

        Input.Init();

        if (_config.EnableAudio)
        {
            FMODAudio.Init();
        }

        GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
    }

    private void Platform_Restored()
    {
        _active = true;
    }

    private void Platform_Minimized()
    {
        _active = false;
    }

    public void Run(Scene? scene = null)
    {
        if (_running)
        {
            return;
        }

        _running = true;

        _current_scene = scene ?? new EmptyScene();

        _current_scene.Load();

        Tick(_current_scene);

        Platform.ShowWindow(true);

        _prev_frame_time = Platform.GetPerformanceCounter();
        _frame_accum = 0;

        while (_running)
        {
            Tick(_current_scene);
        }
    }

    public static void ToggleFullscreen()
    {
        Fullscreen = !Fullscreen;
    }

    public static void Exit()
    {
        _instance._running = false;
    }

    private void Platform_WindowResized((int width, int height) obj)
    {
        Graphics.SetBackbufferSize(obj.width, obj.height);
    }

    private void Platform_OnQuit()
    {
        _running = false;
    }

    private static GameConfig ProcessGameConfig()
    {
        bool CheckData(GameConfig config)
        {
            bool modified = false;

            if (config.Title == null)
            {
                config.Title = "BLIT GAME";
                modified = true;
            }

            if (config.WindowWidth <= 0)
            {
                config.WindowWidth = MinGameWidth;
                modified = true;
            }

            if (config.WindowHeight <= 0)
            {
                config.WindowHeight = MinGameHeight;
                modified = true;
            }

            return modified;
        }

        GameConfig CreateDefault()
        {
            return new GameConfig
            {
                Title = "BLITTY GAME",
                WindowWidth = 640,
                WindowHeight = 480,
                Fullscreen = false
            };
        }

        void Write(GameConfig config)
        {
            var new_json = JsonSerializer.Serialize(config);

            File.WriteAllText("config.json", new_json);
        }

        GameConfig config;

        if (File.Exists("config.json"))
        {
            try
            {
                config = Loader.LoadGameConfig();

                if (CheckData(config))
                {
                    Write(config);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        else
        {
            config = CreateDefault();

            Write(config);
        }

        return config;
    }

    private static void ShowExceptionMessage(Exception? ex)
    {
        Platform.ShowRuntimeError("BLITTY", $"An Unhandled Error Ocurred: {ex?.Message}");
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ShowExceptionMessage(e.ExceptionObject as Exception);
    }
}
