using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial2Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorial2Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_tutorial_2.png");
	}

    public override string Notes => 
        @"We are going to do this in a slightly different order to the way it appeared on the previous slide. We are going to deal with the server first.
        
        We are going to create a hub that will be responsible for sending and receiving information from the clients. Thankfully SignalR ships with an ASP.NET Core web app so we don’t need to do much work here.
        
        SignalR provides us with a mechanism to send messages out to connected clients. We can send to All, Others or even Group clients together and send to the group. It is this last option that we will be using today.";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Creating a hub", canvas, dimensions);

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Center.X - imageWidth / 2, dimensions.Center.Y - imageHeight / 2, imageWidth, imageHeight);

        base.Render(canvas, dimensions);

        var a = @"
using Microsoft.AspNetCore.SignalR;

namespace AirHockey.Server.Hubs;

public class GameHub : Hub
{
    public async Task PlayGame(Guid playerId)
    {
        var groupName = gameId;

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        await Clients
            .Group(groupName)
            .SendAsync(EventNames.PlayerConnected, connectedPlayer);

        if (game.PlayerTwo != PlayerState.Empty)
        {
            await Clients
                .Group(groupName)
                .SendAsync(EventNames.GameStarted, new GameState(game.Id, game.PlayerOne, game.PlayerTwo));
        }
    }

    public async Task UpdatePlayerState(PlayerState playerState)
    {
        await Clients
            .OthersInGroup(playerState.GameId)
            .SendAsync(EventNames.PlayerStateUpdated, playerState);
    }
}";
    }
}
