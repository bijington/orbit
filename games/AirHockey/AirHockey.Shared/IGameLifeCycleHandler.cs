using System;

namespace AirHockey.Shared;

public interface IGameLifeCycleHandler
{
    Task PuckCollision(Guid gameId, Guid playerId);
    
    Task PuckStateUpdated(Guid gameId, PuckState puckState);

    Task ScoreUpdated(Guid gameId, ScoreState scoreState);

    Task WallCollision(Guid gameId);
}