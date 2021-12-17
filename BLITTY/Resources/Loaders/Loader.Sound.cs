using BLITTY.Audio;

namespace BLITTY;

public static partial class Loader
{
    public static Sound LoadSound(SoundSerializableData soundData)
    {
        Sound sound;

        if (soundData.Stream)
        {
            sound = FMODAudio.LoadStreamedSound(soundData.Id, soundData.Data);
        }
        else
        {
            sound = FMODAudio.LoadSound(soundData.Id, soundData.Data);
        }

        return sound;
    }
}

