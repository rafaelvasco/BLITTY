using System.Numerics;

namespace BLITTY;

public static unsafe partial class Graphics
{
    public static RenderView CreateDefaultView()
    {
        var defaultView = new RenderView();

        defaultView.SetBackColor(Color.Black);
        defaultView.SetViewport(0, 0, _width, _height);
        defaultView.SetProjection(Matrix4x4.CreateOrthographicOffCenter(0f, _width, _height, 0f, -1000.0f, 1000.0f));
        defaultView.SetTransform(Matrix4x4.Identity);

        return defaultView;
    }
}
