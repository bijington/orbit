using BuildingGames.GameObjects;
using ChooseYourOwnAdventure;
using Microsoft.AspNetCore.SignalR.Client;

namespace BuildingGames.Slides;

public abstract class VoteSceneBase : SlideSceneBase
{
    private HubConnection hubConnection;
    protected int Option1VoteCount;
    protected int Option2VoteCount;

    public VoteSceneBase(Pointer pointer, Achievement achievement) : base(pointer, achievement)
    {
        try
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://choose-your-own-adventure-poll.azurewebsites.net/votinghub")
                .Build();

            hubConnection.On<Dictionary<int, int>> ("UpdateVotes", votes =>
            {
                Option1VoteCount = votes[0];
                Option2VoteCount = votes[1];
            });

            hubConnection.StartAsync().Wait();

            var voteState = new VoteState
            {
                Title = "Red or blue?",
                Option1Label = "Red",
                Option2Label = "Blue"
            };
            hubConnection.SendAsync("OpenVoting", voteState).Wait();
        }
        catch (Exception ex)
        {
        }
    }
}
