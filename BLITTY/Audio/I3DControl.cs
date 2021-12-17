using System.Numerics;

namespace BLITTY.Audio;

public interface I3DControl
{
    Vector3 Position3D { get; set; }

    Vector3 Velocity3D { get; set; }

    float MinDistance3D { get; set; }

    float MaxDistance3D { get; set; }

    ConeSettings3D ConeSettings3D { get; set; }

}
