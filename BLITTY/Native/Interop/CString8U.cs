namespace BLITTY.Native;

internal readonly unsafe struct CString8U
{
    internal readonly IntPtr _ptr;

    /// <summary>
    ///     Gets a <see cref="bool" /> value indicating whether this <see cref="CString8U" /> is a null pointer.
    /// </summary>
    public bool IsNull => _ptr == IntPtr.Zero;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CString8U" /> struct.
    /// </summary>
    /// <param name="ptr">The pointer value.</param>
    public CString8U(byte* ptr)
    {
        _ptr = (IntPtr)ptr;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CString8U" /> struct.
    /// </summary>
    /// <param name="ptr">The pointer value.</param>
    public CString8U(IntPtr ptr)
    {
        _ptr = ptr;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CString8U" /> struct.
    /// </summary>
    /// <param name="s">The string.</param>
    public CString8U(string s)
    {
        _ptr = NativeOps.CString8U(s);
    }

    /// <summary>
    ///     Performs an explicit conversion from a <see cref="IntPtr" /> to a <see cref="CString8U" />.
    /// </summary>
    /// <param name="ptr">The pointer.</param>
    /// <returns>
    ///     The resulting <see cref="CString8U" />.
    /// </returns>
    public static explicit operator CString8U(IntPtr ptr)
    {
        return new CString8U(ptr);
    }

    /// <summary>
    ///     Performs an implicit conversion from a byte pointer to a <see cref="CString8U" />.
    /// </summary>
    /// <param name="ptr">The pointer.</param>
    /// <returns>
    ///     The resulting <see cref="CString8U" />.
    /// </returns>
    public static implicit operator CString8U(byte* ptr)
    {
        return new CString8U((IntPtr)ptr);
    }

    /// <summary>
    ///     Performs an implicit conversion from a <see cref="CString8U" /> to a <see cref="IntPtr" />.
    /// </summary>
    /// <param name="ptr">The <see cref="CString8U" />.</param>
    /// <returns>
    ///     The resulting <see cref="IntPtr" />.
    /// </returns>
    public static implicit operator IntPtr(CString8U ptr)
    {
        return ptr._ptr;
    }

    /// <summary>
    ///     Performs an implicit conversion from a <see cref="CString8U" /> to a <see cref="string" />.
    /// </summary>
    /// <param name="value">The <see cref="CString8U" />.</param>
    /// <returns>
    ///     The resulting <see cref="string" />.
    /// </returns>
    public static implicit operator string(CString8U value)
    {
        return NativeOps.String8U(value);
    }

    /// <summary>
    ///     Performs an implicit conversion from a <see cref="string" /> to a <see cref="CString8U" />.
    /// </summary>
    /// <param name="s">The <see cref="string" />.</param>
    /// <returns>
    ///     The resulting <see cref="CString8U" />.
    /// </returns>
    public static implicit operator CString8U(string s)
    {
        return NativeOps.CString8U(s);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return (int)NativeOps.djb2((byte*)_ptr);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return NativeOps.String8U(this);
    }
}
