using AirHockey.Server;
using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR;

namespace AirHockey.Server.Hubs;

public class GameHub : Hub
{
    // public async Task PlayerConnected(Player player)
    // {
    //     // Add the player to the game group name.
    //     await Groups.AddToGroupAsync(Context.ConnectionId, player.GroupName);

    //     await Clients.OthersInGroup(player.GroupName).SendAsync("PlayerConnected", player);
    // }

    // public async Task SessionStarted(SessionStarted session)
    // {
    //     await Clients.Group(session.GroupName).SendAsync("SessionStarted", session);
    // }

    // public async Task UpdateDrawingState(DrawingState drawingState)
    // {
    //     await Clients.OthersInGroup(drawingState.GroupName).SendAsync("UpdateDrawingState", drawingState);
    // }

    // public async Task GuessAttempt(Guess guess)
    // {
    //     await Clients.OthersInGroup(guess.GroupName).SendAsync("GuessAttempt", guess);
    // }

    // public async Task GuessCorrect(GuessCorrect guessCorrect)
    // {
    //     await Clients.Group(guessCorrect.GroupName).SendAsync("GuessCorrect", guessCorrect);
    // }

    internal async Task PuckMoved(Guid gameId, PuckState puckState)
    {
        await Clients.Group(gameId.ToString()).SendAsync(EventNames.PuckStateUpdated, puckState);
    }
}
