
using System.Numerics;

namespace BLITTY;

public class Canvas2D
{
    private DynamicMesh _quadsMesh;
    private Texture2D _currentTexture;
    private RenderView _defaultView;
    private Shader _shader;
    private RenderState _defaultRenderState;
    private bool _insideBeginBlock = false;


    public Canvas2D(int maxQuads)
    {
        _currentTexture = Graphics.PrimitiveTexture;
        _quadsMesh = new DynamicMesh("Canvas2D_QuadsMesh", maxQuads * 4, VertexPCT.VertexLayout);
        _quadsMesh.SetIndices(maxQuads);
        _defaultView = Graphics.CreateDefaultView();
        _defaultRenderState = RenderState.Default;
        _shader = Graphics.DefaultShader;
    }

    public void Begin(RenderView? view = null, RenderState? state = null)
    {
        if (_insideBeginBlock)
        {
            throw new ApplicationException("Canvas2D: Can't nest Begin calls");
        }

        Graphics.ApplyRenderView(view ?? _defaultView);
        Graphics.ApplyRenderState(state ?? _defaultRenderState);
        _insideBeginBlock = true;
        
    }

    public void DrawQuad(Texture2D texture, Quad quad, Vector2 position)
    {
        if (texture != _currentTexture)
        {
            Flush();
            SetTexture(texture);
        }

        quad.SetXY(position.X, position.Y, 0.5f, 0.5f);

        _quadsMesh.PushQuad(ref quad);
    }

    public void End()
    {
        if (!_insideBeginBlock)
        {
            throw new ApplicationException("Canvas2D: Calling End without Begin first.");
        }

        Flush();
        _insideBeginBlock = false;
    }

    private void SetTexture(Texture2D texture)
    {
        Graphics.SetTexture(texture);
        _currentTexture = texture;
    }

    private void Flush()
    {
        Graphics.Draw(_quadsMesh, _shader);
        _quadsMesh.Reset();
    }
}
