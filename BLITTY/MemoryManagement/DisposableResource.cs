namespace BLITTY.MemoryManagement;

public class DisposableResource : IDisposable
{
    public bool Disposed { get; private set; }

    public event EventHandler? Disposing;

    ~DisposableResource()
    {
        Dispose(false);
    }

    protected virtual void FreeManaged()
    {
    }

    protected virtual void FreeUnmanaged()
    {
    }

    private void Dispose(bool disposing)
    {
        EnsureNotDisposed();

        if (disposing)
        {
            FreeManaged();
        }

        FreeUnmanaged();
        Disposed = true;
    }

    public void Dispose()
    {
        Disposing?.Invoke(this, EventArgs.Empty);
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected void EnsureNotDisposed()
    {
        if (Disposed)
            throw new ObjectDisposedException(null, "This object has already been disposed.");
    }
}
