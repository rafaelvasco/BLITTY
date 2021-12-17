using System.Numerics;

namespace BLITTY.Audio;

internal interface IChannelControl : I3DControl
{
    void Stop();

    bool Paused { get; set; }
    float Volume { get; set; }
    bool VolumeRamp { get; set; }
    float Audibility { get; }
    float Pitch { get; set; }
    bool Mute { get; set; }
    float LowpassGain { get; set; }

    FMOD.MODE Mode { get;set; }

    bool IsPlaying { get; }

    Vector3 ConeOrientation3D { get; set; }

    Occlusion3D Occlusion3D { get; set; }

    float Spread3D { get; set; }
    float Level3D { get; set; }
    float DopplerLevel3D { get; set; }

    DistanceFilter3D DistanceFilter3D { get; set; }
}
