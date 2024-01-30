using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Plugin.Maui.Audio;

namespace AirHockey;

public class PlayerStateManager : IGameLifeCycleHandler
{
    private HubConnection hubConnection;
    private IAudioPlayer collisionPlayer;
    private IAudioPlayer wallCollisionPlayer;
    private readonly IAudioManager audioManager;
    private readonly IFileSystem fileSystem;
    private readonly IHapticFeedback hapticFeedback;

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

        hubConnection.On<PuckState>(EventNames.PuckStateUpdated, async puckState => await PuckStateUpdated(Guid.Empty, puckState));
        hubConnection.On<ScoreState>(EventNames.ScoreUpdated, async scoreState => await ScoreUpdated(Guid.Empty, scoreState));

        hubConnection.On<PlayerState>(EventNames.PlayerStateUpdated, playerState =>
        {
            OpponentState = playerState;
        });

        hubConnection.On<Guid>(EventNames.PuckCollision, async (playerId) => await PuckCollision(Guid.Empty, playerId));

        hubConnection.On(EventNames.WallCollision, async () => await WallCollision(Guid.Empty));

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
        this.hapticFeedback = hapticFeedback;
    }

    public async Task Initialise()
    {
        this.collisionPlayer = this.audioManager.CreatePlayer(await fileSystem.OpenAppPackageFileAsync("ting.m4a"));
        this.wallCollisionPlayer = this.audioManager.CreatePlayer(await fileSystem.OpenAppPackageFileAsync("wall_collision.mp3"));
    }

    public async Task Connect()
    {
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

    public Task PuckCollision(Guid gameId, Guid playerId)
    {
        this.collisionPlayer.Play();

        if (playerId == PlayerState.Id)
        {
            hapticFeedback.Perform(HapticFeedbackType.Click);
        }
        return Task.CompletedTask;
    }

    public Task PuckStateUpdated(Guid gameId, PuckState puckState)
    {
        PuckState = puckState;
        return Task.CompletedTask;
    }

    public Task ScoreUpdated(Guid gameId, ScoreState scoreState)
    {
        ScoreState = scoreState;
        return Task.CompletedTask;
    }

    public Task WallCollision(Guid gameId)
    {
        this.wallCollisionPlayer.Play();
        return Task.CompletedTask;
    }
}
