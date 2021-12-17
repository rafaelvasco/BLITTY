using BLITTY;

namespace BLITTY_Demos;

internal class TriangleDemo : Scene
{
    private StaticMesh? _mesh;
    private RenderView _view;


    public override void Load()
    {

        _mesh = new StaticMesh("triangleMesh");

        _mesh.SetVertices(new VertexPCT[] {
            new VertexPCT(Game.WindowSize.Width / 2, 0f, 0f, Color.Red),
            new VertexPCT(0, Game.WindowSize.Height, 0f, Color.Green),
            new VertexPCT(Game.WindowSize.Width, Game.WindowSize.Height, 0f, Color.Blue)
        }, VertexPCT.VertexLayout);

        _view = Graphics.CreateDefaultView();

        Graphics.MSAALevel = MSAALevel.Four;
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

            Graphics.Draw(_mesh);
        }
    }
}
