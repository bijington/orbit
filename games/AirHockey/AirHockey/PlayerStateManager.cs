using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Plugin.Maui.Audio;

namespace AirHockey;

public class PlayerStateManager
{
    private HubConnection hubConnection;
    private IAudioPlayer collisionPlayer;
    private IAudioPlayer wallCollisionPlayer;
    private readonly IAudioManager audioManager;
    private readonly IFileSystem fileSystem;

    public PlayerState PlayerState { get; private set; }

    public PlayerState OpponentState { get; private set; }

    public PuckState PuckState { get; private set; }

    public ScoreState ScoreState { get; private set; }

    public PlayerStateManager(
        IAudioManager audioManager,
        IFileSystem fileSystem,
        IHapticFeedback hapticFeedback)
	{
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7226/Game")
            .Build();

        PuckState = new()  { X = 0.5f, Y = 0.5f };
        ScoreState = new();
        PlayerState = new(Guid.NewGuid()) { X = 0.5f, Y = 0.75f };

        hubConnection.On<PuckState>(EventNames.PuckStateUpdated, puckState =>
        {
            PuckState = puckState;
        });
        hubConnection.On<ScoreState>(EventNames.ScoreUpdated, scoreState =>
        {
            ScoreState = scoreState;
        });

        hubConnection.On<PlayerState>(EventNames.PlayerStateUpdated, playerState =>
        {
            OpponentState = playerState;
        });

        hubConnection.On<Guid>(EventNames.PuckCollision, playerId =>
        {
            this.collisionPlayer.Play();

            if (playerId == PlayerState.Id)
            {
                hapticFeedback.Perform(HapticFeedbackType.Click);
            }
        });

        hubConnection.On(EventNames.WallCollision, () =>
        {
            this.wallCollisionPlayer.Play();
        });

        hubConnection.On<PlayerState>(EventNames.PlayerConnected, playerState =>
        {
            if (PlayerState.Id == playerState.Id)
            {
                PlayerState = playerState;
            }
            else
            {
                OpponentState = playerState;
            }
        });

        hubConnection.On<GameState>(EventNames.GameStarted, gameState =>
        {
            if (PlayerState.Id == gameState.PlayerOne.Id)
            {
                PlayerState = gameState.PlayerOne;
                OpponentState = gameState.PlayerTwo;
            }
            else
            {
                PlayerState = gameState.PlayerTwo;
                OpponentState = gameState.PlayerOne;
            }
        });
        this.audioManager = audioManager;
        this.fileSystem = fileSystem;
    }

    public async Task Connect()
    {
        this.collisionPlayer = this.audioManager.CreatePlayer(await fileSystem.OpenAppPackageFileAsync("ting.m4a"));
        this.wallCollisionPlayer = this.audioManager.CreatePlayer(await fileSystem.OpenAppPackageFileAsync("wall_collision.mp3"));

        await hubConnection.StartAsync();

        await hubConnection.SendAsync(MethodNames.PlayGame, PlayerState.Id);
    }

    public Task Disconnect()
    {
        return hubConnection.StopAsync();
    }

    public async Task UpdateState(float x, float y)
    {
        PlayerState.X = x;
        PlayerState.Y = y;
        await hubConnection.SendAsync(MethodNames.UpdatePlayerState, PlayerState);
    }
}
