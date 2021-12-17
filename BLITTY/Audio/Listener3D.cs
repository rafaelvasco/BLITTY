using System.Numerics;

namespace BLITTY.Audio;

public class Listener3D
{
    /// <summary>
    /// List of all listeners. Used to keep track of indices.
    /// </summary>
    private static readonly List<Listener3D> _listeners = new();

    /// <summary>
    /// Listener index. Used to communicate with low-level API.
    /// </summary>
    private int _index;

    /// <summary>
    /// Listener position in 3D space. Used for panning and attenuation
    /// </summary>
    public Vector3 Position3D
    {
        get
        {
            GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
            return p.ToVector3();
        }
        set
        {
            GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
            SetAttributes(value.ToFmodVector(), v, f, u);
        }
    }

    /// <summary>
    /// Listener velocity in 3D space. Used for doppler effect.
    /// </summary>
    public Vector3 Velocity3D
    {
        get
        {
            GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
            return v.ToVector3();
        }
        set
        {
            GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
            SetAttributes(p, value.ToFmodVector(), f, u);
        }
    }

    /// <summary>
    /// Forwards orientation, must be of unit length (1.0) and perpendicular to up.
    /// UnitY by default.
    /// </summary>
    public Vector3 ForwardOrientation
    {
        get
        {
            GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
            return f.ToVector3();
        }
        set
        {
            GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
            SetAttributes(p, v, value.ToFmodVector(), u);
        }
    }

    /// <summary>
    /// Upwards orientation, must be of unit length (1.0) and perpendicular to forward.
    /// UnitZ by default.
    /// </summary>
    public Vector3 UpOrientation
    {
        get
        {
            GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
            return u.ToVector3();
        }
        set
        {
            GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
            SetAttributes(p, v, f, value.ToFmodVector());
        }
    }

    public Listener3D()
    {
        if (!FMODAudio.Initialized)
        {
            throw new Exception("You cannot create listeners before initializing FMODManager!");
        }
        _index = _listeners.Count;
        _listeners.Add(this);
        UpdateNumListeners();

        SetAttributes(
            Vector3.Zero,
            Vector3.Zero,
            Vector3.UnitY,
            Vector3.UnitZ
        );
    }

    /// <summary>
    /// Destroys the listener.
    /// </summary>
    public void Destroy()
    {
        if (_index != _listeners.Count - 1)
        {
            // Listener's index is its only link to its low-level attributes.
            // We don't have access to actual low-level listener objects -
            // if they even exist - and have to resort to this.
            // Basically, if user decides to destroy a listener from the middle of
            // listener list, we take the very last listener and place it instead
            // of the destoyed listener.

            var last = _listeners[^1];

            // Saving attribute.
            last.GetAttributes(out FMOD.VECTOR position, out var velocity, out var forwardVector, out var upVector);
            // Replacing index.
            last._index = _index;
            // Copying attributes over.
            last.SetAttributes(position, velocity, forwardVector, upVector);

            // Changing last element's place in listener list.
            _listeners.RemoveAt(_listeners.Count - 1);
            _listeners.Insert(last._index, last);
        }

        _listeners.Remove(this);
        UpdateNumListeners();

        _index = -1;
    }

    /// <summary>
    /// Gets all listener attributes at once.
    /// </summary>
    public void GetAttributes(
        out Vector3 position,
        out Vector3 velocity,
        out Vector3 forward,
        out Vector3 up
    )
    {
        GetAttributes(out FMOD.VECTOR p, out var v, out var f, out var u);
        position = p.ToVector3();
        velocity = v.ToVector3();
        forward = f.ToVector3();
        up = u.ToVector3();
    }


    /// <summary>
    /// Sets all listener attributes at once.
    /// </summary>
    public void SetAttributes(
        Vector3 position,
        Vector3 velocity,
        Vector3 forward,
        Vector3 up
    )
    {
        SetAttributes(
            position.ToFmodVector(),
            velocity.ToFmodVector(),
            forward.ToFmodVector(),
            up.ToFmodVector()
        );
    }


    /// <summary>
    /// Gets all listener attributes at once.
    /// </summary>
    private void GetAttributes(
        out FMOD.VECTOR position,
        out FMOD.VECTOR velocity,
        out FMOD.VECTOR forward,
        out FMOD.VECTOR up
    )
    {
        FMODAudio.FModSystem.get3DListenerAttributes(
            _index,
                out position,
                out velocity,
                out forward,
                out up
            );
    }


    /// <summary>
    /// Sets all listener attributes at once.
    /// </summary>
    private void SetAttributes(
        FMOD.VECTOR position,
        FMOD.VECTOR velocity,
        FMOD.VECTOR forward,
        FMOD.VECTOR up
    )
    {
        FMODAudio.FModSystem.set3DListenerAttributes(
            _index,
            ref position,
            ref velocity,
            ref forward,
            ref up
        );
    }

    private void UpdateNumListeners()
    {
        FMODAudio.FModSystem.set3DNumListeners(_listeners.Count);
    }
}
