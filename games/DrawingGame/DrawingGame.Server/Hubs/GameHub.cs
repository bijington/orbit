using DrawingGame.Shared;
using Microsoft.AspNetCore.SignalR;

namespace DrawingGame.Server.Hubs;

public class GameHub : Hub
{
    public async Task PlayerConnected(Player player)
    {
        await Clients.Others.SendAsync("PlayerConnected", player);
    }

    public async Task SessionStarted(SessionStarted session)
    {
        await Clients.Others.SendAsync("SessionStarted", session);
    }

    public async Task UpdateDrawingState(DrawingState drawingState)
    {
        await Clients.Others.SendAsync("UpdateDrawingState", drawingState);
    }
}
