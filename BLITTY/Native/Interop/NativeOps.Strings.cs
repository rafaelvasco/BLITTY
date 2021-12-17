using System.Runtime.InteropServices;

namespace BLITTY.Native;

internal static unsafe partial class NativeOps
{
    private static readonly Dictionary<uint, CString8U> StringHashesToPointers8U = new();
    private static readonly Dictionary<IntPtr, string> PointersToStrings8U = new();
    private static readonly Dictionary<uint, CString16U> StringHashesToPointers16U = new();
    private static readonly Dictionary<IntPtr, string> PointersToStrings16U = new();

    /// <summary>
    ///     Converts a <see cref="string" /> from a C style string of type `char` (one dimensional byte array
    ///     terminated by a <c>0x0</c>) by allocating and copying.
    /// </summary>
    /// <param name="ptr">A pointer to the C string.</param>
    /// <returns>A <see cref="string" /> equivalent of <paramref name="ptr" />.</returns>
    public static string String8U(CString8U ptr)
    {
        if (ptr.IsNull)
        {
            return string.Empty;
        }

        if (PointersToStrings8U.TryGetValue(ptr, out var result))
        {
            return result;
        }

        var hash = djb2((byte*)ptr._ptr);
        if (StringHashesToPointers8U.TryGetValue(hash, out var pointer2))
        {
            result = PointersToStrings8U[pointer2];
            return result;
        }

        // calls ASM/C/C++ functions to calculate length and then "FastAllocate" the string with the GC
        // https://mattwarren.org/2016/05/31/Strings-and-the-CLR-a-Special-Relationship/
        result = Marshal.PtrToStringAnsi(ptr);

        if (string.IsNullOrEmpty(result))
        {
            return string.Empty;
        }

        StringHashesToPointers8U.Add(hash, ptr);
        PointersToStrings8U.Add(ptr, result);

        return result;
    }

    /// <summary>
    ///     Converts a <see cref="string" /> from a C style string of type `wchar_t` (one dimensional ushort array
    ///     terminated by a <c>0x0</c>) by allocating and copying.
    /// </summary>
    /// <param name="ptr">A pointer to the C string.</param>
    /// <returns>A <see cref="string" /> equivalent of <paramref name="ptr" />.</returns>
    public static string String16U(CString16U ptr)
    {
        if (ptr.IsNull)
        {
            return string.Empty;
        }

        if (PointersToStrings16U.TryGetValue(ptr, out var result))
        {
            return result;
        }

        var hash = djb2((byte*)ptr._ptr);
        if (StringHashesToPointers16U.TryGetValue(hash, out var pointer2))
        {
            result = PointersToStrings16U[pointer2];
            return result;
        }

        // calls ASM/C/C++ functions to calculate length and then "FastAllocate" the string with the GC
        // https://mattwarren.org/2016/05/31/Strings-and-the-CLR-a-Special-Relationship/
        result = Marshal.PtrToStringUni(pointer2);

        if (string.IsNullOrEmpty(result))
        {
            return string.Empty;
        }

        StringHashesToPointers16U.Add(hash, ptr);
        PointersToStrings16U.Add(ptr, result);

        return result;
    }

    /// <summary>
    ///     Converts a C string pointer (one dimensional byte array terminated by a
    ///     <c>0x0</c>) for a specified <see cref="string" /> by allocating and copying.
    /// </summary>
    /// <param name="str">The <see cref="string" />.</param>
    /// <returns>A C string pointer.</returns>
    public static CString8U CString8U(string str)
    {
        var hash = djb2(str);
        if (StringHashesToPointers8U.TryGetValue(hash, out var r))
        {
            return r;
        }

        // ReSharper disable once JoinDeclarationAndInitializer
        var pointer = Marshal.StringToHGlobalAnsi(str);
        StringHashesToPointers8U.Add(hash, new CString8U(pointer));
        PointersToStrings8U.Add(pointer, str);

        return new CString8U(pointer);
    }

    /// <summary>
    ///     Converts a C string pointer (one dimensional byte array terminated by a
    ///     <c>0x0</c>) for a specified <see cref="string" /> by allocating and copying.
    /// </summary>
    /// <param name="str">The <see cref="string" />.</param>
    /// <returns>A C string pointer.</returns>
    public static CString16U CString16U(string str)
    {
        var hash = djb2(str);
        if (StringHashesToPointers16U.TryGetValue(hash, out var r))
        {
            return r;
        }

        // ReSharper disable once JoinDeclarationAndInitializer
        var pointer = Marshal.StringToHGlobalAnsi(str);
        StringHashesToPointers16U.Add(hash, new CString16U(pointer));
        PointersToStrings16U.Add(pointer, str);

        return new CString16U(pointer);
    }

