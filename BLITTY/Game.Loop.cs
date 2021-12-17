using BLITTY.Audio;

namespace BLITTY;

public partial class Game : IDisposable
{
    private const int TimeHistoryCount = 4;

    private const int UpdateMult = 1;

    private bool _resync = true;

    private double _fixed_delta_time;

    private double _desired_frame_time;

    private double _vsync_max_error;

    private double[] _snap_freqs;

    private double[] _time_averager;

    private double _prev_frame_time;

    private double _frame_accum;

    private void InitGameLoopVariables()
    {
        _fixed_delta_time = 1.0 / FrameRate;
        _desired_frame_time = Platform.GetPerformanceFrequency() / FrameRate;
        _vsync_max_error = Platform.GetPerformanceFrequency() * 0.0002;

        double time_60hz = Platform.GetPerformanceFrequency() / 60;

        _snap_freqs = new[]
        {
                time_60hz, // 60FPS
                time_60hz * 2, // 30FPS
                time_60hz * 3, // 20FPS
                time_60hz * 4, // 15FPS
                (time_60hz + 1) / 2 // 120fps
            };

        _time_averager = new double[TimeHistoryCount];

        for (int i = 0; i < TimeHistoryCount; i++)
        {
            _time_averager[i] = _desired_frame_time;
        }
    }

    private void Tick(Scene scene)
    {
        double cur_frame_time = Platform.GetPerformanceCounter();

        double delta_time = cur_frame_time - _prev_frame_time;

        _prev_frame_time = cur_frame_time;

        // Handle unexpected timer anomalies
        if (delta_time > _desired_frame_time * 8)
        {
            delta_time = _desired_frame_time;
        }

        if (delta_time < 0)
        {
            delta_time = 0;
        }

        // VSync Time Snapping

        for (int i = 0; i < _snap_freqs.Length; ++i)
        {
            var snap_freq = _snap_freqs[i];

            if (Math.Abs(delta_time - snap_freq) < _vsync_max_error)
            {
                delta_time = snap_freq;
                break;
            }
        }

        // Delta Time Averaging

        for (int i = 0; i < TimeHistoryCount - 1; ++i)
        {
            _time_averager[i] = _time_averager[i + 1];
        }

        _time_averager[TimeHistoryCount - 1] = delta_time;

        delta_time = 0;

        for (int i = 0; i < TimeHistoryCount; ++i)
        {
            delta_time += _time_averager[i];
        }

        delta_time /= TimeHistoryCount;

        // Add to Accumulator

        _frame_accum += delta_time;

        // Spiral of Death Protection

        if (_frame_accum > _desired_frame_time * 8)
        {
            _resync = true;
        }

        // Timer Resync Requested

        if (_resync)
        {
            _frame_accum = 0;
            delta_time = _desired_frame_time;
            _resync = false;
        }

        // Process Events and Input

        Platform.ProcessEvents();

        if (UnlockFrameRate)
        {
            double consumed_delta_time = delta_time;

            while (_frame_accum >= _desired_frame_time)
            {
                scene.FixedUpdate((float)_fixed_delta_time);

                // Cap Variable Update dt to not be larger than fixed update,
                // and interleave it (so game state can always get animation frame it needs)

                if (consumed_delta_time > _desired_frame_time)
                {
                    FMODAudio.Update();
                    scene.Update((float)_fixed_delta_time);
                    consumed_delta_time -= _desired_frame_time;
                }

                _frame_accum -= _desired_frame_time;
            }

            FMODAudio.Update();
            scene.Update((float)(consumed_delta_time / Platform.GetPerformanceFrequency()));

            scene.Draw();

            Graphics.Frame();
        }

        // Locked Frame Rate, No Interpolation
        else
        {
            while (_frame_accum >= _desired_frame_time * UpdateMult)
            {
                for (int i = 0; i < UpdateMult; ++i)
                {
                    FMODAudio.Update();
                    scene.FixedUpdate((float)_fixed_delta_time);
                    scene.Update((float)_fixed_delta_time);

                    _frame_accum -= _desired_frame_time;
                }
            }

            scene.Draw();

            Graphics.Frame();

        }

        if (!_active)
        {
            Thread.Sleep(1);
        }
    }
}
