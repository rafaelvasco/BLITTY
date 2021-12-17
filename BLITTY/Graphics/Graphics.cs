using System.Runtime.CompilerServices;
using static BLITTY.Native.BGFX;

namespace BLITTY;

public enum GraphicsBackend
{
    Direct3D,
    OpenGL,
    Metal,
    Vulkan
}

public enum MSAALevel
{
    None,
    Two,
    Four,
    Eight,
    Sixteen
}

public static unsafe partial class Graphics
{
    private static int _width;

    private static int _height;

    private static bool _vSync = true;

    private static MSAALevel _mSAALevel = MSAALevel.None;

    private static readonly List<RenderResource> _renderResources = new(16);

    private static BGFX_StateFlags _renderState;

    private static BGFX_ResetFlags _graphicsFlags;

    private static bool _graphicsFlagsChanged = false;

    private static ushort _currentViewId;

    private static Texture2D? _drawTexture;

    private static Texture2D? _primitiveTexture;

    private static Shader? _defaultShader;

    private static Canvas2D? _canvas;

    internal static Texture2D PrimitiveTexture => _primitiveTexture!;

    public static GraphicsBackend GraphicsBackend { get; private set; }

    public static Shader DefaultShader => _defaultShader!;

    public static bool VSync
    {
        get => _vSync;
        set
        {
            if (_vSync != value)
            {
                _vSync = value;

                UpdateGraphicsFlags();
            }
        }
    }

    public static MSAALevel MSAALevel
    {
        get => _mSAALevel;
        set
        {
            if (_mSAALevel != value)
            {
                _mSAALevel = value;

                UpdateGraphicsFlags();
            }
        }
    }

    internal static void Init(int width, int height)
    {
        _width = width;
        _height = height;

        BGFX_PlatformData platformData = new()
        {
            nwh = Platform.GetRenderSurfaceHandle().ToPointer()
        };

        BGFX_SetPlatformData(&platformData);

        BGFX_InitData init = new();

        BGFX_InitCtor(&init);

        init.type = GetRendererType();

        switch (init.type)
        {
            case BGFX_RendererType.Direct3D11:
            case BGFX_RendererType.Direct3D12:
                GraphicsBackend = GraphicsBackend.Direct3D;
                break;
            case BGFX_RendererType.OpenGL:
                GraphicsBackend = GraphicsBackend.OpenGL;
                break;
            case BGFX_RendererType.Vulkan:
                GraphicsBackend = GraphicsBackend.Vulkan;
                break;
            case BGFX_RendererType.Metal:
                GraphicsBackend = GraphicsBackend.Metal;
                break;
        }

        init.vendorId = (ushort)BGFX_PciIdFlags.None;
        init.resolution.width = (uint)width;
        init.resolution.height = (uint)height;
        init.resolution.reset = (uint)BGFX_ResetFlags.None;
        init.resolution.format = BGFX_TextureFormat.BGRA8;

        BGFX_Init(&init);

#if DEBUG

        BGFX_SetDebug(BGFX_DebugFlags.Text);
        Console.WriteLine($"Graphics Initialized:");
        Console.WriteLine($"Backend: {GraphicsBackend}");

#endif

    }

    internal static void LoadDefaultResources()
    {
        _defaultShader = Content.Get<Shader>("BaseShader");

        if (_defaultShader == null)
        {
            throw new ApplicationException("Could not get BaseShader from Base Pak");
        }

        var pixmap = new Pixmap("", 1, 1);

        Blitter.Begin(pixmap);

        Blitter.SetColor(Color.White);
        Blitter.Fill();

        Blitter.End();

        _primitiveTexture = Content.CreateTexture2D("PrimitiveTexture", pixmap);
        _drawTexture = _primitiveTexture;
    }

    public static void SetBackbufferSize(int width, int height)
    {
        _width = width;
        _height = height;
        _graphicsFlagsChanged = true;
    }

    public static void ApplyRenderView(RenderView view)
    {
        _currentViewId = view.Id;
        BGFX_SetViewClear(view.Id, BGFX_ClearFlags.Color | BGFX_ClearFlags.Depth, view.ClearColor.Rgba, 1f, 0);
        BGFX_SetViewRect(view.Id, view._viewRect.X1, view._viewRect.Y1, view._viewRect.Width, view._viewRect.Height);
        BGFX_SetViewScissor(view.Id, (ushort)view._viewRect.X1, (ushort)view._viewRect.Y1, (ushort)view._viewRect.Width, (ushort)view._viewRect.Height);
        BGFX_SetViewTransform(view.Id, Unsafe.AsPointer(ref view._viewMatrix.M11), Unsafe.AsPointer(ref view._projMatrix.M11));
        BGFX_TouchView(view.Id);
    }

    public static Canvas2D GetCanvas2D(int maxQuads)
    {
        if (_canvas == null)
        {
            _canvas = new Canvas2D(maxQuads);
        }

        return _canvas;
    }

    public static void ApplyRenderState(RenderState state)
    {
        _renderState = state.State;
    }

    public static void SetTexture(Texture2D? texture = null)
    {
        _drawTexture = texture ?? _primitiveTexture;
    }

    public static void Draw(IMesh mesh, Shader? shader = null, PrimitiveType type = PrimitiveType.Triangles)
    {
        if (mesh.VertexCount == 0)
        {
            return;
        }

        shader ??= _defaultShader;

        var renderFlags = _renderState | (BGFX_StateFlags)type;

        shader!.SetTexture(0, _drawTexture!);

        shader.Apply();

        mesh.Submit();

        BGFX_SetRenderState(renderFlags, 0);

        BGFX_Submit(_currentViewId, shader.Handle, 0, BGFX_DiscardFlags.All);
    }

    public static void Frame()
    {
        BGFX_Frame();

        if (_graphicsFlagsChanged)
        {
            _graphicsFlagsChanged = false;
            BGFX_Reset(_width, _height, _graphicsFlags, BGFX_TextureFormat.BGRA8);
        }
    }

    internal static void Shutdown()
    {
        Console.WriteLine("Graphics Shutdown...");

        foreach (var resource in _renderResources)
        {
            Console.WriteLine($"Freeing {resource.Id}");

            resource.Dispose();
        }

        BGFX_Shutdown();
    }

    internal static void RegisterRenderResource(RenderResource resource)
    {
        _renderResources.Add(resource);
    }

    private static BGFX_RendererType GetRendererType()
    {
        return Platform.RunningPlatform switch
        {
            RunningPlatform.Windows => BGFX_RendererType.Direct3D12,
            RunningPlatform.Osx => BGFX_RendererType.Metal,
            RunningPlatform.Linux => BGFX_RendererType.OpenGL,
            _ => BGFX_RendererType.OpenGL,
        };
    }

    private static void UpdateGraphicsFlags()
    {
        _graphicsFlagsChanged = true;

        _graphicsFlags = BGFX_ResetFlags.None;

        if (_vSync)
        {
            _graphicsFlags |= BGFX_ResetFlags.Vsync;
        }


        if (_mSAALevel != MSAALevel.None)
        {
            switch (_mSAALevel)
            {
                case MSAALevel.Two:
                    _graphicsFlags |= BGFX_ResetFlags.MsaaX2;
                    break;
                case MSAALevel.Four:
                    _graphicsFlags |= BGFX_ResetFlags.MsaaX4;
                    break;
                case MSAALevel.Eight:
                    _graphicsFlags |= BGFX_ResetFlags.MsaaX8;
                    break;
                case MSAALevel.Sixteen:
                    _graphicsFlags |= BGFX_ResetFlags.MsaaX16;
                    break;
            }
        }
    }
}
