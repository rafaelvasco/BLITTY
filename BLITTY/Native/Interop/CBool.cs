namespace BLITTY.Native;

internal readonly struct CBool
{
    private readonly byte _value;

    private CBool(bool value)
    {
        _value = Convert.ToByte(value);
    }

    /// <summary>
    ///     Converts the specified <see cref="bool" /> to a <see cref="CBool" />.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A <see cref="CBool" />.</returns>
    public static implicit operator CBool(bool value)
    {
        return new(value);
    }

    /// <summary>
    ///     Converts the specified <see cref="CBool" /> to a <see cref="bool" />.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A <see cref="bool" />.</returns>
    public static implicit operator bool(CBool value)
    {
        return Convert.ToBoolean(value._value);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Convert.ToBoolean(_value).ToString();
    }
}
