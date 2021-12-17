using static BLITTY.Native.SDL2;


namespace BLITTY;

internal static partial class Platform
{
    private static bool _supportsGlobalMouse;

    public static Action? MouseEnter;
    public static Action? MouseLeave;
    public static Action<MouseButton>? MouseUp;
    public static Action<MouseButton>? MouseDown;
    public static Action<int, int>? MouseMove;

    public static bool ButtonPosEventPoolEnabled { get; set; } = false;

    private static int _mWheelValue = 0;

    public static MouseState GetMouseState()
    {
        uint flags;
        int x, y;
        if (GetRelativeMouseMode())
        {
            flags = SDL_GetRelativeMouseState(out x, out y);
        }
        else if (_supportsGlobalMouse)
        {
            flags = SDL_GetGlobalMouseState(out x, out y);
            SDL_GetWindowPosition(WindowHandle, out int wx, out int wy);
            x -= wx;
            y -= wy;
        }
        else
        {
            flags = SDL_GetMouseState(out x, out y);
        }

        var left = (ButtonState)(flags & SDL_BUTTON_LMASK);
        var middle = (ButtonState)((flags & SDL_BUTTON_MMASK) >> 1);
        var right = (ButtonState)((flags & SDL_BUTTON_RMASK) >> 2);
        var x1 = (ButtonState)((flags & SDL_BUTTON_X1MASK) >> 3);
        var x2 = (ButtonState)((flags & SDL_BUTTON_X2MASK) >> 4);

        return new MouseState(x, y, _mWheelValue, left, middle, right, x1, x2);
    }

    public static void SetMousePosition(int x, int y)
    {
        SDL_WarpMouseInWindow(WindowHandle, x, y);
    }

    public static bool GetRelativeMouseMode()
    {
        return SDL_GetRelativeMouseMode() == true;
    }

    public static void SetRelativeMouseMode(bool enable)
    {
        _ = SDL_SetRelativeMouseMode(enable);
    }

    private static void ProcessMouseEvent(SDL_Event evt)
    {
        if (ButtonPosEventPoolEnabled)
        {
            var button = TranslatePlatformMouseButton(evt.button.button);

            switch (evt.type)
            {
                case SDL_EventType.SDL_MOUSEMOTION:
                    MouseMove?.Invoke(evt.motion.x, evt.motion.y);
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    MouseDown?.Invoke(button);
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    MouseUp?.Invoke(button);
                    break;
            }
        }

        if (evt.type == SDL_EventType.SDL_MOUSEWHEEL)
        {
            _mWheelValue = evt.wheel.y * 120;
        }
    }

    private static MouseButton TranslatePlatformMouseButton(byte button)
    {
        return button switch
        {
            1 => MouseButton.Left,
            2 => MouseButton.Middle,
            3 => MouseButton.Right,
            _ => MouseButton.None,
        };
    }

    private static void InitMouse()
    {
        _supportsGlobalMouse =
            RunningPlatform == RunningPlatform.Windows ||
            RunningPlatform == RunningPlatform.Osx ||
            RunningPlatform == RunningPlatform.Linux;
    }
}
