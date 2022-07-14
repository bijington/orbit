using DrawingGame.Shared;
using Microsoft.AspNetCore.SignalR;

namespace DrawingGame.Server.Hubs;

public class GameHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public async Task UpdateDrawingState(DrawingState drawingState)
    {
        await Clients.Others.SendAsync("UpdateDrawingState", drawingState);
    }
}
