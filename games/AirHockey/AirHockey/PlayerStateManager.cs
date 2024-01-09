using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace AirHockey;

public class PlayerStateManager
{
    private HubConnection hubConnection;
    Action<PlayerState> onUpdatePlayerState;

    public PuckState PuckState { get; private set; }

    public PlayerStateManager()
	{
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7226/Game")
            .Build();

        PuckState = new();

        // Handle others in group logic through the "OthersInGroup" property in SignalR.
        // Direction of updates... send up to server.
        // Do we send input up to the server and render response back???
        hubConnection.On<PuckState>(EventNames.PuckStateUpdated, (puckState) =>
        {
            Console.WriteLine("PUCK IT");
            PuckState = puckState;
        });
        hubConnection.On<PlayerState>("UpdatePlayerState", msg =>
        {
            try
            {
                Console.WriteLine($"Received message {msg.X}");
                this.onUpdatePlayerState?.Invoke(msg);
                //Console.WriteLine($"From {msg.UserName}");
                //UpdateProperties(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to receive message");
                Console.WriteLine(ex.Message);
            }
        });

        // Update score
    }

    public Task Connect()
    {
        return hubConnection.StartAsync();
    }

    public Task Disconnect()
    {
        return hubConnection.StopAsync();
    }

    public void RegisterCallback(Action<PlayerState> onUpdatePlayerState)
    {
        this.onUpdatePlayerState = onUpdatePlayerState;
    }

    public async Task UpdateState(int x, int y)
    {
        await hubConnection.SendAsync("UpdatePlayerState", new PlayerState { X = x, Y = y });
    }
}

public class GameState
{
    public PlayerState PlayerOne { get; set; }

    public PlayerState PlayerTwo { get; set; }

    public PuckState Puck { get; set; }
}

public class PlayerState
{
    public int Id { get; set; }

    public float X { get; set; }

    public float Y { get; set; }

    // TODO: Size?
}
