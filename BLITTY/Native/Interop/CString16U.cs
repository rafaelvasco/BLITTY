namespace BLITTY.Native;

internal readonly unsafe struct CString16U
{
    internal readonly IntPtr _ptr;

    /// <summary>
    ///     Gets a <see cref="bool" /> value indicating whether this <see cref="CString16U" /> is a null pointer.
    /// </summary>
    public bool IsNull => _ptr == IntPtr.Zero;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CString16U" /> struct.
    /// </summary>
    /// <param name="ptr">The pointer value.</param>
    public CString16U(byte* ptr)
    {
        _ptr = (IntPtr)ptr;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CString16U" /> struct.
    /// </summary>
    /// <param name="ptr">The pointer value.</param>
    public CString16U(IntPtr ptr)
    {
        _ptr = ptr;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CString16U" /> struct.
    /// </summary>
    /// <param name="s">The string value.</param>
    public CString16U(string s)
    {
        _ptr = NativeOps.CString16U(s);
    }

    /// <summary>
    ///     Performs an explicit conversion from a <see cref="IntPtr" /> to a <see cref="CString16U" />.
    /// </summary>
    /// <param name="ptr">The pointer.</param>
    /// <returns>
    ///     The resulting <see cref="CString16U" />.
    /// </returns>
    public static explicit operator CString16U(IntPtr ptr)
    {
        return new(ptr);
    }

    /// <summary>
    ///     Performs an implicit conversion from a byte pointer to a <see cref="CString16U" />.
    /// </summary>
    /// <param name="ptr">The pointer.</param>
    /// <returns>
    ///     The resulting <see cref="CString16U" />.
    /// </returns>
    public static implicit operator CString16U(byte* ptr)
    {
        return new((IntPtr)ptr);
    }

    /// <summary>
    ///     Performs an implicit conversion from a <see cref="CString16U" /> to a <see cref="IntPtr" />.
    /// </summary>
    /// <param name="value">The pointer.</param>
    /// <returns>
    ///     The resulting <see cref="IntPtr" />.
    /// </returns>
    public static implicit operator IntPtr(CString16U value)
    {
        return value._ptr;
    }

    /// <summary>
    ///     Performs an implicit conversion from a <see cref="CString16U" /> to a <see cref="string" />.
    /// </summary>
    /// <param name="value">The <see cref="CString16U" />.</param>
    /// <returns>
    ///     The resulting <see cref="string" />.
    /// </returns>
    public static implicit operator string(CString16U value)
    {
        return NativeOps.String16U(value);
    }

    /// <summary>
    ///     Performs an implicit conversion from a <see cref="string" /> to a <see cref="CString16U" />.
    /// </summary>
    /// <param name="s">The <see cref="string" />.</param>
    /// <returns>
    ///     The resulting <see cref="CString16U" />.
    /// </returns>
    public static implicit operator CString16U(string s)
    {
        return NativeOps.CString16U(s);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return (int)NativeOps.djb2((byte*)_ptr);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return NativeOps.String16U(this);
    }
}
