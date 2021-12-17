using System.Numerics;

namespace BLITTY.Audio;

public class Sound : GameAsset, I3DControl, IDisposable
{
    internal readonly FMOD.Sound Native;

    private readonly byte[]? _buffer;

    internal static readonly PointerLinker<Sound> _linker = new();

    public bool Looping
    {
        get => (Loops == -1);
        set
        {
            if (value)
            {
                Loops = -1;
            }
            else
            {
                Loops = 0;
            }
        }
    }

    /// <summary>
    /// Amount of loops. 
    /// > 0 - Specific count.
    /// 0 - No loops.
    /// -1 - Infinite loops.
    /// </summary>
    public int Loops = 0;

    /// <summary>
    /// Sound pitch. Affects speed too.
    /// 1 - Normal pitch.
    /// More than 1 - Higher pitch.
    /// Less than 1 - Lower pitch.
    /// </summary>
    public float Pitch = 1;

    /// <summary>
    /// Sound volume.
    /// 1 - Normal volume.
    /// 0 - Muted.
    /// </summary>
    public float Volume = 1;

    /// <summary>
    /// Low pass filter. Makes sound muffled.
    /// 1 - No filtering.
    /// 0 - Full filtering.
    /// </summary>
    public float LowPass = 1;

    /// <summary>
    /// Sound mode.
    /// </summary>
    public FMOD.MODE Mode
    {
        get
        {
            Native.getMode(out var mode);
            return mode;
        }
        set
        {
            Native.setMode(value);
        }
    }

    /// <summary>
    /// Sound's default channel group.
    /// </summary>
    public ChannelGroup? ChannelGroup;

    public SoundGroup? SoundGroup
    {
        get
        {
            Native.getSoundGroup(out var sound);
            sound.getUserData(out var ptr);
            return SoundGroup._linker.Get(ptr);
        }
        set
        {
            Native.setSoundGroup(value.Native);
        }
    }

    /// <summary>
    /// If true, allows sound to be positioned in 3D space.
    /// </summary>
    public bool Is3D = false;

    /// <summary>
    /// Sound's position in 3D space. Can be used only if 3D positioning is enabled.
    /// </summary>
    public Vector3 Position3D { get; set; } = Vector3.Zero;

    /// <summary>
    /// Sound's velocity in 3D space. Can be used only if 3D positioning is enabled.
    /// </summary>
    public Vector3 Velocity3D { get; set; } = Vector3.Zero;

    /// <summary>
    /// Distance from the source where attenuation begins.
    /// </summary>
    public float MinDistance3D
    {
        get
        {
            Native.get3DMinMaxDistance(out var minDistance, out var _);
            return minDistance;
        }
        set =>
            Native.set3DMinMaxDistance(value, MaxDistance3D);
    }

    /// <summary>
    /// Distance from the source where attenuation ends.
    /// </summary>
    public float MaxDistance3D
    {
        get
        {
            _ = Native.get3DMinMaxDistance(out var _, out var maxDistance);
            return maxDistance;
        }
        set =>
            Native.set3DMinMaxDistance(MinDistance3D, value);
    }

    /// <summary>
    /// Sound length in specified time units
    /// </summary>
    public uint Length
    {
        get
        {
            Native.getLength(out var length, LengthTimeunit);
            return length;
        }
    }

    public int LoopCount
    {
        get
        {
            Native.getLoopCount(out var loopCount);
            return loopCount;
        }
        set
        {
            Native.setLoopCount(value);
        }
    }

    public float DefaultFrequency
    {
        get
        {
            Native.getDefaults(out var frequency, out var _);
            return frequency;
        }
        set =>
            Native.setDefaults(value, DefaultPriority);
    }

    public int DefaultPriority
    {
        get
        {
            Native.getDefaults(out var _, out var priority);
            return priority;
        }
        set =>
            Native.setDefaults(DefaultFrequency, value);
    }

    public ConeSettings3D ConeSettings3D
    {
        get
        {
            var cone = new ConeSettings3D();
            Native.get3DConeSettings(out cone.InsideConeAngle, out cone.OutsideVolume, out cone.OutsideVolume);
            return cone;
        }
        set =>
            Native.set3DConeSettings(value.InsideConeAngle, value.OutsideConeAngle, value.OutsideVolume);
    }

    internal Sound(string id, FMOD.Sound sound) : base(id)
    {
        Native = sound;

        Native.setUserData(_linker.Add(this));
    }

    internal Sound(string id, FMOD.Sound sound, byte[] buffer) : this(id, sound)
    {
        _buffer = buffer;
    }

    public Channel Play(bool paused = false) =>
            Play(ChannelGroup, paused);

    public Channel Play(ChannelGroup? group, bool paused = false)
    {
        FMOD.ChannelGroup nativeGroup = default;
        if (group != null)
        {
            nativeGroup = group.Native;
        }

        FMODAudio.FModSystem.playSound(Native, nativeGroup, paused, out FMOD.Channel fmodChannel);
        return new Channel(this, fmodChannel);
    }

    protected override void FreeUnmanaged()
    {
        Native.getUserData(out var ptr);

        if (ptr != IntPtr.Zero)
        {
            _linker.Remove(ptr);
        }

        Native.release();
    }

    public FMOD.TIMEUNIT LengthTimeunit = FMOD.TIMEUNIT.MS;

}

