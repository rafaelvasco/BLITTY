using System.Runtime.InteropServices;

namespace BLITTY.Audio;

internal class PointerLinker<T> where T : class
{
    private readonly Dictionary<ulong, T> _items = new Dictionary<ulong, T>();

    private ulong _currentIndex = 0;

    public IntPtr Add(T item)
    {
        _items.Add(_currentIndex, item);

        var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(_currentIndex));
        Marshal.StructureToPtr(_currentIndex, ptr, false);
        _currentIndex += 1;

        return ptr;
    }

    public T? Get(IntPtr ptr)
    {
        try
        {
            var index = (ulong)Marshal.PtrToStructure(ptr, typeof(ulong));
            return _items[index];
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void Remove(IntPtr ptr)
    {
        var index = (ulong)Marshal.PtrToStructure(ptr, typeof(ulong));
        _items.Remove(index);
        Marshal.FreeHGlobal(ptr);
    }
}
