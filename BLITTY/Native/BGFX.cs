﻿/*
 * Copyright 2011-2021 Branimir Karadzic. All rights reserved.
 * License: https://github.com/bkaradzic/bgfx/blob/master/LICENSE
 */

using System.Runtime.InteropServices;

namespace BLITTY.Native;
#pragma warning disable CS0649

internal static unsafe partial class BGFX
{
    private const string DllName = "BGFX.dll";


    [Flags]
    public enum BGFX_StateFlags : ulong
    {
        /// <summary>
        /// Enable R write.
        /// </summary>
        WriteR = 0x0000000000000001,

        /// <summary>
        /// Enable G write.
        /// </summary>
        WriteG = 0x0000000000000002,

        /// <summary>
        /// Enable B write.
        /// </summary>
        WriteB = 0x0000000000000004,

        /// <summary>
        /// Enable alpha write.
        /// </summary>
        WriteA = 0x0000000000000008,

        /// <summary>
        /// Enable depth write.
        /// </summary>
        WriteZ = 0x0000004000000000,

        /// <summary>
        /// Enable RGB write.
        /// </summary>
        WriteRgb = 0x0000000000000007,

        /// <summary>
        /// Write all channels mask.
        /// </summary>
        WriteMask = 0x000000400000000f,

        /// <summary>
        /// Enable depth test, less.
        /// </summary>
        DepthTestLess = 0x0000000000000010,

        /// <summary>
        /// Enable depth test, less or equal.
        /// </summary>
        DepthTestLequal = 0x0000000000000020,

        /// <summary>
        /// Enable depth test, equal.
        /// </summary>
        DepthTestEqual = 0x0000000000000030,

        /// <summary>
        /// Enable depth test, greater or equal.
        /// </summary>
        DepthTestGequal = 0x0000000000000040,

        /// <summary>
        /// Enable depth test, greater.
        /// </summary>
        DepthTestGreater = 0x0000000000000050,

        /// <summary>
        /// Enable depth test, not equal.
        /// </summary>
        DepthTestNotequal = 0x0000000000000060,

        /// <summary>
        /// Enable depth test, never.
        /// </summary>
        DepthTestNever = 0x0000000000000070,

        /// <summary>
        /// Enable depth test, always.
        /// </summary>
        DepthTestAlways = 0x0000000000000080,
        DepthTestShift = 4,
        DepthTestMask = 0x00000000000000f0,

        /// <summary>
        /// 0, 0, 0, 0
        /// </summary>
        BlendZero = 0x0000000000001000,

        /// <summary>
        /// 1, 1, 1, 1
        /// </summary>
        BlendOne = 0x0000000000002000,

        /// <summary>
        /// Rs, Gs, Bs, As
        /// </summary>
        BlendSrcColor = 0x0000000000003000,

        /// <summary>
        /// 1-Rs, 1-Gs, 1-Bs, 1-As
        /// </summary>
        BlendInvSrcColor = 0x0000000000004000,

        /// <summary>
        /// As, As, As, As
        /// </summary>
        BlendSrcAlpha = 0x0000000000005000,
        /// <summary>
        /// 1-As, 1-As, 1-As, 1-As
        /// </summary>
        BlendInvSrcAlpha = 0x0000000000006000,

        /// <summary>
        /// Ad, Ad, Ad, Ad
        /// </summary>
        BlendDstAlpha = 0x0000000000007000,

        /// <summary>
        /// 1-Ad, 1-Ad, 1-Ad ,1-Ad
        /// </summary>
        BlendInvDstAlpha = 0x0000000000008000,

        /// <summary>
        /// Rd, Gd, Bd, Ad
        /// </summary>
        BlendDstColor = 0x0000000000009000,

        /// <summary>
        /// 1-Rd, 1-Gd, 1-Bd, 1-Ad
        /// </summary>
        BlendInvDstColor = 0x000000000000a000,

        /// <summary>
        /// f, f, f, 1; f = min(As, 1-Ad)
        /// </summary>
        BlendSrcAlphaSat = 0x000000000000b000,

        /// <summary>
        /// Blend factor
        /// </summary>
        BlendFactor = 0x000000000000c000,

        /// <summary>
        /// 1-Blend factor
        /// </summary>
        BlendInvFactor = 0x000000000000d000,
        BlendShift = 12,
        BlendMask = 0x000000000ffff000,

        /// <summary>
        /// Blend add: src + dst.
        /// </summary>
        BlendEquationAdd = 0x0000000000000000,

        /// <summary>
        /// Blend subtract: src - dst.
        /// </summary>
        BlendEquationSub = 0x0000000010000000,

        /// <summary>
        /// Blend reverse subtract: dst - src.
        /// </summary>
        BlendEquationRevsub = 0x0000000020000000,

        /// <summary>
        /// Blend min: min(src, dst).
        /// </summary>
        BlendEquationMin = 0x0000000030000000,

        /// <summary>
        /// Blend max: max(src, dst).
        /// </summary>
        BlendEquationMax = 0x0000000040000000,
        BlendEquationShift = 28,
        BlendEquationMask = 0x00000003f0000000,

        /// <summary>
        /// Cull clockwise triangles.
        /// </summary>
        CullCw = 0x0000001000000000,

        /// <summary>
        /// Cull counter-clockwise triangles.
        /// </summary>
        CullCcw = 0x0000002000000000,
        CullShift = 36,
        CullMask = 0x0000003000000000,
        AlphaRefShift = 40,
        AlphaRefMask = 0x0000ff0000000000,

        /// <summary>
        /// Tristrip.
        /// </summary>
        PtTristrip = 0x0001000000000000,

        /// <summary>
        /// Lines.
        /// </summary>
        PtLines = 0x0002000000000000,

        /// <summary>
        /// Line strip.
        /// </summary>
        PtLinestrip = 0x0003000000000000,

        /// <summary>
        /// Points.
        /// </summary>
        PtPoints = 0x0004000000000000,
        PtShift = 48,
        PtMask = 0x0007000000000000,
        PointSizeShift = 52,
        PointSizeMask = 0x00f0000000000000,

        /// <summary>
        /// Enable MSAA rasterization.
        /// </summary>
        Msaa = 0x0100000000000000,

        /// <summary>
        /// Enable line AA rasterization.
        /// </summary>
        Lineaa = 0x0200000000000000,

        /// <summary>
        /// Enable conservative rasterization.
        /// </summary>
        ConservativeRaster = 0x0400000000000000,

        /// <summary>
        /// No state.
        /// </summary>
        None = 0x0000000000000000,

        /// <summary>
        /// Front counter-clockwise (default is clockwise).
        /// </summary>
        FrontCcw = 0x0000008000000000,

        /// <summary>
        /// Enable blend independent.
        /// </summary>
        BlendIndependent = 0x0000000400000000,

        /// <summary>
        /// Enable alpha to coverage.
        /// </summary>
        BlendAlphaToCoverage = 0x0000000800000000,

        /// <summary>
        /// Default state is write to RGB, alpha, and depth with depth test less enabled, with clockwise
        /// culling and MSAA (when writing into MSAA frame buffer, otherwise this flag is ignored).
        /// </summary>
        Default = 0x010000500000001f,
        Mask = 0xffffffffffffffff,
        ReservedShift = 61,
        ReservedMask = 0xe000000000000000,
    }

    [Flags]
    public enum BGFX_StencilFlags : uint
    {
        FuncRefShift = 0,
        FuncRefMask = 0x000000ff,
        FuncRmaskShift = 8,
        FuncRmaskMask = 0x0000ff00,
        None = 0x00000000,
        Mask = 0xffffffff,
        Default = 0x00000000,

        /// <summary>
        /// Enable stencil test, less.
        /// </summary>
        TestLess = 0x00010000,

        /// <summary>
        /// Enable stencil test, less or equal.
        /// </summary>
        TestLequal = 0x00020000,

        /// <summary>
        /// Enable stencil test, equal.
        /// </summary>
        TestEqual = 0x00030000,

        /// <summary>
        /// Enable stencil test, greater or equal.
        /// </summary>
        TestGequal = 0x00040000,

        /// <summary>
        /// Enable stencil test, greater.
        /// </summary>
        TestGreater = 0x00050000,

        /// <summary>
        /// Enable stencil test, not equal.
        /// </summary>
        TestNotequal = 0x00060000,

        /// <summary>
        /// Enable stencil test, never.
        /// </summary>
        TestNever = 0x00070000,

        /// <summary>
        /// Enable stencil test, always.
        /// </summary>
        TestAlways = 0x00080000,
        TestShift = 16,
        TestMask = 0x000f0000,

        /// <summary>
        /// Zero.
        /// </summary>
        OpFailSZero = 0x00000000,

        /// <summary>
        /// Keep.
        /// </summary>
        OpFailSKeep = 0x00100000,

        /// <summary>
        /// Replace.
        /// </summary>
        OpFailSReplace = 0x00200000,

        /// <summary>
        /// Increment and wrap.
        /// </summary>
        OpFailSIncr = 0x00300000,

        /// <summary>
        /// Increment and clamp.
        /// </summary>
        OpFailSIncrsat = 0x00400000,

        /// <summary>
        /// Decrement and wrap.
        /// </summary>
        OpFailSDecr = 0x00500000,

        /// <summary>
        /// Decrement and clamp.
        /// </summary>
        OpFailSDecrsat = 0x00600000,

        /// <summary>
        /// Invert.
        /// </summary>
        OpFailSInvert = 0x00700000,
        OpFailSShift = 20,
        OpFailSMask = 0x00f00000,

        /// <summary>
        /// Zero.
        /// </summary>
        OpFailZZero = 0x00000000,

        /// <summary>
        /// Keep.
        /// </summary>
        OpFailZKeep = 0x01000000,

        /// <summary>
        /// Replace.
        /// </summary>
        OpFailZReplace = 0x02000000,

        /// <summary>
        /// Increment and wrap.
        /// </summary>
        OpFailZIncr = 0x03000000,

        /// <summary>
        /// Increment and clamp.
        /// </summary>
        OpFailZIncrsat = 0x04000000,

        /// <summary>
        /// Decrement and wrap.
        /// </summary>
        OpFailZDecr = 0x05000000,

        /// <summary>
        /// Decrement and clamp.
        /// </summary>
        OpFailZDecrsat = 0x06000000,

        /// <summary>
        /// Invert.
        /// </summary>
        OpFailZInvert = 0x07000000,
        OpFailZShift = 24,
        OpFailZMask = 0x0f000000,

        /// <summary>
        /// Zero.
        /// </summary>
        OpPassZZero = 0x00000000,

        /// <summary>
        /// Keep.
        /// </summary>
        OpPassZKeep = 0x10000000,

        /// <summary>
        /// Replace.
        /// </summary>
        OpPassZReplace = 0x20000000,

        /// <summary>
        /// Increment and wrap.
        /// </summary>
        OpPassZIncr = 0x30000000,

        /// <summary>
        /// Increment and clamp.
        /// </summary>
        OpPassZIncrsat = 0x40000000,

        /// <summary>
        /// Decrement and wrap.
        /// </summary>
        OpPassZDecr = 0x50000000,

        /// <summary>
        /// Decrement and clamp.
        /// </summary>
        OpPassZDecrsat = 0x60000000,

        /// <summary>
        /// Invert.
        /// </summary>
        OpPassZInvert = 0x70000000,
        OpPassZShift = 28,
        OpPassZMask = 0xf0000000,
    }

    [Flags]
    public enum BGFX_ClearFlags : ushort
    {
        /// <summary>
        /// No clear flags.
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// Clear color.
        /// </summary>
        Color = 0x0001,

        /// <summary>
        /// Clear depth.
        /// </summary>
        Depth = 0x0002,

        /// <summary>
        /// Clear stencil.
        /// </summary>
        Stencil = 0x0004,

        /// <summary>
        /// Discard frame buffer attachment 0.
        /// </summary>
        DiscardColor0 = 0x0008,

        /// <summary>
        /// Discard frame buffer attachment 1.
        /// </summary>
        DiscardColor1 = 0x0010,

        /// <summary>
        /// Discard frame buffer attachment 2.
        /// </summary>
        DiscardColor2 = 0x0020,

        /// <summary>
        /// Discard frame buffer attachment 3.
        /// </summary>
        DiscardColor3 = 0x0040,

        /// <summary>
        /// Discard frame buffer attachment 4.
        /// </summary>
        DiscardColor4 = 0x0080,

        /// <summary>
        /// Discard frame buffer attachment 5.
        /// </summary>
        DiscardColor5 = 0x0100,

        /// <summary>
        /// Discard frame buffer attachment 6.
        /// </summary>
        DiscardColor6 = 0x0200,

        /// <summary>
        /// Discard frame buffer attachment 7.
        /// </summary>
        DiscardColor7 = 0x0400,

        /// <summary>
        /// Discard frame buffer depth attachment.
        /// </summary>
        DiscardDepth = 0x0800,

        /// <summary>
        /// Discard frame buffer stencil attachment.
        /// </summary>
        DiscardStencil = 0x1000,
        DiscardColorMask = 0x07f8,
        DiscardMask = 0x1ff8,
    }

    [Flags]
    public enum BGFX_DiscardFlags : uint
    {
        /// <summary>
        /// Preserve everything.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Discard texture sampler and buffer bindings.
        /// </summary>
        Bindings = 0x00000001,

        /// <summary>
        /// Discard index buffer.
        /// </summary>
        IndexBuffer = 0x00000002,

        /// <summary>
        /// Discard instance data.
        /// </summary>
        InstanceData = 0x00000004,

        /// <summary>
        /// Discard state and uniform bindings.
        /// </summary>
        State = 0x00000008,

        /// <summary>
        /// Discard transform.
        /// </summary>
        Transform = 0x00000010,

        /// <summary>
        /// Discard vertex streams.
        /// </summary>
        VertexStreams = 0x00000020,

        /// <summary>
        /// Discard all states.
        /// </summary>
        All = 0x000000ff,
    }

    [Flags]
    public enum BGFX_DebugFlags : uint
    {
        /// <summary>
        /// No debug.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Enable wireframe for all primitives.
        /// </summary>
        Wireframe = 0x00000001,

        /// <summary>
        /// Enable infinitely fast hardware test. No draw calls will be submitted to driver.
        /// It's useful when profiling to quickly assess bottleneck between CPU and GPU.
        /// </summary>
        Ifh = 0x00000002,

        /// <summary>
        /// Enable statistics display.
        /// </summary>
        Stats = 0x00000004,

