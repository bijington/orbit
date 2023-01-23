using System;
using Plugin.Maui.Audio;

namespace Orbit.Audio;

public class AudioService
{
    readonly IDictionary<string, IAudioPlayer> players = new Dictionary<string, IAudioPlayer>();
    private readonly IAudioManager audioManager;
    private readonly IFileSystem fileSystem;

    public AudioService(
        IAudioManager audioManager,
        IFileSystem fileSystem)
    {
        this.audioManager = audioManager;
        this.fileSystem = fileSystem;
    }

    public async Task Play(string audioItem)
    {
        if (!players.TryGetValue(audioItem, out var audioPlayer))
        {
            audioPlayer = audioManager.CreatePlayer(await fileSystem.OpenAppPackageFileAsync(audioItem));

            players[audioItem] = audioPlayer;
        }

        if (!audioPlayer.IsPlaying)
        {
            audioPlayer.Play();
        }
    }
}