    /// <summary>
    ///     Converts an array of strings to an array of C strings of type `char` (multi-dimensional array of one
    ///     dimensional byte arrays each terminated by a <c>0x0</c>) by allocating and copying.
    /// </summary>
    /// <remarks>
    ///     <para>Calls <see cref="CString8U" />.</para>
    /// </remarks>
    /// <param name="values">The strings.</param>
    /// <returns>An array pointer of C string pointers. You are responsible for freeing the returned pointer.</returns>
    public static CString8U* CString8UArray(ReadOnlySpan<string> values)
    {
        var pointerSize = IntPtr.Size;
        var result = (CString8U*)AllocateMemory(pointerSize * values.Length);
        for (var i = 0; i < values.Length; ++i)
        {
            var @string = values[i];
            var cString = CString8U(@string);
            result[i] = cString;
        }

        return result;
    }

    /// <summary>
    ///     Converts an array of strings to an array of C strings of type `wchar_t` (multi-dimensional array of one
    ///     dimensional ushort arrays each terminated by a <c>0x0</c>) by allocating and copying.
    /// </summary>
    /// <remarks>
    ///     <para>Calls <see cref="CString8U" />.</para>
    /// </remarks>
    /// <param name="values">The strings.</param>
    /// <returns>An array pointer of C string pointers. You are responsible for freeing the returned pointer.</returns>
    public static CString16U* CString16UArray(ReadOnlySpan<string> values)
    {
        var pointerSize = IntPtr.Size;
        var result = (CString16U*)AllocateMemory(pointerSize * values.Length);
        for (var i = 0; i < values.Length; ++i)
        {
            var @string = values[i];
            var cString = CString16U(@string);
            result[i] = cString;
        }

        return result;
    }

    /// <summary>
    ///     Frees the memory for all previously allocated C strings and releases references to all <see cref="string" />
    ///     objects which happened during <see cref="String8U" />, <see cref="String16U"/>, <see cref="CString8U"/>
    ///     or <see cref="CString16U" />. Does <b>not</b> garbage collect.
    /// </summary>
    public static void FreeAllStrings()
    {
        foreach (var (ptr, _) in PointersToStrings8U)
        {
            Marshal.FreeHGlobal(ptr);
        }

        // We can not guarantee that the application has not a strong reference the string since it was allocated,
        //  so we have to let the GC take the wheel here. Thus, this method should NOT garbage collect; that's
        //  on the responsibility of the application developer. The best we can do is just remove any and all strong
        //  references we have here to the strings.

        StringHashesToPointers8U.Clear();
        PointersToStrings8U.Clear();
    }

    /// <summary>
    ///     Frees the memory for specific previously allocated C strings and releases associated references to
    ///     <see cref="string" /> objects which happened during <see cref="String8U" /> or
    ///     <see cref="CString8U" />. Does <b>not</b> garbage collect.
    /// </summary>
    /// <param name="ptrs">The C string pointers.</param>
    /// <param name="count">The number of C string pointers.</param>
    public static void FreeCStrings(CString8U* ptrs, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var ptr = ptrs[i];
            FreeCString8U(ptr);
        }

        FreeMemory(ptrs);
    }

    /// <summary>
    ///     Frees the memory for the previously allocated C string and releases reference to the
    ///     <see cref="string" /> object which happened during <see cref="String8U" /> or <see cref="CString8U" />.
    ///     Does <b>not</b> garbage collect.
    /// </summary>
    /// <param name="ptr">The string.</param>
    public static void FreeCString8U(CString8U ptr)
    {
        if (!PointersToStrings8U.ContainsKey(ptr._ptr))
        {
            return;
        }

        Marshal.FreeHGlobal(ptr);
        var hash = djb2(ptr);
        StringHashesToPointers8U.Remove(hash);
        PointersToStrings8U.Remove(ptr._ptr);
    }

    /// <summary>
    ///     Frees the memory for the previously allocated C string and releases reference to the
    ///     <see cref="string" /> object which happened during <see cref="String16U" /> or <see cref="CString16U" />.
    ///     Does <b>not</b> garbage collect.
    /// </summary>
    /// <param name="ptr">The string.</param>
    public static void FreeCString16U(CString16U ptr)
    {
        if (!PointersToStrings16U.ContainsKey(ptr._ptr))
        {
            return;
        }

        Marshal.FreeHGlobal(ptr);
        var hash = djb2(ptr);
        StringHashesToPointers16U.Remove(hash);
        PointersToStrings16U.Remove(ptr._ptr);
    }

    internal static uint djb2(byte* str)
    {
        uint hash = 5381;

        unchecked
        {
            uint c;
            while ((c = *str++) != 0)
            {
                hash = ((hash << 5) + hash) + c; // hash * 33 + c
            }
        }

        return hash;
    }

    internal static uint djb2(ushort* str)
    {
        uint hash = 5381;

        unchecked
        {
            uint c;
            while ((c = *str++) != 0)
            {
                hash = ((hash << 5) + hash) + c; // hash * 33 + c
            }
        }

        return hash;
    }

    private static uint djb2(string str)
    {
        uint hash = 5381;

        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var c in str)
        {
            hash = (hash << 5) + hash + c; // hash * 33 + c
        }

        return hash;
    }
}