        /// <summary>
        /// Enable debug text display.
        /// </summary>
        Text = 0x00000008,

        /// <summary>
        /// Enable profiler.
        /// </summary>
        Profiler = 0x00000010,
    }

    [Flags]
    public enum BGFX_BufferFlags : ushort
    {
        /// <summary>
        /// 1 8-bit value
        /// </summary>
        ComputeFormat8x1 = 0x0001,

        /// <summary>
        /// 2 8-bit values
        /// </summary>
        ComputeFormat8x2 = 0x0002,

        /// <summary>
        /// 4 8-bit values
        /// </summary>
        ComputeFormat8x4 = 0x0003,

        /// <summary>
        /// 1 16-bit value
        /// </summary>
        ComputeFormat16x1 = 0x0004,

        /// <summary>
        /// 2 16-bit values
        /// </summary>
        ComputeFormat16x2 = 0x0005,

        /// <summary>
        /// 4 16-bit values
        /// </summary>
        ComputeFormat16x4 = 0x0006,

        /// <summary>
        /// 1 32-bit value
        /// </summary>
        ComputeFormat32x1 = 0x0007,

        /// <summary>
        /// 2 32-bit values
        /// </summary>
        ComputeFormat32x2 = 0x0008,

        /// <summary>
        /// 4 32-bit values
        /// </summary>
        ComputeFormat32x4 = 0x0009,
        ComputeFormatShift = 0,
        ComputeFormatMask = 0x000f,

        /// <summary>
        /// Type `int`.
        /// </summary>
        ComputeTypeInt = 0x0010,

        /// <summary>
        /// Type `uint`.
        /// </summary>
        ComputeTypeUint = 0x0020,

        /// <summary>
        /// Type `float`.
        /// </summary>
        ComputeTypeFloat = 0x0030,
        ComputeTypeShift = 4,
        ComputeTypeMask = 0x0030,
        None = 0x0000,

        /// <summary>
        /// Buffer will be read by shader.
        /// </summary>
        ComputeRead = 0x0100,

        /// <summary>
        /// Buffer will be used for writing.
        /// </summary>
        ComputeWrite = 0x0200,

        /// <summary>
        /// Buffer will be used for storing draw indirect commands.
        /// </summary>
        DrawIndirect = 0x0400,

        /// <summary>
        /// Allow dynamic index/vertex buffer resize during update.
        /// </summary>
        AllowResize = 0x0800,

        /// <summary>
        /// Index buffer contains 32-bit indices.
        /// </summary>
        Index32 = 0x1000,
        ComputeReadWrite = 0x0300,
    }

    [Flags]
    public enum BGFX_TextureFlags : ulong
    {
        None = 0x0000000000000000,

        /// <summary>
        /// Texture will be used for MSAA sampling.
        /// </summary>
        MsaaSample = 0x0000000800000000,

        /// <summary>
        /// Render target no MSAA.
        /// </summary>
        Rt = 0x0000001000000000,

        /// <summary>
        /// Texture will be used for compute write.
        /// </summary>
        ComputeWrite = 0x0000100000000000,

        /// <summary>
        /// Sample texture as sRGB.
        /// </summary>
        Srgb = 0x0000200000000000,

        /// <summary>
        /// Texture will be used as blit destination.
        /// </summary>
        BlitDst = 0x0000400000000000,

        /// <summary>
        /// Texture will be used for read back from GPU.
        /// </summary>
        ReadBack = 0x0000800000000000,

        /// <summary>
        /// Render target MSAAx2 mode.
        /// </summary>
        RtMsaaX2 = 0x0000002000000000,

        /// <summary>
        /// Render target MSAAx4 mode.
        /// </summary>
        RtMsaaX4 = 0x0000003000000000,

        /// <summary>
        /// Render target MSAAx8 mode.
        /// </summary>
        RtMsaaX8 = 0x0000004000000000,

        /// <summary>
        /// Render target MSAAx16 mode.
        /// </summary>
        RtMsaaX16 = 0x0000005000000000,
        RtMsaaShift = 36,
        RtMsaaMask = 0x0000007000000000,

        /// <summary>
        /// Render target will be used for writing
        /// </summary>
        RtWriteOnly = 0x0000008000000000,
        RtShift = 36,
        RtMask = 0x000000f000000000,
    }

    [Flags]
    public enum BGFX_SamplerFlags : uint
    {
        /// <summary>
        /// Wrap U mode: Mirror
        /// </summary>
        UMirror = 0x00000001,

        /// <summary>
        /// Wrap U mode: Clamp
        /// </summary>
        UClamp = 0x00000002,

        /// <summary>
        /// Wrap U mode: Border
        /// </summary>
        UBorder = 0x00000003,
        UShift = 0,
        UMask = 0x00000003,

        /// <summary>
        /// Wrap V mode: Mirror
        /// </summary>
        VMirror = 0x00000004,

        /// <summary>
        /// Wrap V mode: Clamp
        /// </summary>
        VClamp = 0x00000008,

        /// <summary>
        /// Wrap V mode: Border
        /// </summary>
        VBorder = 0x0000000c,
        VShift = 2,
        VMask = 0x0000000c,

        /// <summary>
        /// Wrap W mode: Mirror
        /// </summary>
        WMirror = 0x00000010,

        /// <summary>
        /// Wrap W mode: Clamp
        /// </summary>
        WClamp = 0x00000020,

        /// <summary>
        /// Wrap W mode: Border
        /// </summary>
        WBorder = 0x00000030,
        WShift = 4,
        WMask = 0x00000030,

        /// <summary>
        /// Min sampling mode: Point
        /// </summary>
        MinPoint = 0x00000040,

        /// <summary>
        /// Min sampling mode: Anisotropic
        /// </summary>
        MinAnisotropic = 0x00000080,
        MinShift = 6,
        MinMask = 0x000000c0,

        /// <summary>
        /// Mag sampling mode: Point
        /// </summary>
        MagPoint = 0x00000100,

        /// <summary>
        /// Mag sampling mode: Anisotropic
        /// </summary>
        MagAnisotropic = 0x00000200,
        MagShift = 8,
        MagMask = 0x00000300,

        /// <summary>
        /// Mip sampling mode: Point
        /// </summary>
        MipPoint = 0x00000400,
        MipShift = 10,
        MipMask = 0x00000400,

        /// <summary>
        /// Compare when sampling depth texture: less.
        /// </summary>
        CompareLess = 0x00010000,

        /// <summary>
        /// Compare when sampling depth texture: less or equal.
        /// </summary>
        CompareLequal = 0x00020000,

        /// <summary>
        /// Compare when sampling depth texture: equal.
        /// </summary>
        CompareEqual = 0x00030000,

        /// <summary>
        /// Compare when sampling depth texture: greater or equal.
        /// </summary>
        CompareGequal = 0x00040000,

        /// <summary>
        /// Compare when sampling depth texture: greater.
        /// </summary>
        CompareGreater = 0x00050000,

        /// <summary>
        /// Compare when sampling depth texture: not equal.
        /// </summary>
        CompareNotequal = 0x00060000,

        /// <summary>
        /// Compare when sampling depth texture: never.
        /// </summary>
        CompareNever = 0x00070000,

        /// <summary>
        /// Compare when sampling depth texture: always.
        /// </summary>
        CompareAlways = 0x00080000,
        CompareShift = 16,
        CompareMask = 0x000f0000,
        BorderColorShift = 24,
        BorderColorMask = 0x0f000000,
        ReservedShift = 28,
        ReservedMask = 0xf0000000,
        None = 0x00000000,

        /// <summary>
        /// Sample stencil instead of depth.
        /// </summary>
        SampleStencil = 0x00100000,
        Point = 0x00000540,
        UvwMirror = 0x00000015,
        UvwClamp = 0x0000002a,
        UvwBorder = 0x0000003f,
        BitsMask = 0x000f07ff,
    }

    [Flags]
    public enum BGFX_ResetFlags : uint
    {
        /// <summary>
        /// Enable 2x MSAA.
        /// </summary>
        MsaaX2 = 0x00000010,

        /// <summary>
        /// Enable 4x MSAA.
        /// </summary>
        MsaaX4 = 0x00000020,

        /// <summary>
        /// Enable 8x MSAA.
        /// </summary>
        MsaaX8 = 0x00000030,

        /// <summary>
        /// Enable 16x MSAA.
        /// </summary>
        MsaaX16 = 0x00000040,
        MsaaShift = 4,
        MsaaMask = 0x00000070,

        /// <summary>
        /// No reset flags.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Not supported yet.
        /// </summary>
        Fullscreen = 0x00000001,

        /// <summary>
        /// Enable V-Sync.
        /// </summary>
        Vsync = 0x00000080,

        /// <summary>
        /// Turn on/off max anisotropy.
        /// </summary>
        Maxanisotropy = 0x00000100,

        /// <summary>
        /// Begin screen capture.
        /// </summary>
        Capture = 0x00000200,

        /// <summary>
        /// Flush rendering after submitting to GPU.
        /// </summary>
        FlushAfterRender = 0x00002000,

        /// <summary>
        /// This flag specifies where flip occurs. Default behaviour is that flip occurs
        /// before rendering new frame. This flag only has effect when `BGFX_CONFIG_MULTITHREADED=0`.
        /// </summary>
        FlipAfterRender = 0x00004000,

        /// <summary>
        /// Enable sRGB backbuffer.
        /// </summary>
        SrgbBackbuffer = 0x00008000,

        /// <summary>
        /// Enable HDR10 rendering.
        /// </summary>
        Hdr10 = 0x00010000,

        /// <summary>
        /// Enable HiDPI rendering.
        /// </summary>
        Hidpi = 0x00020000,

        /// <summary>
        /// Enable depth clamp.
        /// </summary>
        DepthClamp = 0x00040000,

        /// <summary>
        /// Suspend rendering.
        /// </summary>
        Suspend = 0x00080000,
        FullscreenShift = 0,
        FullscreenMask = 0x00000001,
        ReservedShift = 31,
        ReservedMask = 0x80000000,
    }

    [Flags]
    public enum BGFX_CapsFlags : ulong
    {
        /// <summary>
        /// Alpha to coverage is supported.
        /// </summary>
        AlphaToCoverage = 0x0000000000000001,

        /// <summary>
        /// Blend independent is supported.
        /// </summary>
        BlendIndependent = 0x0000000000000002,

        /// <summary>
        /// Compute shaders are supported.
        /// </summary>
        Compute = 0x0000000000000004,

        /// <summary>
        /// Conservative rasterization is supported.
        /// </summary>
        ConservativeRaster = 0x0000000000000008,

        /// <summary>
        /// Draw indirect is supported.
        /// </summary>
        DrawIndirect = 0x0000000000000010,

        /// <summary>
        /// Fragment depth is available in fragment shader.
        /// </summary>
        FragmentDepth = 0x0000000000000020,

        /// <summary>
        /// Fragment ordering is available in fragment shader.
        /// </summary>
        FragmentOrdering = 0x0000000000000040,

        /// <summary>
        /// Graphics debugger is present.
        /// </summary>
        GraphicsDebugger = 0x0000000000000080,

        /// <summary>
        /// HDR10 rendering is supported.
        /// </summary>
        Hdr10 = 0x0000000000000100,

        /// <summary>
        /// HiDPI rendering is supported.
        /// </summary>
        Hidpi = 0x0000000000000200,

        /// <summary>
        /// Image Read/Write is supported.
        /// </summary>
        ImageRw = 0x0000000000000400,

        /// <summary>
        /// 32-bit indices are supported.
        /// </summary>
        Index32 = 0x0000000000000800,

        /// <summary>
        /// Instancing is supported.
        /// </summary>
        Instancing = 0x0000000000001000,

        /// <summary>
        /// Occlusion query is supported.
        /// </summary>
        OcclusionQuery = 0x0000000000002000,

        /// <summary>
        /// Renderer is on separate thread.
        /// </summary>
        RendererMultithreaded = 0x0000000000004000,

        /// <summary>
        /// Multiple windows are supported.
        /// </summary>
        SwapChain = 0x0000000000008000,

        /// <summary>
        /// 2D texture array is supported.
        /// </summary>
        Texture2dArray = 0x0000000000010000,

        /// <summary>
        /// 3D textures are supported.
        /// </summary>
        Texture3d = 0x0000000000020000,

        /// <summary>
        /// Texture blit is supported.
        /// </summary>
        TextureBlit = 0x0000000000040000,
        TextureCompareReserved = 0x0000000000080000,

        /// <summary>
        /// Texture compare less equal mode is supported.
        /// </summary>
        TextureCompareLequal = 0x0000000000100000,

        /// <summary>
        /// Cubemap texture array is supported.
        /// </summary>
        TextureCubeArray = 0x0000000000200000,

        /// <summary>
        /// CPU direct access to GPU texture memory.
        /// </summary>
        TextureDirectAccess = 0x0000000000400000,

        /// <summary>
        /// Read-back texture is supported.
        /// </summary>
        TextureReadBack = 0x0000000000800000,

        /// <summary>
        /// Vertex attribute half-float is supported.
        /// </summary>
        VertexAttribHalf = 0x0000000001000000,

        /// <summary>
        /// Vertex attribute 10_10_10_2 is supported.
        /// </summary>
        VertexAttribUint10 = 0x0000000002000000,

        /// <summary>
        /// Rendering with VertexID only is supported.
        /// </summary>
        VertexId = 0x0000000004000000,

        /// <summary>
        /// Viewport layer is available in vertex shader.
        /// </summary>
        ViewportLayerArray = 0x0000000008000000,

        /// <summary>
        /// All texture compare modes are supported.
        /// </summary>
        TextureCompareAll = 0x0000000000180000,
    }

    [Flags]
    public enum BGFX_CapsFormatFlags : uint
    {
        /// <summary>
        /// Texture format is not supported.
        /// </summary>
        TextureNone = 0x00000000,

        /// <summary>
        /// Texture format is supported.
        /// </summary>
        Texture2d = 0x00000001,

        /// <summary>
        /// Texture as sRGB format is supported.
        /// </summary>
        Texture2dSrgb = 0x00000002,

        /// <summary>
        /// Texture format is emulated.
        /// </summary>
        Texture2dEmulated = 0x00000004,

        /// <summary>
        /// Texture format is supported.
        /// </summary>
        Texture3d = 0x00000008,

        /// <summary>
        /// Texture as sRGB format is supported.
        /// </summary>
        Texture3dSrgb = 0x00000010,

