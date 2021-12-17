using System.Runtime.InteropServices;
using static BLITTY.Native.SDL2;

namespace BLITTY;

public enum RunningPlatform
{
    Windows,
    Osx,
    Linux,
    Unknown
}

internal static partial class Platform
{
    public static event Action? OnQuit;


    public static RunningPlatform RunningPlatform
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return RunningPlatform.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return RunningPlatform.Osx;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return RunningPlatform.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                return RunningPlatform.Linux;
            }
            else
            {
                return RunningPlatform.Unknown;
            }
        }
    }

    public static void Init(GameConfig config)
    {
        Ensure64BitArchitecture();

#if DEBUG

        SDL_SetHint("SDL_WINDOWS_DISABLE_THREAD_NAMING", "1");

#endif

        if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_JOYSTICK | SDL_INIT_GAMECONTROLLER | SDL_INIT_HAPTIC) < 0)
        {
            SDL_Quit();
            throw new ApplicationException("Failed to initialize SDL");
        }

        CreateWindow(config.Title!, config.WindowWidth, config.WindowHeight, config.Fullscreen);
    }

    public static void ProcessEvents()
    {
        while (SDL_PollEvent(out var ev) == 1)
        {
            switch (ev.type)
            {
                case SDL_EventType.SDL_QUIT:
                    OnQuit?.Invoke();
                    break;

                case SDL_EventType.SDL_KEYDOWN:
                case SDL_EventType.SDL_KEYUP:
                    ProcessKeyEvent(ev);
                    break;

                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                case SDL_EventType.SDL_MOUSEMOTION:
                    //ProcessMouseEvent(ev);
                    break;

                case SDL_EventType.SDL_WINDOWEVENT:
                    ProcessWindowEvent(ev);
                    break;

                case SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                    break;

                case SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                    break;
            }
        }
    }

    public static ulong GetPerformanceCounter()
    {
        return SDL_GetPerformanceCounter();
    }

    public static ulong GetPerformanceFrequency()
    {
        return SDL_GetPerformanceFrequency();
    }

    public static void ShowRuntimeError(string title, string message)
    {
        _ = SDL_ShowSimpleMessageBox(
            SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR,
            title ?? "",
            message ?? "",
            IntPtr.Zero
        );
    }

    public static void Shutdown()
    {
        Console.WriteLine("Platform Shutdown...");

        DestroyWindow();
        SDL_Quit();
    }

    private static void Ensure64BitArchitecture()
    {
        var runtime_architecture = RuntimeInformation.OSArchitecture;
        if (runtime_architecture is Architecture.Arm or Architecture.X86)
        {
            throw new NotSupportedException("32-bit architecture is not supported.");
        }
    }
}
