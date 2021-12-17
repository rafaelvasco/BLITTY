using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BLITTY.Native;

internal static unsafe partial class NativeOps
{
    /// <summary>
    ///     C equivalent of `malloc`; allocates a specified size of memory in bytes.
    /// </summary>
    /// <param name="byteCount">The numbers of bytes to allocate.</param>
    /// <returns>A pointer to the allocated memory.</returns>
    public static void* AllocateMemory(int byteCount)
    {
        var result = Marshal.AllocHGlobal(byteCount);
        return (void*)result;
    }

    /// <summary>
    ///     C equivalent of `free`; deallocates previously allocated memory.
    /// </summary>
    /// <param name="pointer">The pointer to the allocated memory.</param>
    public static void FreeMemory(void* pointer)
    {
        Marshal.FreeHGlobal((IntPtr)pointer);
    }

    /// <summary>
    ///     Reads a blittable value type of <see cref="Runtime.ReadMemory{T}" /> from the memory address.
    /// </summary>
    /// <param name="address">The memory address.</param>
    /// <typeparam name="T">The blittable value type.</typeparam>
    /// <returns>The read blittable value type <see cref="Runtime.ReadMemory{T}" /> from memory.</returns>
    public static T ReadMemory<T>(IntPtr address)
        where T : unmanaged
    {
        var source = (void*)address;
        var result = Unsafe.ReadUnaligned<T>(source);
        return result;
    }
}