        /// <summary>
        /// Texture format is emulated.
        /// </summary>
        Texture3dEmulated = 0x00000020,

        /// <summary>
        /// Texture format is supported.
        /// </summary>
        TextureCube = 0x00000040,

        /// <summary>
        /// Texture as sRGB format is supported.
        /// </summary>
        TextureCubeSrgb = 0x00000080,

        /// <summary>
        /// Texture format is emulated.
        /// </summary>
        TextureCubeEmulated = 0x00000100,

        /// <summary>
        /// Texture format can be used from vertex shader.
        /// </summary>
        TextureVertex = 0x00000200,

        /// <summary>
        /// Texture format can be used as image and read from.
        /// </summary>
        TextureImageRead = 0x00000400,

        /// <summary>
        /// Texture format can be used as image and written to.
        /// </summary>
        TextureImageWrite = 0x00000800,

        /// <summary>
        /// Texture format can be used as frame buffer.
        /// </summary>
        TextureFramebuffer = 0x00001000,

        /// <summary>
        /// Texture format can be used as MSAA frame buffer.
        /// </summary>
        TextureFramebufferMsaa = 0x00002000,

        /// <summary>
        /// Texture can be sampled as MSAA.
        /// </summary>
        TextureMsaa = 0x00004000,

        /// <summary>
        /// Texture format supports auto-generated mips.
        /// </summary>
        TextureMipAutogen = 0x00008000,
    }

    [Flags]
    public enum BGFX_ResolveFlags : uint
    {
        /// <summary>
        /// No resolve flags.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Auto-generate mip maps on resolve.
        /// </summary>
        AutoGenMips = 0x00000001,
    }

    [Flags]
    public enum BGFX_PciIdFlags : ushort
    {
        /// <summary>
        /// Autoselect adapter.
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// Software rasterizer.
        /// </summary>
        SoftwareRasterizer = 0x0001,

        /// <summary>
        /// AMD adapter.
        /// </summary>
        Amd = 0x1002,

        /// <summary>
        /// Intel adapter.
        /// </summary>
        Intel = 0x8086,

        /// <summary>
        /// nVidia adapter.
        /// </summary>
        Nvidia = 0x10de,
    }

    [Flags]
    public enum BGFX_CubeMapFlags : uint
    {
        /// <summary>
        /// Cubemap +x.
        /// </summary>
        PositiveX = 0x00000000,

        /// <summary>
        /// Cubemap -x.
        /// </summary>
        NegativeX = 0x00000001,

        /// <summary>
        /// Cubemap +y.
        /// </summary>
        PositiveY = 0x00000002,

        /// <summary>
        /// Cubemap -y.
        /// </summary>
        NegativeY = 0x00000003,

        /// <summary>
        /// Cubemap +z.
        /// </summary>
        PositiveZ = 0x00000004,

        /// <summary>
        /// Cubemap -z.
        /// </summary>
        NegativeZ = 0x00000005,
    }

    public enum BGFX_Fatal
    {
        DebugCheck,
        InvalidShader,
        UnableToInitialize,
        UnableToCreateTexture,
        DeviceLost,

        Count
    }

    public enum BGFX_RendererType
    {
        /// <summary>
        /// No rendering.
        /// </summary>
        Noop,

        /// <summary>
        /// AGC
        /// </summary>
        Agc,

        /// <summary>
        /// Direct3D 9.0
        /// </summary>
        Direct3D9,

        /// <summary>
        /// Direct3D 11.0
        /// </summary>
        Direct3D11,

        /// <summary>
        /// Direct3D 12.0
        /// </summary>
        Direct3D12,

        /// <summary>
        /// GNM
        /// </summary>
        Gnm,

        /// <summary>
        /// Metal
        /// </summary>
        Metal,

        /// <summary>
        /// NVN
        /// </summary>
        Nvn,

        /// <summary>
        /// OpenGL ES 2.0+
        /// </summary>
        OpenGLES,

        /// <summary>
        /// OpenGL 2.1+
        /// </summary>
        OpenGL,

        /// <summary>
        /// Vulkan
        /// </summary>
        Vulkan,

        /// <summary>
        /// WebGPU
        /// </summary>
        WebGPU,

        Count
    }

    public enum BGFX_Access
    {
        /// <summary>
        /// Read.
        /// </summary>
        Read,

        /// <summary>
        /// Write.
        /// </summary>
        Write,

        /// <summary>
        /// Read and write.
        /// </summary>
        ReadWrite,

        Count
    }

    public enum BGFX_Attrib
    {
        /// <summary>
        /// a_position
        /// </summary>
        Position,

        /// <summary>
        /// a_normal
        /// </summary>
        Normal,

        /// <summary>
        /// a_tangent
        /// </summary>
        Tangent,

        /// <summary>
        /// a_bitangent
        /// </summary>
        Bitangent,

        /// <summary>
        /// a_color0
        /// </summary>
        Color0,

        /// <summary>
        /// a_color1
        /// </summary>
        Color1,

        /// <summary>
        /// a_color2
        /// </summary>
        Color2,

        /// <summary>
        /// a_color3
        /// </summary>
        Color3,

        /// <summary>
        /// a_indices
        /// </summary>
        Indices,

        /// <summary>
        /// a_weight
        /// </summary>
        Weight,

        /// <summary>
        /// a_texcoord0
        /// </summary>
        TexCoord0,

        /// <summary>
        /// a_texcoord1
        /// </summary>
        TexCoord1,

        /// <summary>
        /// a_texcoord2
        /// </summary>
        TexCoord2,

        /// <summary>
        /// a_texcoord3
        /// </summary>
        TexCoord3,

        /// <summary>
        /// a_texcoord4
        /// </summary>
        TexCoord4,

        /// <summary>
        /// a_texcoord5
        /// </summary>
        TexCoord5,

        /// <summary>
        /// a_texcoord6
        /// </summary>
        TexCoord6,

        /// <summary>
        /// a_texcoord7
        /// </summary>
        TexCoord7,

        Count
    }

    public enum BGFX_AttribType
    {
        /// <summary>
        /// Uint8
        /// </summary>
        Uint8,

        /// <summary>
        /// Uint10, availability depends on: `BGFX_CAPS_VERTEX_ATTRIB_UINT10`.
        /// </summary>
        Uint10,

        /// <summary>
        /// Int16
        /// </summary>
        Int16,

        /// <summary>
        /// Half, availability depends on: `BGFX_CAPS_VERTEX_ATTRIB_HALF`.
        /// </summary>
        Half,

        /// <summary>
        /// Float
        /// </summary>
        Float,

        Count
    }

    public enum BGFX_TextureFormat
    {
        /// <summary>
        /// DXT1 R5G6B5A1
        /// </summary>
        BC1,

        /// <summary>
        /// DXT3 R5G6B5A4
        /// </summary>
        BC2,

        /// <summary>
        /// DXT5 R5G6B5A8
        /// </summary>
        BC3,

        /// <summary>
        /// LATC1/ATI1 R8
        /// </summary>
        BC4,

        /// <summary>
        /// LATC2/ATI2 RG8
        /// </summary>
        BC5,

        /// <summary>
        /// BC6H RGB16F
        /// </summary>
        BC6H,

        /// <summary>
        /// BC7 RGB 4-7 bits per color channel, 0-8 bits alpha
        /// </summary>
        BC7,

        /// <summary>
        /// ETC1 RGB8
        /// </summary>
        ETC1,

        /// <summary>
        /// ETC2 RGB8
        /// </summary>
        ETC2,

        /// <summary>
        /// ETC2 RGBA8
        /// </summary>
        ETC2A,

        /// <summary>
        /// ETC2 RGB8A1
        /// </summary>
        ETC2A1,

        /// <summary>
        /// PVRTC1 RGB 2BPP
        /// </summary>
        PTC12,

        /// <summary>
        /// PVRTC1 RGB 4BPP
        /// </summary>
        PTC14,

        /// <summary>
        /// PVRTC1 RGBA 2BPP
        /// </summary>
        PTC12A,

        /// <summary>
        /// PVRTC1 RGBA 4BPP
        /// </summary>
        PTC14A,

        /// <summary>
        /// PVRTC2 RGBA 2BPP
        /// </summary>
        PTC22,

        /// <summary>
        /// PVRTC2 RGBA 4BPP
        /// </summary>
        PTC24,

        /// <summary>
        /// ATC RGB 4BPP
        /// </summary>
        ATC,

        /// <summary>
        /// ATCE RGBA 8 BPP explicit alpha
        /// </summary>
        ATCE,

        /// <summary>
        /// ATCI RGBA 8 BPP interpolated alpha
        /// </summary>
        ATCI,

        /// <summary>
        /// ASTC 4x4 8.0 BPP
        /// </summary>
        ASTC4x4,

        /// <summary>
        /// ASTC 5x5 5.12 BPP
        /// </summary>
        ASTC5x5,

        /// <summary>
        /// ASTC 6x6 3.56 BPP
        /// </summary>
        ASTC6x6,

        /// <summary>
        /// ASTC 8x5 3.20 BPP
        /// </summary>
        ASTC8x5,

        /// <summary>
        /// ASTC 8x6 2.67 BPP
        /// </summary>
        ASTC8x6,

        /// <summary>
        /// ASTC 10x5 2.56 BPP
        /// </summary>
        ASTC10x5,

        /// <summary>
        /// Compressed formats above.
        /// </summary>
        Unknown,
        R1,
        A8,
        R8,
        R8I,
        R8U,
        R8S,
        R16,
        R16I,
        R16U,
        R16F,
        R16S,
        R32I,
        R32U,
        R32F,
        RG8,
        RG8I,
        RG8U,
        RG8S,
        RG16,
        RG16I,
        RG16U,
        RG16F,
        RG16S,
        RG32I,
        RG32U,
        RG32F,
        RGB8,
        RGB8I,
        RGB8U,
        RGB8S,
        RGB9E5F,
        BGRA8,
        RGBA8,
        RGBA8I,
        RGBA8U,
        RGBA8S,
        RGBA16,
        RGBA16I,
        RGBA16U,
        RGBA16F,
        RGBA16S,
        RGBA32I,
        RGBA32U,
        RGBA32F,
        R5G6B5,
        RGBA4,
        RGB5A1,
        RGB10A2,
        RG11B10F,

        /// <summary>
        /// Depth formats below.
        /// </summary>
        UnknownDepth,
        D16,
        D24,
        D24S8,
        D32,
        D16F,
        D24F,
        D32F,
        D0S8,

        Count
    }

    public enum BGFX_UniformType
    {
        /// <summary>
        /// Sampler.
        /// </summary>
        Sampler,

        /// <summary>
        /// Reserved, do not use.
        /// </summary>
        End,

        /// <summary>
        /// 4 floats vector.
        /// </summary>
        Vec4,

        /// <summary>
        /// 3x3 matrix.
        /// </summary>
        Mat3,

        /// <summary>
        /// 4x4 matrix.
        /// </summary>
        Mat4,

        Count
    }

    public enum BGFX_BackbufferRatio
    {
        /// <summary>
        /// Equal to backbuffer.
        /// </summary>
        Equal,

        /// <summary>
        /// One half size of backbuffer.
        /// </summary>
        Half,

        /// <summary>
        /// One quarter size of backbuffer.
        /// </summary>
        Quarter,

        /// <summary>
        /// One eighth size of backbuffer.
        /// </summary>
        Eighth,

        /// <summary>
        /// One sixteenth size of backbuffer.
        /// </summary>
        Sixteenth,

        /// <summary>
        /// Double size of backbuffer.
        /// </summary>
        Double,

        Count
    }

    public enum BGFX_OcclusionQueryResult
    {
        /// <summary>
        /// Query failed test.
        /// </summary>
        Invisible,

        /// <summary>
        /// Query passed test.
        /// </summary>
        Visible,

        /// <summary>
        /// Query result is not available yet.
        /// </summary>
        NoResult,

        Count
    }

    public enum BGFX_Topology
    {
        /// <summary>
        /// Triangle list.
        /// </summary>
        TriList,

        /// <summary>
        /// Triangle strip.
        /// </summary>
        TriStrip,

        /// <summary>
        /// Line list.
        /// </summary>
        LineList,

        /// <summary>
        /// Line strip.
        /// </summary>
        LineStrip,

        /// <summary>
        /// Point list.
        /// </summary>
        PointList,

        Count
    }

    public enum BGFX_TopologyConvertOptions
    {
        /// <summary>
        /// Flip winding order of triangle list.
        /// </summary>
        TriListFlipWinding,

        /// <summary>
        /// Flip winding order of triangle strip.
        /// </summary>
        TriStripFlipWinding,

        /// <summary>
        /// Convert triangle list to line list.
        /// </summary>
        TriListToLineList,

        /// <summary>
        /// Convert triangle strip to triangle list.
        /// </summary>
        TriStripToTriList,

        /// <summary>
        /// Convert line strip to line list.
        /// </summary>
        LineStripToLineList,

        Count
    }

    public enum BGFX_TopologySort
    {
        DirectionFrontToBackMin,
        DirectionFrontToBackAvg,
        DirectionFrontToBackMax,
        DirectionBackToFrontMin,
        DirectionBackToFrontAvg,
        DirectionBackToFrontMax,
        DistanceFrontToBackMin,
        DistanceFrontToBackAvg,
        DistanceFrontToBackMax,
        DistanceBackToFrontMin,
        DistanceBackToFrontAvg,
        DistanceBackToFrontMax,

        Count
    }

    public enum BGFX_ViewMode
    {
        /// <summary>
        /// Default sort order.
        /// </summary>
        Default,

        /// <summary>
        /// Sort in the same order in which submit calls were called.
        /// </summary>
        Sequential,

        /// <summary>
        /// Sort draw call depth in ascending order.
        /// </summary>
        DepthAscending,

        /// <summary>
        /// Sort draw call depth in descending order.
        /// </summary>
        DepthDescending,

        Count
    }

    public enum BGFX_RenderFrameState
    {
        /// <summary>
        /// Renderer context is not created yet.
        /// </summary>
        NoContext,

        /// <summary>
        /// Renderer context is created and rendering.
        /// </summary>
        Render,

        /// <summary>
        /// Renderer context wait for main thread signal timed out without rendering.
        /// </summary>
        Timeout,

        /// <summary>
        /// Renderer context is getting destroyed.
        /// </summary>
        Exiting,

        Count
    }

