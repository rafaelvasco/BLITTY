using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BLITTY.Audio;

internal static class FMODAudio
{
    public static FMOD.System FModSystem { get; private set; }

    public static bool Initialized { get; private set; } = false;


    public static void Init(
        int maxChannels = 256,
        uint dspBufferLength = 4,
        int dspBufferCount = 32,
        FMOD.INITFLAGS coreInitFlags = FMOD.INITFLAGS.CHANNEL_LOWPASS | FMOD.INITFLAGS.CHANNEL_DISTANCEFILTER
    )
    {
        if (Initialized)
        {
            throw new Exception("FMODAudio is already initialized!");
        }

        Initialized = true;

        FMOD.Factory.System_Create(out var system);

        FModSystem = system;

        FModSystem.init(maxChannels, coreInitFlags, (IntPtr)0);

        FModSystem.setDSPBufferSize(dspBufferLength, dspBufferCount);
    }

    public static void Update()
    {
        FModSystem.update();
    }

    public static void Unload()
    {
        FModSystem.release();
    }

    public static Sound LoadSound(string id, byte[] data)
    {
        var info = new FMOD.CREATESOUNDEXINFO();

        info.length = (uint)data.Length;
        info.cbsize = Marshal.SizeOf(info);

        FModSystem.createSound(
            data,
            FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESAMPLE,
            ref info,
            out FMOD.Sound newSound
        );

        return new Sound(id, newSound);
    }

    public static Sound LoadStreamedSound(string id, byte[] data)
    {
        var pinnedBuffer = GC.AllocateArray<byte>(data.Length, pinned: true);

        Unsafe.CopyBlockUnaligned(ref pinnedBuffer[0], ref data[0], (uint)data.Length);

        var info = new FMOD.CREATESOUNDEXINFO();

        info.length = (uint)pinnedBuffer.Length;
        info.cbsize = Marshal.SizeOf(info);

        FModSystem.createSound(
            data,
            FMOD.MODE.OPENMEMORY | FMOD.MODE.CREATESTREAM,
            ref info,
            out FMOD.Sound newSound
        );

        return new Sound(id, newSound, pinnedBuffer);
    }
}
