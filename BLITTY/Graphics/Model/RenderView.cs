using System.Numerics;

namespace BLITTY;

public struct RenderView
{
    private static ushort _staticId = 0;

    public Matrix4x4 ViewMatrix => _viewMatrix;
    public Matrix4x4 ProjectionMatrix => _projMatrix;
    public Color ClearColor => _clearColor;
    public Rect ViewRect => _viewRect;

    internal ushort Id { get; private set; }


    internal Matrix4x4 _viewMatrix;
    internal Matrix4x4 _projMatrix;
    internal uint _clearColor;
    internal Rect _viewRect;

    public RenderView()
    {
        Id = _staticId++;
        _viewMatrix = Matrix4x4.Identity;
        _projMatrix = Matrix4x4.Identity;
        _clearColor = 0;
        _viewRect = Rect.Empty;
    }

    public void SetViewport(int x, int y, int w, int h)
    {
        _viewRect = Rect.FromBox(x, y, w, h);
    }

    public void SetBackColor(Color color)
    {
        _clearColor = color;
    }

    public void SetTransform(Matrix4x4 matrix)
    {
        _viewMatrix = matrix;
    }

    public void SetProjection(Matrix4x4 matrix)
    {
        _projMatrix = matrix;
    }
}
