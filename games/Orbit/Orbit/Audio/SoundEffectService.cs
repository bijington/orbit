using Plugin.Maui.Audio;

namespace Orbit.Audio;

public class SoundEffectService
{
    static readonly IDictionary<SoundEffect, string> soundEffectToResourceMapping = new Dictionary<SoundEffect, string>()
    {
        [SoundEffect.Thruster] = "engine-heavy-loop.mp3"
    };

    public static string GetSoundEffectFile(SoundEffect soundEffect) =>
        soundEffectToResourceMapping[soundEffect];

}