

using System.Globalization;
using System.Runtime.CompilerServices;

namespace BLITTY.Native;

internal static unsafe partial class BGFX
{
    /// <summary>
    /// Specifies debug text colors.
    /// </summary>
    public enum BGFX_DebugColor
    {
        /// <summary>
        /// Black.
        /// </summary>
        Black,

        /// <summary>
        /// Blue.
        /// </summary>
        Blue,

        /// <summary>
        /// Green.
        /// </summary>
        Green,

        /// <summary>
        /// Cyan.
        /// </summary>
        Cyan,

        /// <summary>
        /// Red.
        /// </summary>
        Red,

        /// <summary>
        /// Magenta.
        /// </summary>
        Magenta,

        /// <summary>
        /// Brown.
        /// </summary>
        Brown,

        /// <summary>
        /// Light gray.
        /// </summary>
        LightGray,

        /// <summary>
        /// Dark gray.
        /// </summary>
        DarkGray,

        /// <summary>
        /// Light blue.
        /// </summary>
        LightBlue,

        /// <summary>
        /// Light green.
        /// </summary>
        LightGreen,

        /// <summary>
        /// Light cyan.
        /// </summary>
        LightCyan,

        /// <summary>
        /// Light red.
        /// </summary>
        LightRed,

        /// <summary>
        /// Light magenta.
        /// </summary>
        LightMagenta,

        /// <summary>
        /// Yellow.
        /// </summary>
        Yellow,

        /// <summary>
        /// White.
        /// </summary>
        White
    }


    /// <summary>
    /// Clears the debug text buffer.
    /// </summary>
    /// <param name="color">The color with which to clear the background.</param>
    /// <param name="smallText"><c>true</c> to use a small font for debug output; <c>false</c> to use normal sized text.</param>
    public static void BGFX_DebugTextClear(BGFX_DebugColor color = BGFX_DebugColor.Black, bool smallText = false)
    {
        var attr = (byte)((byte)color << 4);
        BGFX_DebugTextClear(attr, smallText);
    }

    /// <summary>
    /// Writes debug text to the screen.
    /// </summary>
    /// <param name="x">The X position, in cells.</param>
    /// <param name="y">The Y position, in cells.</param>
    /// <param name="foreColor">The foreground color of the text.</param>
    /// <param name="backColor">The background color of the text.</param>
    /// <param name="format">The format of the message.</param>
    /// <param name="args">The arguments with which to format the message.</param>
    public static void BGFX_DebugTextWrite(int x, int y, BGFX_DebugColor foreColor, BGFX_DebugColor backColor, string format, params object[] args)
    {
        BGFX_DebugTextWrite(x, y, foreColor, backColor, string.Format(CultureInfo.CurrentCulture, format, args));
    }

    /// <summary>
    /// Writes debug text to the screen.
    /// </summary>
    /// <param name="x">The X position, in cells.</param>
    /// <param name="y">The Y position, in cells.</param>
    /// <param name="foreColor">The foreground color of the text.</param>
    /// <param name="backColor">The background color of the text.</param>
    /// <param name="message">The message to write.</param>
    public static void BGFX_DebugTextWrite(int x, int y, BGFX_DebugColor foreColor, BGFX_DebugColor backColor, string message)
    {
        var attr = (byte)(((byte)backColor << 4) | (byte)foreColor);
        BGFX_DebugTextPrintf((ushort)x, (ushort)y, attr, "%s", message);
    }

    /// <summary>
    /// Draws data directly into the debug text buffer.
    /// </summary>
    /// <param name="x">The X position, in cells.</param>
    /// <param name="y">The Y position, in cells.</param>
    /// <param name="width">The width of the image to draw.</param>
    /// <param name="height">The height of the image to draw.</param>
    /// <param name="data">The image data bytes.</param>
    /// <param name="pitch">The pitch of each line in the image data.</param>
    public static void BGFX_DebugTextImage(int x, int y, int width, int height, IntPtr data, int pitch)
    {
        BGFX_DebugTextImage((ushort)x, (ushort)y, (ushort)width, (ushort)height, (void*)data, (ushort)pitch);
    }

    /// <summary>
    /// Draws data directly into the debug text buffer.
    /// </summary>
    /// <param name="x">The X position, in cells.</param>
    /// <param name="y">The Y position, in cells.</param>
    /// <param name="width">The width of the image to draw.</param>
    /// <param name="height">The height of the image to draw.</param>
    /// <param name="data">The image data bytes.</param>
    /// <param name="pitch">The pitch of each line in the image data.</param>
    public static void BGFX_DebugTextImage(int x, int y, int width, int height, byte[] data, int pitch)
    {
        fixed (byte* ptr = data)
            BGFX_DebugTextImage((ushort)x, (ushort)y, (ushort)width, (ushort)height, ptr, (ushort)pitch);
    }

    public static BGFX_Memory* BGFX_AllocGraphicsMemoryBuffer<T>(Span<T> array)
    {
        var size = (uint)(array.Length * Unsafe.SizeOf<T>());
        var data = BGFX_Alloc(size);
        Unsafe.CopyBlockUnaligned(data->data, Unsafe.AsPointer(ref array[0]), size);
        return data;
    }

    public static BGFX_Memory* BGFX_AllocGraphicsMemoryBuffer(IntPtr dataPtr, int dataSize)
    {
        var data = BGFX_Alloc((uint)dataSize);
        Unsafe.CopyBlockUnaligned(data->data, dataPtr.ToPointer(), (uint)dataSize);
        return data;
    }

    public static BGFX_Memory* BGFX_GetMemoryBufferReference<T>(Span<T> array)
    {
        var size = (uint)(array.Length * Unsafe.SizeOf<T>());
        var data = BGFX_MakeRef(Unsafe.AsPointer(ref array[0]), size);
        return data;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BGFX_StateFlags BGFX_STATE_BLEND_FUNC(BGFX_StateFlags src, BGFX_StateFlags dst)
    {
        return BGFX_STATE_BLEND_FUNC_SEPARATE(src, dst, src, dst);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BGFX_StateFlags BGFX_STATE_BLEND_FUNC_SEPARATE(BGFX_StateFlags srcRgb, BGFX_StateFlags dstRgb, BGFX_StateFlags srcA, BGFX_StateFlags dstA)
    {
        return (BGFX_StateFlags)((((ulong)(srcRgb) | ((ulong)(dstRgb) << 4))) | (((ulong)(srcA) | ((ulong)(dstA) << 4)) << 8));
    }
}