    public unsafe struct BGFX_Caps
    {
        public unsafe struct GPU
        {
            public ushort vendorId;
            public ushort deviceId;
        }

        public unsafe struct Limits
        {
            public uint maxDrawCalls;
            public uint maxBlits;
            public uint maxTextureSize;
            public uint maxTextureLayers;
            public uint maxViews;
            public uint maxFrameBuffers;
            public uint maxFBAttachments;
            public uint maxPrograms;
            public uint maxShaders;
            public uint maxTextures;
            public uint maxTextureSamplers;
            public uint maxComputeBindings;
            public uint maxVertexLayouts;
            public uint maxVertexStreams;
            public uint maxIndexBuffers;
            public uint maxVertexBuffers;
            public uint maxDynamicIndexBuffers;
            public uint maxDynamicVertexBuffers;
            public uint maxUniforms;
            public uint maxOcclusionQueries;
            public uint maxEncoders;
            public uint minResourceCbSize;
            public uint transientVbSize;
            public uint transientIbSize;
        }

        public BGFX_RendererType rendererType;
        public ulong supported;
        public ushort vendorId;
        public ushort deviceId;
        public byte homogeneousDepth;
        public byte originBottomLeft;
        public byte numGPUs;
        public fixed uint gpu[4];
        public Limits limits;
        public fixed ushort formats[85];
    }

    public unsafe struct BGFX_InternalData
    {
        public BGFX_Caps* caps;
        public void* context;
    }

    public unsafe struct BGFX_PlatformData
    {
        public void* ndt;
        public void* nwh;
        public void* context;
        public void* backBuffer;
        public void* backBufferDS;
    }

    public unsafe struct BGFX_Resolution
    {
        public BGFX_TextureFormat format;
        public uint width;
        public uint height;
        public uint reset;
        public byte numBackBuffers;
        public byte maxFrameLatency;
    }

    public unsafe struct BGFX_InitData
    {
        public unsafe struct Limits
        {
            public ushort maxEncoders;
            public uint minResourceCbSize;
            public uint transientVbSize;
            public uint transientIbSize;
        }

        public BGFX_RendererType type;
        public ushort vendorId;
        public ushort deviceId;
        public ulong capabilities;
        public byte debug;
        public byte profile;
        public BGFX_PlatformData platformData;
        public BGFX_Resolution resolution;
        public Limits limits;
        public IntPtr callback;
        public IntPtr allocator;
    }

    public unsafe struct BGFX_Memory
    {
        public byte* data;
        public uint size;
    }

    public unsafe struct BGFX_TransientIndexBuffer
    {
        public byte* data;
        public uint size;
        public uint startIndex;
        public BGFX_IndexBufferHandle handle;
        public byte isIndex16;
    }

    public unsafe struct BGFX_TransientVertexBuffer
    {
        public byte* data;
        public uint size;
        public uint startVertex;
        public ushort stride;
        public BGFX_VertexBufferHandle handle;
        public BGFX_VertexLayoutHandle layoutHandle;
    }

    public unsafe struct BGFX_InstanceDataBuffer
    {
        public byte* data;
        public uint size;
        public uint offset;
        public uint num;
        public ushort stride;
        public BGFX_VertexBufferHandle handle;
    }

    public unsafe struct BGFX_TextureInfo
    {
        public BGFX_TextureFormat format;
        public uint storageSize;
        public ushort width;
        public ushort height;
        public ushort depth;
        public ushort numLayers;
        public byte numMips;
        public byte bitsPerPixel;
        public byte cubeMap;
    }

    public unsafe struct BGFX_UniformInfo
    {
        public fixed byte name[256];
        public BGFX_UniformType type;
        public ushort num;
    }

    public unsafe struct BGFX_Attachment
    {
        public BGFX_Access access;
        public BGFX_TextureHandle handle;
        public ushort mip;
        public ushort layer;
        public ushort numLayers;
        public byte resolve;
    }

    public unsafe struct BGFX_Transform
    {
        public float* data;
        public ushort num;
    }

    public unsafe struct BGFX_ViewStats
    {
        public fixed byte name[256];
        public ushort view;
        public long cpuTimeBegin;
        public long cpuTimeEnd;
        public long gpuTimeBegin;
        public long gpuTimeEnd;
    }

    public unsafe struct BGFX_EncoderStats
    {
        public long cpuTimeBegin;
        public long cpuTimeEnd;
    }

    public unsafe struct BGFX_Stats
    {
        public long cpuTimeFrame;
        public long cpuTimeBegin;
        public long cpuTimeEnd;
        public long cpuTimerFreq;
        public long gpuTimeBegin;
        public long gpuTimeEnd;
        public long gpuTimerFreq;
        public long waitRender;
        public long waitSubmit;
        public uint numDraw;
        public uint numCompute;
        public uint numBlit;
        public uint maxGpuLatency;
        public ushort numDynamicIndexBuffers;
        public ushort numDynamicVertexBuffers;
        public ushort numFrameBuffers;
        public ushort numIndexBuffers;
        public ushort numOcclusionQueries;
        public ushort numPrograms;
        public ushort numShaders;
        public ushort numTextures;
        public ushort numUniforms;
        public ushort numVertexBuffers;
        public ushort numVertexLayouts;
        public long textureMemoryUsed;
        public long rtMemoryUsed;
        public int transientVbUsed;
        public int transientIbUsed;
        public fixed uint numPrims[5];
        public long gpuMemoryMax;
        public long gpuMemoryUsed;
        public ushort width;
        public ushort height;
        public ushort textWidth;
        public ushort textHeight;
        public ushort numViews;
        public BGFX_ViewStats* viewStats;
        public byte numEncoders;
        public BGFX_EncoderStats* encoderStats;
    }

    public unsafe struct BGFX_VertexLayout
    {
        public uint hash;
        public ushort stride;
        public fixed ushort offset[18];
        public fixed ushort attributes[18];
    }

