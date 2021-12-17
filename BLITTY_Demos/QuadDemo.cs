using BLITTY;

namespace BLITTY_Demos;

internal class QuadDemo : Scene
{
    private StaticMesh? _mesh;
    private RenderView _view;


    public override void Load()
    {

        _mesh = new StaticMesh("quadMesh");

        _mesh.SetIndices(new ushort[] { 0, 1, 2, 0, 2, 3});

        var x0 = Game.WindowSize.Width / 2 - 128;
        var y0 = Game.WindowSize.Height / 2 - 128;
        var x1 = x0 + 256;
        var y1 = y0 + 256;

        _mesh.SetVertices(new VertexPCT[] { 
            new VertexPCT(x0, y0, 0f, Color.Red),
            new VertexPCT(x1, y0, 0f, Color.Green),
            new VertexPCT(x1, y1, 0f, Color.Blue),
            new VertexPCT(x0, y1, 0f, Color.Yellow),
        }, 
        VertexPCT.VertexLayout);

        _view = Graphics.CreateDefaultView();
    }

    public override void Unload()
    {
    }

    public override void FixedUpdate(float dt)
    {
    }

    public override void Update(float dt)
    {
        if (Input.KeyPressed(Keys.Escape))
        {
            Game.Exit();
        }
    }

    public override void Draw()
    {
        if (_mesh != null)
        {
            Graphics.ApplyRenderView(_view);
            Graphics.ApplyRenderState(RenderState.Default);

            Graphics.Draw(_mesh, Graphics.DefaultShader!, PrimitiveType.Triangles);
        }
    }
}
