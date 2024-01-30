using Microsoft.AspNetCore.SignalR;
using AirHockey.Server.Hubs;
using AirHockey.Shared;

namespace AirHockey.Server;

public class SignalRGameLifeCycleHandler : IGameLifeCycleHandler
{
    private readonly IHubContext<GameHub> hubContext;

    public SignalRGameLifeCycleHandler(IHubContext<GameHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    public async Task PuckCollision(Guid gameId, Guid playerId)
    {
        await this.hubContext.Clients.Group(gameId.ToString()).SendAsync(EventNames.PuckCollision, playerId);
    }

    public async Task PuckStateUpdated(Guid gameId, PuckState puckState)
    {
        await this.hubContext.Clients.Group(gameId.ToString()).SendAsync(EventNames.PuckStateUpdated, puckState);
    }

    public async Task ScoreUpdated(Guid gameId, ScoreState scoreState)
    {
        await this.hubContext.Clients.Group(gameId.ToString()).SendAsync(EventNames.ScoreUpdated, scoreState);
    }

    public async Task WallCollision(Guid gameId)
    {
        await this.hubContext.Clients.Group(gameId.ToString()).SendAsync(EventNames.WallCollision);
    }
}