    public struct BGFX_DynamicIndexBufferHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_DynamicVertexBufferHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_FrameBufferHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_IndexBufferHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_IndirectBufferHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_OcclusionQueryHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_ProgramHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_ShaderHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_TextureHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_UniformHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_VertexBufferHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }

    public struct BGFX_VertexLayoutHandle
    {
        public ushort idx;
        public bool Valid => idx != ushort.MaxValue;
    }


    /// <summary>
    /// Init attachment.
    /// </summary>
    ///
    /// <param name="_handle">Render target texture handle.</param>
    /// <param name="_access">Access. See `Access::Enum`.</param>
    /// <param name="_layer">Cubemap side or depth layer/slice to use.</param>
    /// <param name="_numLayers">Number of texture layer/slice(s) in array to use.</param>
    /// <param name="_mip">Mip level.</param>
    /// <param name="_resolve">Resolve flags. See: `BGFX_RESOLVE_*`</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_attachment_init", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_AttachmentInit(BGFX_Attachment* _this, BGFX_TextureHandle _handle, BGFX_Access _access, ushort _layer, ushort _numLayers, ushort _mip, byte _resolve);

    /// <summary>
    /// Start VertexLayout.
    /// </summary>
    ///
    /// <param name="_rendererType">Renderer backend type. See: `bgfx::RendererType`</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_layout_begin", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_VertexLayout* BGFX_VertexLayoutBegin(BGFX_VertexLayout* _this, BGFX_RendererType _rendererType);

    /// <summary>
    /// Add attribute to VertexLayout.
    /// @remarks Must be called between begin/end.
    /// </summary>
    ///
    /// <param name="_attrib">Attribute semantics. See: `bgfx::Attrib`</param>
    /// <param name="_num">Number of elements 1, 2, 3 or 4.</param>
    /// <param name="_type">Element type.</param>
    /// <param name="_normalized">When using fixed point AttribType (f.e. Uint8) value will be normalized for vertex shader usage. When normalized is set to true, AttribType::Uint8 value in range 0-255 will be in range 0.0-1.0 in vertex shader.</param>
    /// <param name="_asInt">Packaging rule for vertexPack, vertexUnpack, and vertexConvert for AttribType::Uint8 and AttribType::Int16. Unpacking code must be implemented inside vertex shader.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_layout_add", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_VertexLayout* BGFX_VertexLayoutAdd(BGFX_VertexLayout* _this, BGFX_Attrib _attrib, byte _num, BGFX_AttribType _type, bool _normalized, bool _asInt);

    /// <summary>
    /// Decode attribute.
    /// </summary>
    ///
    /// <param name="_attrib">Attribute semantics. See: `bgfx::Attrib`</param>
    /// <param name="_num">Number of elements.</param>
    /// <param name="_type">Element type.</param>
    /// <param name="_normalized">Attribute is normalized.</param>
    /// <param name="_asInt">Attribute is packed as int.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_layout_decode", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_VertexLayoutDecode(BGFX_VertexLayout* _this, BGFX_Attrib _attrib, byte* _num, BGFX_AttribType* _type, bool* _normalized, bool* _asInt);

    /// <summary>
    /// Returns `true` if VertexLayout contains attribute.
    /// </summary>
    ///
    /// <param name="_attrib">Attribute semantics. See: `bgfx::Attrib`</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_layout_has", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern unsafe bool BGFX_VertexLayoutHas(BGFX_VertexLayout* _this, BGFX_Attrib _attrib);

    /// <summary>
    /// Skip `_num` bytes in vertex stream.
    /// </summary>
    ///
    /// <param name="_num">Number of bytes to skip.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_layout_skip", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_VertexLayout* BGFX_VertexLayoutSkip(BGFX_VertexLayout* _this, byte _num);

    /// <summary>
    /// End VertexLayout.
    /// </summary>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_layout_end", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_VertexLayoutEnd(BGFX_VertexLayout* _this);

    /// <summary>
    /// Pack vertex attribute into vertex stream format.
    /// </summary>
    ///
    /// <param name="_input">Value to be packed into vertex stream.</param>
    /// <param name="_inputNormalized">`true` if input value is already normalized.</param>
    /// <param name="_attr">Attribute to pack.</param>
    /// <param name="_layout">Vertex stream layout.</param>
    /// <param name="_data">Destination vertex stream where data will be packed.</param>
    /// <param name="_index">Vertex index that will be modified.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_pack", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_VertexPack(float _input, bool _inputNormalized, BGFX_Attrib _attr, BGFX_VertexLayout* _layout, void* _data, uint _index);

    /// <summary>
    /// Unpack vertex attribute from vertex stream format.
    /// </summary>
    ///
    /// <param name="_output">Result of unpacking.</param>
    /// <param name="_attr">Attribute to unpack.</param>
    /// <param name="_layout">Vertex stream layout.</param>
    /// <param name="_data">Source vertex stream from where data will be unpacked.</param>
    /// <param name="_index">Vertex index that will be unpacked.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_unpack", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_VertexUnpack(float _output, BGFX_Attrib _attr, BGFX_VertexLayout* _layout, void* _data, uint _index);

    /// <summary>
    /// Converts vertex stream data from one vertex stream format to another.
    /// </summary>
    ///
    /// <param name="_dstLayout">Destination vertex stream layout.</param>
    /// <param name="_dstData">Destination vertex stream.</param>
    /// <param name="_srcLayout">Source vertex stream layout.</param>
    /// <param name="_srcData">Source vertex stream data.</param>
    /// <param name="_num">Number of vertices to convert from source to destination.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_vertex_convert", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_VertexConvert(BGFX_VertexLayout* _dstLayout, void* _dstData, BGFX_VertexLayout* _srcLayout, void* _srcData, uint _num);

    /// <summary>
    /// Weld vertices.
    /// </summary>
    ///
    /// <param name="_output">Welded vertices remapping table. The size of buffer must be the same as number of vertices.</param>
    /// <param name="_layout">Vertex stream layout.</param>
    /// <param name="_data">Vertex stream.</param>
    /// <param name="_num">Number of vertices in vertex stream.</param>
    /// <param name="_index32">Set to `true` if input indices are 32-bit.</param>
    /// <param name="_epsilon">Error tolerance for vertex position comparison.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_weld_vertices", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_WeldVertices(void* _output, BGFX_VertexLayout* _layout, void* _data, uint _num, bool _index32, float _epsilon);

    /// <summary>
    /// Convert index buffer for use with different primitive topologies.
    /// </summary>
    ///
    /// <param name="_conversion">Conversion type, see `TopologyConvert::Enum`.</param>
    /// <param name="_dst">Destination index buffer. If this argument is NULL function will return number of indices after conversion.</param>
    /// <param name="_dstSize">Destination index buffer in bytes. It must be large enough to contain output indices. If destination size is insufficient index buffer will be truncated.</param>
    /// <param name="_indices">Source indices.</param>
    /// <param name="_numIndices">Number of input indices.</param>
    /// <param name="_index32">Set to `true` if input indices are 32-bit.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_topology_convert", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_TopologyConvert(BGFX_TopologyConvertOptions _conversion, void* _dst, uint _dstSize, void* _indices, uint _numIndices, bool _index32);

    /// <summary>
    /// Sort indices.
    /// </summary>
    ///
    /// <param name="_sort">Sort order, see `TopologySort::Enum`.</param>
    /// <param name="_dst">Destination index buffer.</param>
    /// <param name="_dstSize">Destination index buffer in bytes. It must be large enough to contain output indices. If destination size is insufficient index buffer will be truncated.</param>
    /// <param name="_dir">Direction (vector must be normalized).</param>
    /// <param name="_pos">Position.</param>
    /// <param name="_vertices">Pointer to first vertex represented as float x, y, z. Must contain at least number of vertices referencende by index buffer.</param>
    /// <param name="_stride">Vertex stride.</param>
    /// <param name="_indices">Source indices.</param>
    /// <param name="_numIndices">Number of input indices.</param>
    /// <param name="_index32">Set to `true` if input indices are 32-bit.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_topology_sort_tri_list", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_TopologySortTriList(BGFX_TopologySort _sort, void* _dst, uint _dstSize, float _dir, float _pos, void* _vertices, uint _stride, void* _indices, uint _numIndices, bool _index32);

    /// <summary>
    /// Returns supported backend API renderers.
    /// </summary>
    ///
    /// <param name="_max">Maximum number of elements in _enum array.</param>
    /// <param name="_enum">Array where supported renderers will be written.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_supported_renderers", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe byte BGFX_GetSupportedRenderers(byte _max, [In, MarshalAs(UnmanagedType.LPArray)] BGFX_RendererType[] _enum);

    /// <summary>
    /// Returns name of renderer.
    /// </summary>
    ///
    /// <param name="_type">Renderer backend type. See: `bgfx::RendererType`</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_renderer_name", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe IntPtr BGFX_GetRendererName(BGFX_RendererType _type);

    [DllImport(DllName, EntryPoint = "bgfx_init_ctor", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_InitCtor(BGFX_InitData* _init);

    /// <summary>
    /// Initialize bgfx library.
    /// </summary>
    ///
    /// <param name="_init">Initialization parameters. See: `bgfx::Init` for more info.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_init", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern unsafe bool BGFX_Init(BGFX_InitData* _init);

    /// <summary>
    /// Shutdown bgfx library.
    /// </summary>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_shutdown", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_Shutdown();

    /// <summary>
    /// Reset graphic settings and back-buffer size.
    /// @attention This call doesn't actually change window size, it just
    ///   resizes back-buffer. Windowing code has to change window size.
    /// </summary>
    ///
    /// <param name="_width">Back-buffer width.</param>
    /// <param name="_height">Back-buffer height.</param>
    /// <param name="_flags">See: `BGFX_RESET_*` for more info.   - `BGFX_RESET_NONE` - No reset flags.   - `BGFX_RESET_FULLSCREEN` - Not supported yet.   - `BGFX_RESET_MSAA_X[2/4/8/16]` - Enable 2, 4, 8 or 16 x MSAA.   - `BGFX_RESET_VSYNC` - Enable V-Sync.   - `BGFX_RESET_MAXANISOTROPY` - Turn on/off max anisotropy.   - `BGFX_RESET_CAPTURE` - Begin screen capture.   - `BGFX_RESET_FLUSH_AFTER_RENDER` - Flush rendering after submitting to GPU.   - `BGFX_RESET_FLIP_AFTER_RENDER` - This flag  specifies where flip     occurs. Default behaviour is that flip occurs before rendering new     frame. This flag only has effect when `BGFX_CONFIG_MULTITHREADED=0`.   - `BGFX_RESET_SRGB_BACKBUFFER` - Enable sRGB backbuffer.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_reset", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_Reset(int width, int height, BGFX_ResetFlags flags, BGFX_TextureFormat format);

    /// <summary>
    /// Advance to next frame. When using multithreaded renderer, this call
    /// just swaps internal buffers, kicks render thread, and returns. In
    /// singlethreaded renderer this call does frame rendering.
    /// </summary>
    ///
    /// <param name="_capture">Capture frame with graphics debugger.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_frame", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_Frame(bool _capture = false);

    /// <summary>
    /// Returns current renderer backend API type.
    /// @remarks
    ///   Library must be initialized.
    /// </summary>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_renderer_type", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_RendererType BGFX_GetRendererType();

    /// <summary>
    /// Returns renderer capabilities.
    /// @remarks
    ///   Library must be initialized.
    /// </summary>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_caps", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_Caps* BGFX_GetCaps();

    /// <summary>
    /// Returns performance counters.
    /// @attention Pointer returned is valid until `bgfx::frame` is called.
    /// </summary>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_stats", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_Stats* BGFX_GetStats();

    /// <summary>
    /// Allocate buffer to pass to bgfx calls. Data will be freed inside bgfx.
    /// </summary>
    ///
    /// <param name="_size">Size to allocate.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_alloc", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_Memory* BGFX_Alloc(uint _size);

    /// <summary>
    /// Allocate buffer and copy data into it. Data will be freed inside bgfx.
    /// </summary>
    ///
    /// <param name="_data">Pointer to data to be copied.</param>
    /// <param name="_size">Size of data to be copied.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_copy", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_Memory* BGFX_Copy(void* _data, uint _size);

    /// <summary>
    /// Make reference to data to pass to bgfx. Unlike `bgfx::alloc`, this call
    /// doesn't allocate memory for data. It just copies the _data pointer. You
    /// can pass `ReleaseFn` function pointer to release this memory after it's
    /// consumed, otherwise you must make sure _data is available for at least 2
    /// `bgfx::frame` calls. `ReleaseFn` function must be able to be called
    /// from any thread.
    /// @attention Data passed must be available for at least 2 `bgfx::frame` calls.
    /// </summary>
    ///
    /// <param name="_data">Pointer to data.</param>
    /// <param name="_size">Size of data.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_make_ref", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_Memory* BGFX_MakeRef(void* _data, uint _size);

    /// <summary>
    /// Make reference to data to pass to bgfx. Unlike `bgfx::alloc`, this call
    /// doesn't allocate memory for data. It just copies the _data pointer. You
    /// can pass `ReleaseFn` function pointer to release this memory after it's
    /// consumed, otherwise you must make sure _data is available for at least 2
    /// `bgfx::frame` calls. `ReleaseFn` function must be able to be called
    /// from any thread.
    /// @attention Data passed must be available for at least 2 `bgfx::frame` calls.
    /// </summary>
    ///
    /// <param name="_data">Pointer to data.</param>
    /// <param name="_size">Size of data.</param>
    /// <param name="_releaseFn">Callback function to release memory after use.</param>
    /// <param name="_userData">User data to be passed to callback function.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_make_ref_release", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_Memory* BGFX_MakeRefRelease(void* _data, uint _size, IntPtr _releaseFn, void* _userData);

    /// <summary>
    /// Set debug flags.
    /// </summary>
    ///
    /// <param name="_debug">Available flags:   - `BGFX_DEBUG_IFH` - Infinitely fast hardware. When this flag is set     all rendering calls will be skipped. This is useful when profiling     to quickly assess potential bottlenecks between CPU and GPU.   - `BGFX_DEBUG_PROFILER` - Enable profiler.   - `BGFX_DEBUG_STATS` - Display internal statistics.   - `BGFX_DEBUG_TEXT` - Display debug text.   - `BGFX_DEBUG_WIREFRAME` - Wireframe rendering. All rendering     primitives will be rendered as lines.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_debug", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetDebug(BGFX_DebugFlags _debug);

    /// <summary>
    /// Clear internal debug text buffer.
    /// </summary>
    ///
    /// <param name="_attr">Background color.</param>
    /// <param name="_small">Default 8x16 or 8x8 font.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_dbg_text_clear", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DebugTextClear(byte _attr, bool _small);

    /// <summary>
    /// Print formatted data to internal debug text character-buffer (VGA-compatible text mode).
    /// </summary>
    ///
    /// <param name="_x">Position x from the left corner of the window.</param>
    /// <param name="_y">Position y from the top corner of the window.</param>
    /// <param name="_attr">Color palette. Where top 4-bits represent index of background, and bottom 4-bits represent foreground color from standard VGA text palette (ANSI escape codes).</param>
    /// <param name="_format">`printf` style format.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_dbg_text_printf", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DebugTextPrintf(ushort _x, ushort _y, byte _attr, [MarshalAs(UnmanagedType.LPStr)] string _format, [MarshalAs(UnmanagedType.LPStr)] string args);

    /// <summary>
    /// Print formatted data from variable argument list to internal debug text character-buffer (VGA-compatible text mode).
    /// </summary>
    ///
    /// <param name="_x">Position x from the left corner of the window.</param>
    /// <param name="_y">Position y from the top corner of the window.</param>
    /// <param name="_attr">Color palette. Where top 4-bits represent index of background, and bottom 4-bits represent foreground color from standard VGA text palette (ANSI escape codes).</param>
    /// <param name="_format">`printf` style format.</param>
    /// <param name="_argList">Variable arguments list for format string.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_dbg_text_vprintf", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DebugTextVPrintf(ushort _x, ushort _y, byte _attr, [MarshalAs(UnmanagedType.LPStr)] string _format, IntPtr _argList);

    /// <summary>
    /// Draw image into internal debug text buffer.
    /// </summary>
    ///
    /// <param name="_x">Position x from the left corner of the window.</param>
    /// <param name="_y">Position y from the top corner of the window.</param>
    /// <param name="_width">Image width.</param>
    /// <param name="_height">Image height.</param>
    /// <param name="_data">Raw image data (character/attribute raw encoding).</param>
    /// <param name="_pitch">Image pitch in bytes.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_dbg_text_image", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DebugTextImage(ushort _x, ushort _y, ushort _width, ushort _height, void* _data, ushort _pitch);

    /// <summary>
    /// Create static index buffer.
    /// </summary>
    ///
    /// <param name="_mem">Index buffer data.</param>
    /// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_IndexBufferHandle BGFX_CreateIndexBuffer(BGFX_Memory* _mem, BGFX_BufferFlags _flags);

    /// <summary>
    /// Set static index buffer debug name.
    /// </summary>
    ///
    /// <param name="_handle">Static index buffer handle.</param>
    /// <param name="_name">Static index buffer name.</param>
    /// <param name="_len">Static index buffer name length (if length is INT32_MAX, it's expected that _name is zero terminated string.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_index_buffer_name", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetIndexBufferName(BGFX_IndexBufferHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

    /// <summary>
    /// Destroy static index buffer.
    /// </summary>
    ///
    /// <param name="_handle">Static index buffer handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyIndexBuffer(BGFX_IndexBufferHandle _handle);

    /// <summary>
    /// Create vertex layout.
    /// </summary>
    ///
    /// <param name="_layout">Vertex layout.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_vertex_layout", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_VertexLayoutHandle BGFX_CreateVertexLayout(BGFX_VertexLayout* _layout);

    /// <summary>
    /// Destroy vertex layout.
    /// </summary>
    ///
    /// <param name="_layoutHandle">Vertex layout handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_vertex_layout", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyVertexLayout(BGFX_VertexLayoutHandle _layoutHandle);

    /// <summary>
    /// Create static vertex buffer.
    /// </summary>
    ///
    /// <param name="_mem">Vertex buffer data.</param>
    /// <param name="_layout">Vertex layout.</param>
    /// <param name="_flags">Buffer creation flags.  - `BGFX_BUFFER_NONE` - No flags.  - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.  - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer      is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.  - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.  - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of      data is passed. If this flag is not specified, and more data is passed on update, the buffer      will be trimmed to fit the existing buffer size. This flag has effect only on dynamic buffers.  - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on index buffers.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_VertexBufferHandle BGFX_CreateVertexBuffer(BGFX_Memory* _mem, BGFX_VertexLayout* _layout, BGFX_BufferFlags _flags);

    /// <summary>
    /// Set static vertex buffer debug name.
    /// </summary>
    ///
    /// <param name="_handle">Static vertex buffer handle.</param>
    /// <param name="_name">Static vertex buffer name.</param>
    /// <param name="_len">Static vertex buffer name length (if length is INT32_MAX, it's expected that _name is zero terminated string.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_vertex_buffer_name", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetVertexBufferName(BGFX_VertexBufferHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

    /// <summary>
    /// Destroy static vertex buffer.
    /// </summary>
    ///
    /// <param name="_handle">Static vertex buffer handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyVertexBuffer(BGFX_VertexBufferHandle _handle);

    /// <summary>
    /// Create empty dynamic index buffer.
    /// </summary>
    ///
    /// <param name="_num">Number of indices.</param>
    /// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_DynamicIndexBufferHandle BGFX_CreateDynamicIndexBuffer(uint _num, BGFX_BufferFlags _flags);

    /// <summary>
    /// Create dynamic index buffer and initialized it.
    /// </summary>
    ///
    /// <param name="_mem">Index buffer data.</param>
    /// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_dynamic_index_buffer_mem", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_DynamicIndexBufferHandle BGFX_CreateDynamicIndexBufferFrom(BGFX_Memory* _mem, BGFX_BufferFlags _flags);

    /// <summary>
    /// Update dynamic index buffer.
    /// </summary>
    ///
    /// <param name="_handle">Dynamic index buffer handle.</param>
    /// <param name="_startIndex">Start index.</param>
    /// <param name="_mem">Index buffer data.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_update_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_UpdateDynamicIndexBuffer(BGFX_DynamicIndexBufferHandle _handle, uint _startIndex, BGFX_Memory* _mem);

    /// <summary>
    /// Destroy dynamic index buffer.
    /// </summary>
    ///
    /// <param name="_handle">Dynamic index buffer handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyDynamicIndexBuffer(BGFX_DynamicIndexBufferHandle _handle);

    /// <summary>
    /// Create empty dynamic vertex buffer.
    /// </summary>
    ///
    /// <param name="_num">Number of vertices.</param>
    /// <param name="_layout">Vertex layout.</param>
    /// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_DynamicVertexBufferHandle BGFX_CreateDynamicVertexBuffer(uint _num, BGFX_VertexLayout* _layout, BGFX_BufferFlags _flags);

    /// <summary>
    /// Create dynamic vertex buffer and initialize it.
    /// </summary>
    ///
    /// <param name="_mem">Vertex buffer data.</param>
    /// <param name="_layout">Vertex layout.</param>
    /// <param name="_flags">Buffer creation flags.   - `BGFX_BUFFER_NONE` - No flags.   - `BGFX_BUFFER_COMPUTE_READ` - Buffer will be read from by compute shader.   - `BGFX_BUFFER_COMPUTE_WRITE` - Buffer will be written into by compute shader. When buffer       is created with `BGFX_BUFFER_COMPUTE_WRITE` flag it cannot be updated from CPU.   - `BGFX_BUFFER_COMPUTE_READ_WRITE` - Buffer will be used for read/write by compute shader.   - `BGFX_BUFFER_ALLOW_RESIZE` - Buffer will resize on buffer update if a different amount of       data is passed. If this flag is not specified, and more data is passed on update, the buffer       will be trimmed to fit the existing buffer size. This flag has effect only on dynamic       buffers.   - `BGFX_BUFFER_INDEX32` - Buffer is using 32-bit indices. This flag has effect only on       index buffers.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_dynamic_vertex_buffer_mem", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_DynamicVertexBufferHandle BGFX_CreateDynamicVertexBufferFrom(BGFX_Memory* _mem, BGFX_VertexLayout* _layout, BGFX_BufferFlags _flags);

    /// <summary>
    /// Update dynamic vertex buffer.
    /// </summary>
    ///
    /// <param name="_handle">Dynamic vertex buffer handle.</param>
    /// <param name="_startVertex">Start vertex.</param>
    /// <param name="_mem">Vertex buffer data.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_update_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_UpdateDynamicVertexBuffer(BGFX_DynamicVertexBufferHandle _handle, uint _startVertex, BGFX_Memory* _mem);

    /// <summary>
    /// Destroy dynamic vertex buffer.
    /// </summary>
    ///
    /// <param name="_handle">Dynamic vertex buffer handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyDynamicVertexBuffer(BGFX_DynamicVertexBufferHandle _handle);

    /// <summary>
    /// Returns number of requested or maximum available indices.
    /// </summary>
    ///
    /// <param name="_num">Number of required indices.</param>
    /// <param name="_index32">Set to `true` if input indices will be 32-bit.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_avail_transient_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_GetAvailableTransidentIndexBuffer(uint _num, bool _index32);

    /// <summary>
    /// Returns number of requested or maximum available vertices.
    /// </summary>
    ///
    /// <param name="_num">Number of required vertices.</param>
    /// <param name="_layout">Vertex layout.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_avail_transient_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_GetAvailableTransientVertexBuffer(uint _num, BGFX_VertexLayout* _layout);

    /// <summary>
    /// Returns number of requested or maximum available instance buffer slots.
    /// </summary>
    ///
    /// <param name="_num">Number of required instances.</param>
    /// <param name="_stride">Stride per instance.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_avail_instance_data_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_GetAvailableInstanceDataBuffer(uint _num, ushort _stride);

    /// <summary>
    /// Allocate transient index buffer.
    /// </summary>
    ///
    /// <param name="_tib">TransientIndexBuffer structure is filled and is valid for the duration of frame, and it can be reused for multiple draw calls.</param>
    /// <param name="_num">Number of indices to allocate.</param>
    /// <param name="_index32">Set to `true` if input indices will be 32-bit.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_alloc_transient_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_AllocTransientIndexBuffer(BGFX_TransientIndexBuffer* _tib, uint _num, bool _index32);

    /// <summary>
    /// Allocate transient vertex buffer.
    /// </summary>
    ///
    /// <param name="_tvb">TransientVertexBuffer structure is filled and is valid for the duration of frame, and it can be reused for multiple draw calls.</param>
    /// <param name="_num">Number of vertices to allocate.</param>
    /// <param name="_layout">Vertex layout.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_alloc_transient_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_AllocTransientVertexBuffer(BGFX_TransientVertexBuffer* _tvb, uint _num, BGFX_VertexLayout* _layout);

    /// <summary>
    /// Check for required space and allocate transient vertex and index
    /// buffers. If both space requirements are satisfied function returns
    /// true.
    /// </summary>
    ///
    /// <param name="_tvb">TransientVertexBuffer structure is filled and is valid for the duration of frame, and it can be reused for multiple draw calls.</param>
    /// <param name="_layout">Vertex layout.</param>
    /// <param name="_numVertices">Number of vertices to allocate.</param>
    /// <param name="_tib">TransientIndexBuffer structure is filled and is valid for the duration of frame, and it can be reused for multiple draw calls.</param>
    /// <param name="_numIndices">Number of indices to allocate.</param>
    /// <param name="_index32">Set to `true` if input indices will be 32-bit.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_alloc_transient_buffers", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern unsafe bool BGFX_AllocTransientBuffers(BGFX_TransientVertexBuffer* _tvb, BGFX_VertexLayout* _layout, uint _numVertices, BGFX_TransientIndexBuffer* _tib, uint _numIndices, bool _index32);

    /// <summary>
    /// Allocate instance data buffer.
    /// </summary>
    ///
    /// <param name="_idb">InstanceDataBuffer structure is filled and is valid for duration of frame, and it can be reused for multiple draw calls.</param>
    /// <param name="_num">Number of instances.</param>
    /// <param name="_stride">Instance stride. Must be multiple of 16.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_alloc_instance_data_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_AllocInstanceDataBuffer(BGFX_InstanceDataBuffer* _idb, uint _num, ushort _stride);

    /// <summary>
    /// Create draw indirect buffer.
    /// </summary>
    ///
    /// <param name="_num">Number of indirect calls.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_indirect_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_IndirectBufferHandle BGFX_CreateIndirectBuffer(uint _num);

    /// <summary>
    /// Destroy draw indirect buffer.
    /// </summary>
    ///
    /// <param name="_handle">Indirect buffer handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_indirect_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyIndirectBuffer(BGFX_IndirectBufferHandle _handle);

    /// <summary>
    /// Create shader from memory buffer.
    /// </summary>
    ///
    /// <param name="_mem">Shader binary.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_shader", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_ShaderHandle BGFX_CreateShader(BGFX_Memory* _mem);

    /// <summary>
    /// Returns the number of uniforms and uniform handles used inside a shader.
    /// @remarks
    ///   Only non-predefined uniforms are returned.
    /// </summary>
    ///
    /// <param name="_handle">Shader handle.</param>
    /// <param name="_uniforms">UniformHandle array where data will be stored.</param>
    /// <param name="_max">Maximum capacity of array.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_shader_uniforms", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe ushort BGFX_GetShaderUniforms(BGFX_ShaderHandle _handle, BGFX_UniformHandle* _uniforms, ushort _max);

    /// <summary>
    /// Set shader debug name.
    /// </summary>
    ///
    /// <param name="_handle">Shader handle.</param>
    /// <param name="_name">Shader name.</param>
    /// <param name="_len">Shader name length (if length is INT32_MAX, it's expected that _name is zero terminated string).</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_shader_name", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetShaderName(BGFX_ShaderHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

    /// <summary>
    /// Destroy shader.
    /// @remark Once a shader program is created with _handle,
    ///   it is safe to destroy that shader.
    /// </summary>
    ///
    /// <param name="_handle">Shader handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_shader", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyShader(BGFX_ShaderHandle _handle);

    /// <summary>
    /// Create program with vertex and fragment shaders.
    /// </summary>
    ///
    /// <param name="_vsh">Vertex shader.</param>
    /// <param name="_fsh">Fragment shader.</param>
    /// <param name="_destroyShaders">If true, shaders will be destroyed when program is destroyed.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_program", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_ProgramHandle BGFX_CreateShaderProgram(BGFX_ShaderHandle _vsh, BGFX_ShaderHandle _fsh, bool _destroyShaders);

    /// <summary>
    /// Create program with compute shader.
    /// </summary>
    ///
    /// <param name="_csh">Compute shader.</param>
    /// <param name="_destroyShaders">If true, shaders will be destroyed when program is destroyed.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_compute_program", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_ProgramHandle BGFX_CreateComputeProgram(BGFX_ShaderHandle _csh, bool _destroyShaders);

    /// <summary>
    /// Destroy program.
    /// </summary>
    ///
    /// <param name="_handle">Program handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_program", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyShaderProgram(BGFX_ProgramHandle _handle);

    /// <summary>
    /// Validate texture parameters.
    /// </summary>
    ///
    /// <param name="_depth">Depth dimension of volume texture.</param>
    /// <param name="_cubeMap">Indicates that texture contains cubemap.</param>
    /// <param name="_numLayers">Number of layers in texture array.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    /// <param name="_flags">Texture flags. See `BGFX_TEXTURE_*`.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_is_texture_valid", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern unsafe bool BGFX_IsTextureValid(ushort _depth, bool _cubeMap, ushort _numLayers, BGFX_TextureFormat _format, ulong _flags);

    /// <summary>
    /// Validate frame buffer parameters.
    /// </summary>
    ///
    /// <param name="_num">Number of attachments.</param>
    /// <param name="_attachment">Attachment texture info. See: `bgfx::Attachment`.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_is_frame_buffer_valid", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern unsafe bool BGFX_IsFrameBufferValid(byte _num, BGFX_Attachment* _attachment);

    /// <summary>
    /// Calculate amount of memory required for texture.
    /// </summary>
    ///
    /// <param name="_info">Resulting texture info structure. See: `TextureInfo`.</param>
    /// <param name="_width">Width.</param>
    /// <param name="_height">Height.</param>
    /// <param name="_depth">Depth dimension of volume texture.</param>
    /// <param name="_cubeMap">Indicates that texture contains cubemap.</param>
    /// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
    /// <param name="_numLayers">Number of layers in texture array.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_calc_texture_size", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_CalcTextureSize(BGFX_TextureInfo* _info, ushort _width, ushort _height, ushort _depth, bool _cubeMap, bool _hasMips, ushort _numLayers, BGFX_TextureFormat _format);

    /// <summary>
    /// Create texture from memory buffer.
    /// </summary>
    ///
    /// <param name="_mem">DDS, KTX or PVR texture binary data.</param>
    /// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
    /// <param name="_skip">Skip top level mips when parsing texture.</param>
    /// <param name="_info">When non-`NULL` is specified it returns parsed texture information.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_texture", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_TextureHandle BGFX_CreateTexture(BGFX_Memory* _mem, ulong _flags, byte _skip, BGFX_TextureInfo* _info);

    /// <summary>
    /// Create 2D texture.
    /// </summary>
    ///
    /// <param name="_width">Width.</param>
    /// <param name="_height">Height.</param>
    /// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
    /// <param name="_numLayers">Number of layers in texture array. Must be 1 if caps `BGFX_CAPS_TEXTURE_2D_ARRAY` flag is not set.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    /// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
    /// <param name="_mem">Texture data. If `_mem` is non-NULL, created texture will be immutable. If `_mem` is NULL content of the texture is uninitialized. When `_numLayers` is more than 1, expected memory layout is texture and all mips together for each array element.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_texture_2d", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_TextureHandle BGFX_CreateTexture2D(int _width, int _height, bool _hasMips, int _numLayers, BGFX_TextureFormat _format, BGFX_SamplerFlags _flags, BGFX_Memory* _mem);

    /// <summary>
    /// Create texture with size based on backbuffer ratio. Texture will maintain ratio
    /// if back buffer resolution changes.
    /// </summary>
    ///
    /// <param name="_ratio">Texture size in respect to back-buffer size. See: `BackbufferRatio::Enum`.</param>
    /// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
    /// <param name="_numLayers">Number of layers in texture array. Must be 1 if caps `BGFX_CAPS_TEXTURE_2D_ARRAY` flag is not set.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    /// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_texture_2d_scaled", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_TextureHandle BGFX_CreateTexture2DScaled(BGFX_BackbufferRatio _ratio, bool _hasMips, ushort _numLayers, BGFX_TextureFormat _format, ulong _flags);

    /// <summary>
    /// Create 3D texture.
    /// </summary>
    ///
    /// <param name="_width">Width.</param>
    /// <param name="_height">Height.</param>
    /// <param name="_depth">Depth.</param>
    /// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    /// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
    /// <param name="_mem">Texture data. If `_mem` is non-NULL, created texture will be immutable. If `_mem` is NULL content of the texture is uninitialized. When `_numLayers` is more than 1, expected memory layout is texture and all mips together for each array element.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_texture_3d", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_TextureHandle BGFX_CreateTexture3D(ushort _width, ushort _height, ushort _depth, bool _hasMips, BGFX_TextureFormat _format, ulong _flags, BGFX_Memory* _mem);

    /// <summary>
    /// Create Cube texture.
    /// </summary>
    ///
    /// <param name="_size">Cube side size.</param>
    /// <param name="_hasMips">Indicates that texture contains full mip-map chain.</param>
    /// <param name="_numLayers">Number of layers in texture array. Must be 1 if caps `BGFX_CAPS_TEXTURE_2D_ARRAY` flag is not set.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    /// <param name="_flags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
    /// <param name="_mem">Texture data. If `_mem` is non-NULL, created texture will be immutable. If `_mem` is NULL content of the texture is uninitialized. When `_numLayers` is more than 1, expected memory layout is texture and all mips together for each array element.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_texture_cube", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_TextureHandle BGFX_CreateTextureCube(ushort _size, bool _hasMips, ushort _numLayers, BGFX_TextureFormat _format, ulong _flags, BGFX_Memory* _mem);

    /// <summary>
    /// Update 2D texture.
    /// @attention It's valid to update only mutable texture. See `bgfx::createTexture2D` for more info.
    /// </summary>
    ///
    /// <param name="_handle">Texture handle.</param>
    /// <param name="_layer">Layer in texture array.</param>
    /// <param name="_mip">Mip level.</param>
    /// <param name="_x">X offset in texture.</param>
    /// <param name="_y">Y offset in texture.</param>
    /// <param name="_width">Width of texture block.</param>
    /// <param name="_height">Height of texture block.</param>
    /// <param name="_mem">Texture update data.</param>
    /// <param name="_pitch">Pitch of input image (bytes). When _pitch is set to UINT16_MAX, it will be calculated internally based on _width.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_update_texture_2d", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_UpdateTexture2D(BGFX_TextureHandle _handle, ushort _layer, byte _mip, ushort _x, ushort _y, ushort _width, ushort _height, BGFX_Memory* _mem, ushort _pitch);

    /// <summary>
    /// Update 3D texture.
    /// @attention It's valid to update only mutable texture. See `bgfx::createTexture3D` for more info.
    /// </summary>
    ///
    /// <param name="_handle">Texture handle.</param>
    /// <param name="_mip">Mip level.</param>
    /// <param name="_x">X offset in texture.</param>
    /// <param name="_y">Y offset in texture.</param>
    /// <param name="_z">Z offset in texture.</param>
    /// <param name="_width">Width of texture block.</param>
    /// <param name="_height">Height of texture block.</param>
    /// <param name="_depth">Depth of texture block.</param>
    /// <param name="_mem">Texture update data.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_update_texture_3d", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_UpdateTexture3D(BGFX_TextureHandle _handle, byte _mip, ushort _x, ushort _y, ushort _z, ushort _width, ushort _height, ushort _depth, BGFX_Memory* _mem);

    /// <summary>
    /// Update Cube texture.
    /// @attention It's valid to update only mutable texture. See `bgfx::createTextureCube` for more info.
    /// </summary>
    ///
    /// <param name="_handle">Texture handle.</param>
    /// <param name="_layer">Layer in texture array.</param>
    /// <param name="_side">Cubemap side `BGFX_CUBE_MAP_<POSITIVE or NEGATIVE>_<X, Y or Z>`,   where 0 is +X, 1 is -X, 2 is +Y, 3 is -Y, 4 is +Z, and 5 is -Z.                  +----------+                  |-z       2|                  | ^  +y    |                  | |        |    Unfolded cube:                  | +---->+x |       +----------+----------+----------+----------+       |+y       1|+y       4|+y       0|+y       5|       | ^  -x    | ^  +z    | ^  +x    | ^  -z    |       | |        | |        | |        | |        |       | +---->+z | +---->+x | +---->-z | +---->-x |       +----------+----------+----------+----------+                  |+z       3|                  | ^  -y    |                  | |        |                  | +---->+x |                  +----------+</param>
    /// <param name="_mip">Mip level.</param>
    /// <param name="_x">X offset in texture.</param>
    /// <param name="_y">Y offset in texture.</param>
    /// <param name="_width">Width of texture block.</param>
    /// <param name="_height">Height of texture block.</param>
    /// <param name="_mem">Texture update data.</param>
    /// <param name="_pitch">Pitch of input image (bytes). When _pitch is set to UINT16_MAX, it will be calculated internally based on _width.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_update_texture_cube", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_UpdateTextureCube(BGFX_TextureHandle _handle, ushort _layer, byte _side, byte _mip, ushort _x, ushort _y, ushort _width, ushort _height, BGFX_Memory* _mem, ushort _pitch);

    /// <summary>
    /// Read back texture content.
    /// @attention Texture must be created with `BGFX_TEXTURE_READ_BACK` flag.
    /// @attention Availability depends on: `BGFX_CAPS_TEXTURE_READ_BACK`.
    /// </summary>
    ///
    /// <param name="_handle">Texture handle.</param>
    /// <param name="_data">Destination buffer.</param>
    /// <param name="_mip">Mip level.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_read_texture", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_ReadTexture(BGFX_TextureHandle _handle, void* _data, byte _mip);

    /// <summary>
    /// Set texture debug name.
    /// </summary>
    ///
    /// <param name="_handle">Texture handle.</param>
    /// <param name="_name">Texture name.</param>
    /// <param name="_len">Texture name length (if length is INT32_MAX, it's expected that _name is zero terminated string.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_texture_name", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetTextureName(BGFX_TextureHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

    /// <summary>
    /// Returns texture direct access pointer.
    /// @attention Availability depends on: `BGFX_CAPS_TEXTURE_DIRECT_ACCESS`. This feature
    ///   is available on GPUs that have unified memory architecture (UMA) support.
    /// </summary>
    ///
    /// <param name="_handle">Texture handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_direct_access_ptr", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void* BGFX_GetTextureDirectAccessPtr(BGFX_TextureHandle _handle);

    /// <summary>
    /// Destroy texture.
    /// </summary>
    ///
    /// <param name="_handle">Texture handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_texture", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyTexture(BGFX_TextureHandle _handle);

    /// <summary>
    /// Create frame buffer (simple).
    /// </summary>
    ///
    /// <param name="_width">Texture width.</param>
    /// <param name="_height">Texture height.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    /// <param name="_textureFlags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_FrameBufferHandle BGFX_CreateFrameBuffer(ushort _width, ushort _height, BGFX_TextureFormat _format, ulong _textureFlags);

    /// <summary>
    /// Create frame buffer with size based on backbuffer ratio. Frame buffer will maintain ratio
    /// if back buffer resolution changes.
    /// </summary>
    ///
    /// <param name="_ratio">Frame buffer size in respect to back-buffer size. See: `BackbufferRatio::Enum`.</param>
    /// <param name="_format">Texture format. See: `TextureFormat::Enum`.</param>
    /// <param name="_textureFlags">Texture creation (see `BGFX_TEXTURE_*`.), and sampler (see `BGFX_SAMPLER_*`) flags. Default texture sampling mode is linear, and wrap mode is repeat. - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap   mode. - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic   sampling.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer_scaled", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_FrameBufferHandle BGFX_CreateFrameBufferScaled(BGFX_BackbufferRatio _ratio, BGFX_TextureFormat _format, ulong _textureFlags);

    /// <summary>
    /// Create MRT frame buffer from texture handles (simple).
    /// </summary>
    ///
    /// <param name="_num">Number of texture handles.</param>
    /// <param name="_handles">Texture attachments.</param>
    /// <param name="_destroyTexture">If true, textures will be destroyed when frame buffer is destroyed.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer_from_handles", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_FrameBufferHandle BGFX_CreateFrameBufferFromTextureHandles(byte _num, BGFX_TextureHandle* _handles, bool _destroyTexture);

    /// <summary>
    /// Create MRT frame buffer from texture handles with specific layer and
    /// mip level.
    /// </summary>
    ///
    /// <param name="_num">Number of attachments.</param>
    /// <param name="_attachment">Attachment texture info. See: `bgfx::Attachment`.</param>
    /// <param name="_destroyTexture">If true, textures will be destroyed when frame buffer is destroyed.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer_from_attachment", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_FrameBufferHandle BGFX_CreateFrameBufferFromAttachment(byte _num, BGFX_Attachment* _attachment, bool _destroyTexture);

    /// <summary>
    /// Create frame buffer for multiple window rendering.
    /// @remarks
    ///   Frame buffer cannot be used for sampling.
    /// @attention Availability depends on: `BGFX_CAPS_SWAP_CHAIN`.
    /// </summary>
    ///
    /// <param name="_nwh">OS' target native window handle.</param>
    /// <param name="_width">Window back buffer width.</param>
    /// <param name="_height">Window back buffer height.</param>
    /// <param name="_format">Window back buffer color format.</param>
    /// <param name="_depthFormat">Window back buffer depth format.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_frame_buffer_from_nwh", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_FrameBufferHandle BGFX_CreateFrameBufferFromNativeWindowHandle(void* _nwh, ushort _width, ushort _height, BGFX_TextureFormat _format, BGFX_TextureFormat _depthFormat);

    /// <summary>
    /// Set frame buffer debug name.
    /// </summary>
    ///
    /// <param name="_handle">Frame buffer handle.</param>
    /// <param name="_name">Frame buffer name.</param>
    /// <param name="_len">Frame buffer name length (if length is INT32_MAX, it's expected that _name is zero terminated string.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_frame_buffer_name", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetFrameBufferName(BGFX_FrameBufferHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _name, int _len);

    /// <summary>
    /// Obtain texture handle of frame buffer attachment.
    /// </summary>
    ///
    /// <param name="_handle">Frame buffer handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_texture", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_TextureHandle BGFX_GetTextureFromFrameBuffer(BGFX_FrameBufferHandle _handle, byte _attachment);

    /// <summary>
    /// Destroy frame buffer.
    /// </summary>
    ///
    /// <param name="_handle">Frame buffer handle.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_frame_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyFrameBuffer(BGFX_FrameBufferHandle _handle);

    /// <summary>
    /// Create shader uniform parameter.
    /// @remarks
    ///   1. Uniform names are unique. It's valid to call `bgfx::createUniform`
    ///      multiple times with the same uniform name. The library will always
    ///      return the same handle, but the handle reference count will be
    ///      incremented. This means that the same number of `bgfx::destroyUniform`
    ///      must be called to properly destroy the uniform.
    ///   2. Predefined uniforms (declared in `bgfx_shader.sh`):
    ///      - `u_viewRect vec4(x, y, width, height)` - view rectangle for current
    ///        view, in pixels.
    ///      - `u_viewTexel vec4(1.0/width, 1.0/height, undef, undef)` - inverse
    ///        width and height
    ///      - `u_view mat4` - view matrix
    ///      - `u_invView mat4` - inverted view matrix
    ///      - `u_proj mat4` - projection matrix
    ///      - `u_invProj mat4` - inverted projection matrix
    ///      - `u_viewProj mat4` - concatenated view projection matrix
    ///      - `u_invViewProj mat4` - concatenated inverted view projection matrix
    ///      - `u_model mat4[BGFX_CONFIG_MAX_BONES]` - array of model matrices.
    ///      - `u_modelView mat4` - concatenated model view matrix, only first
    ///        model matrix from array is used.
    ///      - `u_modelViewProj mat4` - concatenated model view projection matrix.
    ///      - `u_alphaRef float` - alpha reference value for alpha test.
    /// </summary>
    ///
    /// <param name="_name">Uniform name in shader.</param>
    /// <param name="_type">Type of uniform (See: `bgfx::UniformType`).</param>
    /// <param name="_num">Number of elements in array.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_uniform", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_UniformHandle BGFX_CreateShaderUniform([MarshalAs(UnmanagedType.LPStr)] string _name, BGFX_UniformType _type, ushort _num);

    /// <summary>
    /// Retrieve uniform info.
    /// </summary>
    ///
    /// <param name="_handle">Handle to uniform object.</param>
    /// <param name="_info">Uniform info.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_uniform_info", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_GetShaderUniformInfo(BGFX_UniformHandle _handle, BGFX_UniformInfo* _info);

    /// <summary>
    /// Destroy shader uniform parameter.
    /// </summary>
    ///
    /// <param name="_handle">Handle to uniform object.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_uniform", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyShaderUniform(BGFX_UniformHandle _handle);

    /// <summary>
    /// Create occlusion query.
    /// </summary>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_create_occlusion_query", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_OcclusionQueryHandle BGFX_CreateOcclusionQuery();

    /// <summary>
    /// Retrieve occlusion query result from previous frame.
    /// </summary>
    ///
    /// <param name="_handle">Handle to occlusion query object.</param>
    /// <param name="_result">Number of pixels that passed test. This argument can be `NULL` if result of occlusion query is not needed.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_get_result", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_OcclusionQueryResult BGFX_GetOcclusionQueryResult(BGFX_OcclusionQueryHandle _handle, int* _result);

    /// <summary>
    /// Destroy occlusion query.
    /// </summary>
    ///
    /// <param name="_handle">Handle to occlusion query object.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_destroy_occlusion_query", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_DestroyOcclusionQuery(BGFX_OcclusionQueryHandle _handle);

    /// <summary>
    /// Set palette color value.
    /// </summary>
    ///
    /// <param name="_index">Index into palette.</param>
    /// <param name="_rgba">RGBA floating point values.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_palette_color", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetPaletteColor(byte _index, float _rgba);

    /// <summary>
    /// Set palette color value.
    /// </summary>
    ///
    /// <param name="_index">Index into palette.</param>
    /// <param name="_rgba">Packed 32-bit RGBA value.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_palette_color_rgba8", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetPaletteColorRgba8(byte _index, uint _rgba);

    /// <summary>
    /// Set view name.
    /// @remarks
    ///   This is debug only feature.
    ///   In graphics debugger view name will appear as:
    ///       "nnnc <view name>"
    ///        ^  ^ ^
    ///        |  +--- compute (C)
    ///        +------ view id
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_name">View name.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_name", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewName(ushort _id, [MarshalAs(UnmanagedType.LPStr)] string _name);

    /// <summary>
    /// Set view rectangle. Draw primitive outside view will be clipped.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_x">Position x from the left corner of the window.</param>
    /// <param name="_y">Position y from the top corner of the window.</param>
    /// <param name="_width">Width of view port region.</param>
    /// <param name="_height">Height of view port region.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_rect", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewRect(ushort _id, int _x, int _y, int _width, int _height);

    /// <summary>
    /// Set view rectangle. Draw primitive outside view will be clipped.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_x">Position x from the left corner of the window.</param>
    /// <param name="_y">Position y from the top corner of the window.</param>
    /// <param name="_ratio">Width and height will be set in respect to back-buffer size. See: `BackbufferRatio::Enum`.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_rect_ratio", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewRectRatio(ushort _id, ushort _x, ushort _y, BGFX_BackbufferRatio _ratio);

    /// <summary>
    /// Set view scissor. Draw primitive outside view will be clipped. When
    /// _x, _y, _width and _height are set to 0, scissor will be disabled.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_x">Position x from the left corner of the window.</param>
    /// <param name="_y">Position y from the top corner of the window.</param>
    /// <param name="_width">Width of view scissor region.</param>
    /// <param name="_height">Height of view scissor region.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_scissor", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewScissor(ushort _id, ushort _x, ushort _y, ushort _width, ushort _height);

    /// <summary>
    /// Set view clear flags.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_flags">Clear flags. Use `BGFX_CLEAR_NONE` to remove any clear operation. See: `BGFX_CLEAR_*`.</param>
    /// <param name="_rgba">Color clear value.</param>
    /// <param name="_depth">Depth clear value.</param>
    /// <param name="_stencil">Stencil clear value.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_clear", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewClear(ushort _id, BGFX_ClearFlags _flags, uint _rgba, float _depth, byte _stencil);

    /// <summary>
    /// Set view clear flags with different clear color for each
    /// frame buffer texture. Must use `bgfx::setPaletteColor` to setup clear color
    /// palette.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_flags">Clear flags. Use `BGFX_CLEAR_NONE` to remove any clear operation. See: `BGFX_CLEAR_*`.</param>
    /// <param name="_depth">Depth clear value.</param>
    /// <param name="_stencil">Stencil clear value.</param>
    /// <param name="_c0">Palette index for frame buffer attachment 0.</param>
    /// <param name="_c1">Palette index for frame buffer attachment 1.</param>
    /// <param name="_c2">Palette index for frame buffer attachment 2.</param>
    /// <param name="_c3">Palette index for frame buffer attachment 3.</param>
    /// <param name="_c4">Palette index for frame buffer attachment 4.</param>
    /// <param name="_c5">Palette index for frame buffer attachment 5.</param>
    /// <param name="_c6">Palette index for frame buffer attachment 6.</param>
    /// <param name="_c7">Palette index for frame buffer attachment 7.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_clear_mrt", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewClearMultipleRenderTargets(ushort _id, ushort _flags, float _depth, byte _stencil, byte _c0, byte _c1, byte _c2, byte _c3, byte _c4, byte _c5, byte _c6, byte _c7);

    /// <summary>
    /// Set view sorting mode.
    /// @remarks
    ///   View mode must be set prior calling `bgfx::submit` for the view.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_mode">View sort mode. See `ViewMode::Enum`.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_mode", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewMode(ushort _id, BGFX_ViewMode _mode);

    /// <summary>
    /// Set view frame buffer.
    /// @remarks
    ///   Not persistent after `bgfx::reset` call.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_handle">Frame buffer handle. Passing `BGFX_INVALID_HANDLE` as frame buffer handle will draw primitives from this view into default back buffer.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_frame_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewFrameBuffer(ushort _id, BGFX_FrameBufferHandle _handle);

    /// <summary>
    /// Set view view and projection matrices, all draw primitives in this
    /// view will use these matrices.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_view">View matrix.</param>
    /// <param name="_proj">Projection matrix.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_transform", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewTransform(ushort _id, void* _view, void* _proj);

    /// <summary>
    /// Post submit view reordering.
    /// </summary>
    ///
    /// <param name="_id">First view id.</param>
    /// <param name="_num">Number of views to remap.</param>
    /// <param name="_order">View remap id table. Passing `NULL` will reset view ids to default state.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_view_order", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetViewOrder(ushort _id, ushort _num, ushort* _order);

    /// <summary>
    /// Reset all view settings to default.
    /// </summary>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_reset_view", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_ResetView(ushort _id);

    /// <summary>
    /// Request screen shot of window back buffer.
    /// @remarks
    ///   `bgfx::CallbackI::screenShot` must be implemented.
    /// @attention Frame buffer handle must be created with OS' target native window handle.
    /// </summary>
    ///
    /// <param name="_handle">Frame buffer handle. If handle is `BGFX_INVALID_HANDLE` request will be made for main window back buffer.</param>
    /// <param name="_filePath">Will be passed to `bgfx::CallbackI::screenShot` callback.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_request_screen_shot", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_RequestScreenshot(BGFX_FrameBufferHandle _handle, [MarshalAs(UnmanagedType.LPStr)] string _filePath);

    /// <summary>
    /// Render frame.
    /// @attention `bgfx::renderFrame` is blocking call. It waits for
    ///   `bgfx::frame` to be called from API thread to process frame.
    ///   If timeout value is passed call will timeout and return even
    ///   if `bgfx::frame` is not called.
    /// @warning This call should be only used on platforms that don't
    ///   allow creating separate rendering thread. If it is called before
    ///   to bgfx::init, render thread won't be created by bgfx::init call.
    /// </summary>
    ///
    /// <param name="_msecs">Timeout in milliseconds.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_render_frame", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe BGFX_RenderFrameState BGFX_RenderFrame(int _msecs);

    /// <summary>
    /// Set platform data.
    /// @warning Must be called before `bgfx::init`.
    /// </summary>
    ///
    /// <param name="_data">Platform data.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_platform_data", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetPlatformData(BGFX_PlatformData* _data);

    /// <summary>
    /// Set render states for draw primitive.
    /// @remarks
    ///   1. To setup more complex states use:
    ///      `BGFX_STATE_ALPHA_REF(_ref)`,
    ///      `BGFX_STATE_POINT_SIZE(_size)`,
    ///      `BGFX_STATE_BLEND_FUNC(_src, _dst)`,
    ///      `BGFX_STATE_BLEND_FUNC_SEPARATE(_srcRGB, _dstRGB, _srcA, _dstA)`,
    ///      `BGFX_STATE_BLEND_EQUATION(_equation)`,
    ///      `BGFX_STATE_BLEND_EQUATION_SEPARATE(_equationRGB, _equationA)`
    ///   2. `BGFX_STATE_BLEND_EQUATION_ADD` is set when no other blend
    ///      equation is specified.
    /// </summary>
    ///
    /// <param name="_state">State flags. Default state for primitive type is   triangles. See: `BGFX_STATE_DEFAULT`.   - `BGFX_STATE_DEPTH_TEST_*` - Depth test function.   - `BGFX_STATE_BLEND_*` - See remark 1 about BGFX_STATE_BLEND_FUNC.   - `BGFX_STATE_BLEND_EQUATION_*` - See remark 2.   - `BGFX_STATE_CULL_*` - Backface culling mode.   - `BGFX_STATE_WRITE_*` - Enable R, G, B, A or Z write.   - `BGFX_STATE_MSAA` - Enable hardware multisample antialiasing.   - `BGFX_STATE_PT_[TRISTRIP/LINES/POINTS]` - Primitive type.</param>
    /// <param name="_rgba">Sets blend factor used by `BGFX_STATE_BLEND_FACTOR` and   `BGFX_STATE_BLEND_INV_FACTOR` blend modes.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_state", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetRenderState(BGFX_StateFlags flags, uint _rgba);

    /// <summary>
    /// Set condition for rendering.
    /// </summary>
    ///
    /// <param name="_handle">Occlusion query handle.</param>
    /// <param name="_visible">Render if occlusion query is visible.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_condition", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetOcclusion(BGFX_OcclusionQueryHandle _handle, bool _visible);

    /// <summary>
    /// Set stencil test state.
    /// </summary>
    ///
    /// <param name="_fstencil">Front stencil state.</param>
    /// <param name="_bstencil">Back stencil state. If back is set to `BGFX_STENCIL_NONE` _fstencil is applied to both front and back facing primitives.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_stencil", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetStencil(uint _fstencil, uint _bstencil);

    /// <summary>
    /// Set scissor for draw primitive.
    /// @remark
    ///   To scissor for all primitives in view see `bgfx::setViewScissor`.
    /// </summary>
    ///
    /// <param name="_x">Position x from the left corner of the window.</param>
    /// <param name="_y">Position y from the top corner of the window.</param>
    /// <param name="_width">Width of view scissor region.</param>
    /// <param name="_height">Height of view scissor region.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_scissor", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe ushort BGFX_SetScissor(ushort _x, ushort _y, ushort _width, ushort _height);

    /// <summary>
    /// Set scissor from cache for draw primitive.
    /// @remark
    ///   To scissor for all primitives in view see `bgfx::setViewScissor`.
    /// </summary>
    ///
    /// <param name="_cache">Index in scissor cache.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_scissor_cached", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetScissorCached(ushort _cache);

    /// <summary>
    /// Set model matrix for draw primitive. If it is not called,
    /// the model will be rendered with an identity model matrix.
    /// </summary>
    ///
    /// <param name="_mtx">Pointer to first matrix in array.</param>
    /// <param name="_num">Number of matrices in array.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_transform", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_SetTransform(void* _mtx, ushort _num);

    /// <summary>
    ///  Set model matrix from matrix cache for draw primitive.
    /// </summary>
    ///
    /// <param name="_cache">Index in matrix cache.</param>
    /// <param name="_num">Number of matrices from cache.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_transform_cached", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetTransformCached(uint _cache, ushort _num);

    /// <summary>
    /// Reserve matrices in internal matrix cache.
    /// @attention Pointer returned can be modifed until `bgfx::frame` is called.
    /// </summary>
    ///
    /// <param name="_transform">Pointer to `Transform` structure.</param>
    /// <param name="_num">Number of matrices.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_alloc_transform", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe uint BGFX_AllocTransform(BGFX_Transform* _transform, ushort _num);

    /// <summary>
    /// Set shader uniform parameter for draw primitive.
    /// </summary>
    ///
    /// <param name="_handle">Uniform.</param>
    /// <param name="_value">Pointer to uniform data.</param>
    /// <param name="_num">Number of elements. Passing `UINT16_MAX` will use the _num passed on uniform creation.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_uniform", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetShaderUniform(BGFX_UniformHandle _handle, void* _value, ushort _num);

    /// <summary>
    /// Set index buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_handle">Index buffer.</param>
    /// <param name="_firstIndex">First index to render.</param>
    /// <param name="_numIndices">Number of indices to render.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetIndexBuffer(BGFX_IndexBufferHandle _handle, uint _firstIndex, uint _numIndices);

    /// <summary>
    /// Set index buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_handle">Dynamic index buffer.</param>
    /// <param name="_firstIndex">First index to render.</param>
    /// <param name="_numIndices">Number of indices to render.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_dynamic_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetDynamicIndexBuffer(BGFX_DynamicIndexBufferHandle _handle, uint _firstIndex, uint _numIndices);

    /// <summary>
    /// Set index buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_tib">Transient index buffer.</param>
    /// <param name="_firstIndex">First index to render.</param>
    /// <param name="_numIndices">Number of indices to render.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_transient_index_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetTransientIndexBuffer(BGFX_TransientIndexBuffer* _tib, uint _firstIndex, uint _numIndices);

    /// <summary>
    /// Set vertex buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_stream">Vertex stream.</param>
    /// <param name="_handle">Vertex buffer.</param>
    /// <param name="_startVertex">First vertex to render.</param>
    /// <param name="_numVertices">Number of vertices to render.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetVertexBuffer(byte _stream, BGFX_VertexBufferHandle _handle, uint _startVertex, uint _numVertices);

    /// <summary>
    /// Set vertex buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_stream">Vertex stream.</param>
    /// <param name="_handle">Vertex buffer.</param>
    /// <param name="_startVertex">First vertex to render.</param>
    /// <param name="_numVertices">Number of vertices to render.</param>
    /// <param name="_layoutHandle">Vertex layout for aliasing vertex buffer. If invalid handle is used, vertex layout used for creation of vertex buffer will be used.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetVertexBufferWithLayout(byte _stream, BGFX_VertexBufferHandle _handle, uint _startVertex, uint _numVertices, BGFX_VertexLayoutHandle _layoutHandle);

    /// <summary>
    /// Set vertex buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_stream">Vertex stream.</param>
    /// <param name="_handle">Dynamic vertex buffer.</param>
    /// <param name="_startVertex">First vertex to render.</param>
    /// <param name="_numVertices">Number of vertices to render.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_dynamic_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetDynamicVertexBuffer(byte _stream, BGFX_DynamicVertexBufferHandle _handle, uint _startVertex, uint _numVertices);

    /// <summary>
    /// Set vertex buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_stream">Vertex stream.</param>
    /// <param name="_handle">Dynamic vertex buffer.</param>
    /// <param name="_startVertex">First vertex to render.</param>
    /// <param name="_numVertices">Number of vertices to render.</param>
    /// <param name="_layoutHandle">Vertex layout for aliasing vertex buffer. If invalid handle is used, vertex layout used for creation of vertex buffer will be used.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_dynamic_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetDynamicVertexBufferWithLayout(byte _stream, BGFX_DynamicVertexBufferHandle _handle, uint _startVertex, uint _numVertices, BGFX_VertexLayoutHandle _layoutHandle);

    /// <summary>
    /// Set vertex buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_stream">Vertex stream.</param>
    /// <param name="_tvb">Transient vertex buffer.</param>
    /// <param name="_startVertex">First vertex to render.</param>
    /// <param name="_numVertices">Number of vertices to render.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_transient_vertex_buffer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetTransientVertexBuffer(byte _stream, BGFX_TransientVertexBuffer* _tvb, uint _startVertex, uint _numVertices);

    /// <summary>
    /// Set vertex buffer for draw primitive.
    /// </summary>
    ///
    /// <param name="_stream">Vertex stream.</param>
    /// <param name="_tvb">Transient vertex buffer.</param>
    /// <param name="_startVertex">First vertex to render.</param>
    /// <param name="_numVertices">Number of vertices to render.</param>
    /// <param name="_layoutHandle">Vertex layout for aliasing vertex buffer. If invalid handle is used, vertex layout used for creation of vertex buffer will be used.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_transient_vertex_buffer_with_layout", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetTransientVertexBufferWithLayout(byte _stream, BGFX_TransientVertexBuffer* _tvb, uint _startVertex, uint _numVertices, BGFX_VertexLayoutHandle _layoutHandle);

    /// <summary>
    /// Set texture stage for draw primitive.
    /// </summary>
    ///
    /// <param name="_stage">Texture unit.</param>
    /// <param name="_sampler">Program sampler.</param>
    /// <param name="_handle">Texture handle.</param>
    /// <param name="_flags">Texture sampling mode. Default value UINT32_MAX uses   texture sampling settings from the texture.   - `BGFX_SAMPLER_[U/V/W]_[MIRROR/CLAMP]` - Mirror or clamp to edge wrap     mode.   - `BGFX_SAMPLER_[MIN/MAG/MIP]_[POINT/ANISOTROPIC]` - Point or anisotropic     sampling.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_set_texture", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SetTexture(byte _stage, BGFX_UniformHandle _sampler, BGFX_TextureHandle _handle, BGFX_SamplerFlags _flags);

    /// <summary>
    /// Submit an empty primitive for rendering. Uniforms and draw state
    /// will be applied but no geometry will be submitted.
    /// @remark
    ///   These empty draw calls will sort before ordinary draw calls.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_touch", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_TouchView(ushort _id);

    /// <summary>
    /// Submit primitive for rendering.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_program">Program.</param>
    /// <param name="_depth">Depth for sorting.</param>
    /// <param name="_flags">Which states to discard for next draw. See `BGFX_DISCARD_*`.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_submit", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_Submit(ushort _id, BGFX_ProgramHandle _program, uint _depth, BGFX_DiscardFlags _flags);

    /// <summary>
    /// Submit primitive with occlusion query for rendering.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_program">Program.</param>
    /// <param name="_occlusionQuery">Occlusion query.</param>
    /// <param name="_depth">Depth for sorting.</param>
    /// <param name="_flags">Which states to discard for next draw. See `BGFX_DISCARD_*`.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_submit_occlusion_query", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_SubmitOcclusionQuery(ushort _id, BGFX_ProgramHandle _program, BGFX_OcclusionQueryHandle _occlusionQuery, uint _depth, byte _flags);


    /// <summary>
    /// Blit 2D texture region between two 2D textures.
    /// @attention Destination texture must be created with `BGFX_TEXTURE_BLIT_DST` flag.
    /// @attention Availability depends on: `BGFX_CAPS_TEXTURE_BLIT`.
    /// </summary>
    ///
    /// <param name="_id">View id.</param>
    /// <param name="_dst">Destination texture handle.</param>
    /// <param name="_dstMip">Destination texture mip level.</param>
    /// <param name="_dstX">Destination texture X position.</param>
    /// <param name="_dstY">Destination texture Y position.</param>
    /// <param name="_dstZ">If texture is 2D this argument should be 0. If destination texture is cube this argument represents destination texture cube face. For 3D texture this argument represents destination texture Z position.</param>
    /// <param name="_src">Source texture handle.</param>
    /// <param name="_srcMip">Source texture mip level.</param>
    /// <param name="_srcX">Source texture X position.</param>
    /// <param name="_srcY">Source texture Y position.</param>
    /// <param name="_srcZ">If texture is 2D this argument should be 0. If source texture is cube this argument represents source texture cube face. For 3D texture this argument represents source texture Z position.</param>
    /// <param name="_width">Width of region.</param>
    /// <param name="_height">Height of region.</param>
    /// <param name="_depth">If texture is 3D this argument represents depth of region, otherwise it's unused.</param>
    ///
    [DllImport(DllName, EntryPoint = "bgfx_blit", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void BGFX_BlitTexture(ushort _id, BGFX_TextureHandle _dst, byte _dstMip, ushort _dstX, ushort _dstY, ushort _dstZ, BGFX_TextureHandle _src, byte _srcMip, ushort _srcX, ushort _srcY, ushort _srcZ, ushort _width, ushort _height, ushort _depth);
}

#pragma warning restore CS0649
