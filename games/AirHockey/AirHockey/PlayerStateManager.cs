using Microsoft.AspNetCore.SignalR.Client;

namespace AirHockey;

public class PlayerStateManager
{
    private HubConnection hubConnection;
    Action<PlayerState> onUpdatePlayerState;

    public PlayerStateManager()
	{
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7030/Game")
            .Build();

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

public class PlayerState
{
    public int X { get; set; }

    public int Y { get; set; }
}
