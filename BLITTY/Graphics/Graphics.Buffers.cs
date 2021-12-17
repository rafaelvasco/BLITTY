using System.Runtime.CompilerServices;
using static BLITTY.Native.BGFX;

namespace BLITTY;

public static unsafe partial class Graphics
{
    public static VertexBuffer CreateVertexBuffer(string id, Span<VertexPCT> vertices, VertexLayout layout)
    {
        var memory = BGFX_GetMemoryBufferReference(vertices);
        var handle = BGFX_CreateVertexBuffer(memory, &layout.Handle, BGFX_BufferFlags.None);

        var vertexBuffer = new VertexBuffer(id, handle);

        RegisterRenderResource(vertexBuffer);

        return vertexBuffer;
    }

    public static void DestroyVertexBuffer(VertexBuffer buffer)
    {
        if (buffer.Handle.Valid)
        {
            BGFX_DestroyVertexBuffer(buffer.Handle);
        }
    }

    public static IndexBuffer CreateIndexBuffer(string id, Span<ushort> indices)
    {
        var memory = BGFX_GetMemoryBufferReference(indices);
        var handle = BGFX_CreateIndexBuffer(memory, BGFX_BufferFlags.None);

        var indexBuffer = new IndexBuffer(id, handle);

        RegisterRenderResource(indexBuffer);

        return indexBuffer;
    }

    public static void DestroyIndexBuffer(IndexBuffer buffer)
    {
        if (buffer.Handle.Valid)
        {
            BGFX_DestroyIndexBuffer(buffer.Handle);
        }
    }

    public static DynamicVertexBuffer CreateDynamicVertexBuffer(string id, int vertexCount, VertexLayout layout)
    {
        var handle = BGFX_CreateDynamicVertexBuffer((uint)vertexCount, &layout.Handle, BGFX_BufferFlags.None);

        var buffer = new DynamicVertexBuffer(id, handle);

        RegisterRenderResource(buffer);

        return buffer;
    }

    public static DynamicVertexBuffer CreateDynamicVertexBuffer(string id, Span<VertexPCT> vertices, VertexLayout layout)
    {
        var memory = BGFX_GetMemoryBufferReference(vertices);

        var handle = BGFX_CreateDynamicVertexBufferFrom(memory, &layout.Handle, BGFX_BufferFlags.None);

        var buffer = new DynamicVertexBuffer(id, handle);

        RegisterRenderResource(buffer);

        return buffer;
    }

    public static void UpdateDynamicVertexBuffer(DynamicVertexBuffer buffer, int startVertex, Span<VertexPCT> vertices)
    {
        var memory = BGFX_GetMemoryBufferReference(vertices);

        BGFX_UpdateDynamicVertexBuffer(buffer.Handle, (uint)startVertex, memory);
    }

    public static void DestroyDynamicVertexBuffer(DynamicVertexBuffer buffer)
    {
        if (buffer.Handle.Valid)
        {
            BGFX_DestroyDynamicVertexBuffer(buffer.Handle);
        }
    }

    public static DynamicIndexBuffer CreateDynamicIndexBuffer(string id, int indexCount)
    {
        var handle = BGFX_CreateDynamicIndexBuffer((uint)indexCount, BGFX_BufferFlags.AllowResize);

        var buffer = new DynamicIndexBuffer(id, handle);

        RegisterRenderResource(buffer);

        return buffer;
    }

    public static DynamicIndexBuffer CreateDynamicIndexBuffer(string id, Span<ushort> indices)
    {
        var memory = BGFX_GetMemoryBufferReference(indices);

        var handle = BGFX_CreateDynamicIndexBufferFrom(memory, BGFX_BufferFlags.None);

        var buffer = new DynamicIndexBuffer(id, handle);

        RegisterRenderResource(buffer);

        return buffer;
    }

    public static void UpdateDynamicIndexBuffer(DynamicIndexBuffer buffer, int startIndex, Span<ushort> indices)
    {
        var memory = BGFX_GetMemoryBufferReference(indices);

        BGFX_UpdateDynamicIndexBuffer(buffer.Handle, (uint)startIndex, memory);
    }

    public static void DestroyDynamicIndexBuffer(DynamicIndexBuffer buffer)
    {
        if (buffer.Handle.Valid)
        {
            BGFX_DestroyDynamicIndexBuffer(buffer.Handle);
        }
    }

    public static TransientVertexBuffer CreateTransientVertexBuffer(Span<VertexPCT> vertices, VertexLayout layout)
    {
        var handle = new BGFX_TransientVertexBuffer();

        BGFX_AllocTransientVertexBuffer(&handle, (uint)vertices.Length, &layout.Handle);

        var dataSize = vertices.Length * VertexPCT.Stride;

        Unsafe.CopyBlockUnaligned(handle.data, Unsafe.AsPointer(ref vertices[0]), (uint)dataSize);

        return new TransientVertexBuffer(handle);
    }

    public static void SetIndexBuffer(IndexBuffer buffer, int firstIndex, int numIndices)
    {
        BGFX_SetIndexBuffer(buffer.Handle, (uint)firstIndex, (uint)numIndices);
    }

    public static void SetDynamicIndexBuffer(DynamicIndexBuffer buffer, int firstIndex, int numIndices)
    {
        BGFX_SetDynamicIndexBuffer(buffer.Handle, (uint)firstIndex, (uint)numIndices);
    }

    public static void SetVertexBuffer(VertexBuffer buffer, int firstIndex, int numVertices)
    {
        BGFX_SetVertexBuffer(0, buffer.Handle, (uint)firstIndex, (uint)numVertices);
    }

    public static void SetDynamicVertexBuffer(DynamicVertexBuffer buffer, int firstIndex, int numVertices)
    {
        BGFX_SetDynamicVertexBuffer(0, buffer.Handle, (uint)firstIndex, (uint)numVertices);
    }

    public static void SetTransientVertexBuffer(TransientVertexBuffer buffer, int numVertices)
    {
        BGFX_SetTransientVertexBuffer(0, &buffer.Handle, 0, (uint)numVertices);
    }
}
