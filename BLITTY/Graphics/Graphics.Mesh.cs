namespace BLITTY;

public static unsafe partial class Graphics
{
    public static DynamicMesh CreateDynamicMesh(
        string id,
        int vertexCount,
        VertexLayout layout)
    {
        var mesh = new DynamicMesh(id, vertexCount, layout);

        RegisterRenderResource(mesh);

        return mesh;
    }
}
