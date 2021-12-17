using System.Text;
using static BLITTY.Native.SDL2;

namespace BLITTY;

internal static partial class Platform
{
    public static bool UseScanCodeKeyDetection { get; set; } = true;

    public static Action<Keys>? KeyDown;
    public static Action<Keys>? KeyUp;
    public static Action<char>? CharInput;

    private static readonly List<Keys> _knownKeys = new(10);

    private static readonly Dictionary<int, Keys> KeycodeMap = new()
    {
        { (int)SDL_Keycode.SDLK_a, Keys.A },
        { (int)SDL_Keycode.SDLK_b, Keys.B },
        { (int)SDL_Keycode.SDLK_c, Keys.C },
        { (int)SDL_Keycode.SDLK_d, Keys.D },
        { (int)SDL_Keycode.SDLK_e, Keys.E },
        { (int)SDL_Keycode.SDLK_f, Keys.F },
        { (int)SDL_Keycode.SDLK_g, Keys.G },
        { (int)SDL_Keycode.SDLK_h, Keys.H },
        { (int)SDL_Keycode.SDLK_i, Keys.I },
        { (int)SDL_Keycode.SDLK_j, Keys.J },
        { (int)SDL_Keycode.SDLK_k, Keys.K },
        { (int)SDL_Keycode.SDLK_l, Keys.L },
        { (int)SDL_Keycode.SDLK_m, Keys.M },
        { (int)SDL_Keycode.SDLK_n, Keys.N },
        { (int)SDL_Keycode.SDLK_o, Keys.O },
        { (int)SDL_Keycode.SDLK_p, Keys.P },
        { (int)SDL_Keycode.SDLK_q, Keys.Q },
        { (int)SDL_Keycode.SDLK_r, Keys.R },
        { (int)SDL_Keycode.SDLK_s, Keys.S },
        { (int)SDL_Keycode.SDLK_t, Keys.T },
        { (int)SDL_Keycode.SDLK_u, Keys.U },
        { (int)SDL_Keycode.SDLK_v, Keys.V },
        { (int)SDL_Keycode.SDLK_w, Keys.W },
        { (int)SDL_Keycode.SDLK_x, Keys.X },
        { (int)SDL_Keycode.SDLK_y, Keys.Y },
        { (int)SDL_Keycode.SDLK_z, Keys.Z },
        { (int)SDL_Keycode.SDLK_0, Keys.D0 },
        { (int)SDL_Keycode.SDLK_1, Keys.D1 },
        { (int)SDL_Keycode.SDLK_2, Keys.D2 },
        { (int)SDL_Keycode.SDLK_3, Keys.D3 },
        { (int)SDL_Keycode.SDLK_4, Keys.D4 },
        { (int)SDL_Keycode.SDLK_5, Keys.D5 },
        { (int)SDL_Keycode.SDLK_6, Keys.D6 },
        { (int)SDL_Keycode.SDLK_7, Keys.D7 },
        { (int)SDL_Keycode.SDLK_8, Keys.D8 },
        { (int)SDL_Keycode.SDLK_9, Keys.D9 },
        { (int)SDL_Keycode.SDLK_KP_0, Keys.NumPad0 },
        { (int)SDL_Keycode.SDLK_KP_1, Keys.NumPad1 },
        { (int)SDL_Keycode.SDLK_KP_2, Keys.NumPad2 },
        { (int)SDL_Keycode.SDLK_KP_3, Keys.NumPad3 },
        { (int)SDL_Keycode.SDLK_KP_4, Keys.NumPad4 },
        { (int)SDL_Keycode.SDLK_KP_5, Keys.NumPad5 },
        { (int)SDL_Keycode.SDLK_KP_6, Keys.NumPad6 },
        { (int)SDL_Keycode.SDLK_KP_7, Keys.NumPad7 },
        { (int)SDL_Keycode.SDLK_KP_8, Keys.NumPad8 },
        { (int)SDL_Keycode.SDLK_KP_9, Keys.NumPad9 },
        { (int)SDL_Keycode.SDLK_KP_DECIMAL, Keys.Decimal },
        { (int)SDL_Keycode.SDLK_KP_DIVIDE, Keys.Divide },
        { (int)SDL_Keycode.SDLK_KP_ENTER, Keys.Enter },
        { (int)SDL_Keycode.SDLK_KP_MINUS, Keys.Subtract },
        { (int)SDL_Keycode.SDLK_KP_MULTIPLY, Keys.Multiply },
        { (int)SDL_Keycode.SDLK_KP_PLUS, Keys.Add },
        { (int)SDL_Keycode.SDLK_F1, Keys.F1 },
        { (int)SDL_Keycode.SDLK_F2, Keys.F2 },
        { (int)SDL_Keycode.SDLK_F3, Keys.F3 },
        { (int)SDL_Keycode.SDLK_F4, Keys.F4 },
        { (int)SDL_Keycode.SDLK_F5, Keys.F5 },
        { (int)SDL_Keycode.SDLK_F6, Keys.F6 },
        { (int)SDL_Keycode.SDLK_F7, Keys.F7 },
        { (int)SDL_Keycode.SDLK_F8, Keys.F8 },
        { (int)SDL_Keycode.SDLK_F9, Keys.F9 },
        { (int)SDL_Keycode.SDLK_F10, Keys.F10 },
        { (int)SDL_Keycode.SDLK_F11, Keys.F11 },
        { (int)SDL_Keycode.SDLK_F12, Keys.F12 },
        { (int)SDL_Keycode.SDLK_F13, Keys.F13 },
        { (int)SDL_Keycode.SDLK_F14, Keys.F14 },
        { (int)SDL_Keycode.SDLK_F15, Keys.F15 },
        { (int)SDL_Keycode.SDLK_F16, Keys.F16 },
        { (int)SDL_Keycode.SDLK_F17, Keys.F17 },
        { (int)SDL_Keycode.SDLK_F18, Keys.F18 },
        { (int)SDL_Keycode.SDLK_F19, Keys.F19 },
        { (int)SDL_Keycode.SDLK_F20, Keys.F20 },
        { (int)SDL_Keycode.SDLK_F21, Keys.F21 },
        { (int)SDL_Keycode.SDLK_F22, Keys.F22 },
        { (int)SDL_Keycode.SDLK_F23, Keys.F23 },
        { (int)SDL_Keycode.SDLK_F24, Keys.F24 },
        { (int)SDL_Keycode.SDLK_SPACE, Keys.Space },
        { (int)SDL_Keycode.SDLK_UP, Keys.Up },
        { (int)SDL_Keycode.SDLK_DOWN, Keys.Down },
        { (int)SDL_Keycode.SDLK_LEFT, Keys.Left },
        { (int)SDL_Keycode.SDLK_RIGHT, Keys.Right },
        { (int)SDL_Keycode.SDLK_LALT, Keys.LeftAlt },
        { (int)SDL_Keycode.SDLK_RALT, Keys.RightAlt },
        { (int)SDL_Keycode.SDLK_LCTRL, Keys.LeftControl },
        { (int)SDL_Keycode.SDLK_RCTRL, Keys.RightControl },
        { (int)SDL_Keycode.SDLK_LSHIFT, Keys.LeftShift },
        { (int)SDL_Keycode.SDLK_RSHIFT, Keys.RightShift },
        { (int)SDL_Keycode.SDLK_CAPSLOCK, Keys.CapsLock },
        { (int)SDL_Keycode.SDLK_DELETE, Keys.Delete },
        { (int)SDL_Keycode.SDLK_END, Keys.End },
        { (int)SDL_Keycode.SDLK_BACKSPACE, Keys.Back },
        { (int)SDL_Keycode.SDLK_RETURN, Keys.Enter },
        { (int)SDL_Keycode.SDLK_ESCAPE, Keys.Escape },
        { (int)SDL_Keycode.SDLK_HOME, Keys.Home },
        { (int)SDL_Keycode.SDLK_PAGEUP, Keys.PageUp },
        { (int)SDL_Keycode.SDLK_PAGEDOWN, Keys.PageDown },
        { (int)SDL_Keycode.SDLK_PAUSE, Keys.Pause },
        { (int)SDL_Keycode.SDLK_TAB, Keys.Tab },
        { (int)SDL_Keycode.SDLK_VOLUMEUP, Keys.VolumeUp },
        { (int)SDL_Keycode.SDLK_VOLUMEDOWN, Keys.VolumeDown },
        { (int)SDL_Keycode.SDLK_UNKNOWN, Keys.None }
    };
    private static readonly Dictionary<int, Keys> KeyscanMap = new()
    {
        { (int)SDL_Scancode.SDL_SCANCODE_A, Keys.A },
        { (int)SDL_Scancode.SDL_SCANCODE_B, Keys.B },
        { (int)SDL_Scancode.SDL_SCANCODE_C, Keys.C },
        { (int)SDL_Scancode.SDL_SCANCODE_D, Keys.D },
        { (int)SDL_Scancode.SDL_SCANCODE_E, Keys.E },
        { (int)SDL_Scancode.SDL_SCANCODE_F, Keys.F },
        { (int)SDL_Scancode.SDL_SCANCODE_G, Keys.G },
        { (int)SDL_Scancode.SDL_SCANCODE_H, Keys.H },
        { (int)SDL_Scancode.SDL_SCANCODE_I, Keys.I },
        { (int)SDL_Scancode.SDL_SCANCODE_J, Keys.J },
        { (int)SDL_Scancode.SDL_SCANCODE_K, Keys.K },
        { (int)SDL_Scancode.SDL_SCANCODE_L, Keys.L },
        { (int)SDL_Scancode.SDL_SCANCODE_M, Keys.M },
        { (int)SDL_Scancode.SDL_SCANCODE_N, Keys.N },
        { (int)SDL_Scancode.SDL_SCANCODE_O, Keys.O },
        { (int)SDL_Scancode.SDL_SCANCODE_P, Keys.P },
        { (int)SDL_Scancode.SDL_SCANCODE_Q, Keys.Q },
        { (int)SDL_Scancode.SDL_SCANCODE_R, Keys.R },
        { (int)SDL_Scancode.SDL_SCANCODE_S, Keys.S },
        { (int)SDL_Scancode.SDL_SCANCODE_T, Keys.T },
        { (int)SDL_Scancode.SDL_SCANCODE_U, Keys.U },
        { (int)SDL_Scancode.SDL_SCANCODE_V, Keys.V },
        { (int)SDL_Scancode.SDL_SCANCODE_W, Keys.W },
        { (int)SDL_Scancode.SDL_SCANCODE_X, Keys.X },
        { (int)SDL_Scancode.SDL_SCANCODE_Y, Keys.Y },
        { (int)SDL_Scancode.SDL_SCANCODE_Z, Keys.Z },
        { (int)SDL_Scancode.SDL_SCANCODE_0, Keys.D0 },
        { (int)SDL_Scancode.SDL_SCANCODE_1, Keys.D1 },
        { (int)SDL_Scancode.SDL_SCANCODE_2, Keys.D2 },
        { (int)SDL_Scancode.SDL_SCANCODE_3, Keys.D3 },
        { (int)SDL_Scancode.SDL_SCANCODE_4, Keys.D4 },
        { (int)SDL_Scancode.SDL_SCANCODE_5, Keys.D5 },
        { (int)SDL_Scancode.SDL_SCANCODE_6, Keys.D6 },
        { (int)SDL_Scancode.SDL_SCANCODE_7, Keys.D7 },
        { (int)SDL_Scancode.SDL_SCANCODE_8, Keys.D8 },
        { (int)SDL_Scancode.SDL_SCANCODE_9, Keys.D9 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_0, Keys.NumPad0 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_1, Keys.NumPad1 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_2, Keys.NumPad2 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_3, Keys.NumPad3 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_4, Keys.NumPad4 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_5, Keys.NumPad5 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_6, Keys.NumPad6 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_7, Keys.NumPad7 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_8, Keys.NumPad8 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_9, Keys.NumPad9 },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_DECIMAL, Keys.Decimal },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_DIVIDE, Keys.Divide },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_ENTER, Keys.Enter },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_MINUS, Keys.Subtract },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY, Keys.Multiply },
        { (int)SDL_Scancode.SDL_SCANCODE_KP_PLUS, Keys.Add },
        { (int)SDL_Scancode.SDL_SCANCODE_F1, Keys.F1 },
        { (int)SDL_Scancode.SDL_SCANCODE_F2, Keys.F2 },
        { (int)SDL_Scancode.SDL_SCANCODE_F3, Keys.F3 },
        { (int)SDL_Scancode.SDL_SCANCODE_F4, Keys.F4 },
        { (int)SDL_Scancode.SDL_SCANCODE_F5, Keys.F5 },
        { (int)SDL_Scancode.SDL_SCANCODE_F6, Keys.F6 },
        { (int)SDL_Scancode.SDL_SCANCODE_F7, Keys.F7 },
        { (int)SDL_Scancode.SDL_SCANCODE_F8, Keys.F8 },
        { (int)SDL_Scancode.SDL_SCANCODE_F9, Keys.F9 },
        { (int)SDL_Scancode.SDL_SCANCODE_F10, Keys.F10 },
        { (int)SDL_Scancode.SDL_SCANCODE_F11, Keys.F11 },
        { (int)SDL_Scancode.SDL_SCANCODE_F12, Keys.F12 },
        { (int)SDL_Scancode.SDL_SCANCODE_F13, Keys.F13 },
        { (int)SDL_Scancode.SDL_SCANCODE_F14, Keys.F14 },
        { (int)SDL_Scancode.SDL_SCANCODE_F15, Keys.F15 },
        { (int)SDL_Scancode.SDL_SCANCODE_F16, Keys.F16 },
        { (int)SDL_Scancode.SDL_SCANCODE_F17, Keys.F17 },
        { (int)SDL_Scancode.SDL_SCANCODE_F18, Keys.F18 },
        { (int)SDL_Scancode.SDL_SCANCODE_F19, Keys.F19 },
        { (int)SDL_Scancode.SDL_SCANCODE_F20, Keys.F20 },
        { (int)SDL_Scancode.SDL_SCANCODE_F21, Keys.F21 },
        { (int)SDL_Scancode.SDL_SCANCODE_F22, Keys.F22 },
        { (int)SDL_Scancode.SDL_SCANCODE_F23, Keys.F23 },
        { (int)SDL_Scancode.SDL_SCANCODE_F24, Keys.F24 },
        { (int)SDL_Scancode.SDL_SCANCODE_SPACE, Keys.Space },
        { (int)SDL_Scancode.SDL_SCANCODE_UP, Keys.Up },
        { (int)SDL_Scancode.SDL_SCANCODE_DOWN, Keys.Down },
        { (int)SDL_Scancode.SDL_SCANCODE_LEFT, Keys.Left },
        { (int)SDL_Scancode.SDL_SCANCODE_RIGHT, Keys.Right },
        { (int)SDL_Scancode.SDL_SCANCODE_LALT, Keys.LeftAlt },
        { (int)SDL_Scancode.SDL_SCANCODE_RALT, Keys.RightAlt },
        { (int)SDL_Scancode.SDL_SCANCODE_LCTRL, Keys.LeftControl },
        { (int)SDL_Scancode.SDL_SCANCODE_RCTRL, Keys.RightControl },
        { (int)SDL_Scancode.SDL_SCANCODE_LSHIFT, Keys.LeftShift },
        { (int)SDL_Scancode.SDL_SCANCODE_RSHIFT, Keys.RightShift },
        { (int)SDL_Scancode.SDL_SCANCODE_CAPSLOCK, Keys.CapsLock },
        { (int)SDL_Scancode.SDL_SCANCODE_DELETE, Keys.Delete },
        { (int)SDL_Scancode.SDL_SCANCODE_END, Keys.End },
        { (int)SDL_Scancode.SDL_SCANCODE_BACKSPACE, Keys.Back },
        { (int)SDL_Scancode.SDL_SCANCODE_RETURN, Keys.Enter },
        { (int)SDL_Scancode.SDL_SCANCODE_ESCAPE, Keys.Escape },
        { (int)SDL_Scancode.SDL_SCANCODE_HOME, Keys.Home },
        { (int)SDL_Scancode.SDL_SCANCODE_PAGEUP, Keys.PageUp },
        { (int)SDL_Scancode.SDL_SCANCODE_PAGEDOWN, Keys.PageDown },
        { (int)SDL_Scancode.SDL_SCANCODE_PAUSE, Keys.Pause },
        { (int)SDL_Scancode.SDL_SCANCODE_TAB, Keys.Tab },
        { (int)SDL_Scancode.SDL_SCANCODE_VOLUMEUP, Keys.VolumeUp },
        { (int)SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN, Keys.VolumeDown },
        { (int)SDL_Scancode.SDL_SCANCODE_UNKNOWN, Keys.None },
    };

    public static KeyboardState GetKeyboardState()
    {
        return new KeyboardState(_knownKeys);
    }

    public static void StartTextInput()
    {
        SDL_StartTextInput();
    }

    public static void StopTextInput()
    {
        SDL_StopTextInput();
    }

    private static Keys ConvertKey(ref SDL_Keysym key)
    {
        if (UseScanCodeKeyDetection)
        {
            if (KeyscanMap.TryGetValue((int)key.scancode, out Keys retVal))
            {
                return retVal;
            }
        }
        else
        {
            if (KeycodeMap.TryGetValue((int)key.sym, out Keys retVal))
            {
                return retVal;
            }
        }

        return Keys.None;
    }

    private static void ProcessKeyEvent(SDL_Event evt)
    {
        if (evt.type == SDL_EventType.SDL_KEYDOWN)
        {
            Keys key = ConvertKey(ref evt.key.keysym);

            KeyDown?.Invoke(key);

            if (!_knownKeys.Contains(key))
            {
                _knownKeys.Add(key);
            }
        }
        else if (evt.type == SDL_EventType.SDL_KEYUP)
        {
            Keys key = ConvertKey(ref evt.key.keysym);

            KeyUp?.Invoke(key);

            _knownKeys.Remove(key);
        }
    }

    private static void ProcessTextInputEvent(SDL_Event evt)
    {
        if (evt.type == SDL_EventType.SDL_TEXTINPUT)
        {
            unsafe
            {
                int bytes = MeasureStringLength(evt.text.text);
                if (bytes > 0)
                {
                    char* chars_buffer = stackalloc char[bytes];
                    int chars = Encoding.UTF8.GetChars(
                        evt.text.text,
                        bytes,
                        chars_buffer,
                        bytes
                    );

                    for (int i = 0; i < chars; i += 1)
                    {
                        CharInput?.Invoke(chars_buffer[i]);
                    }
                }
            }
        }
    }

    private unsafe static int MeasureStringLength(byte* ptr)
    {
        int bytes;
        for (bytes = 0; *ptr != 0; ptr += 1, ++bytes) ;
        return bytes;
    }
}
