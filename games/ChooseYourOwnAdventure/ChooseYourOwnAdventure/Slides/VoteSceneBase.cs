using BuildingGames.GameObjects;
using ChooseYourOwnAdventure;
using Microsoft.AspNetCore.SignalR.Client;

namespace BuildingGames.Slides;

public abstract class VoteSceneBase : SlideSceneBase, IDestinationKnowingScene
{
    private readonly HubConnection hubConnection;
    protected int Option1VoteCount;
    protected int Option2VoteCount;
    protected abstract Type Option1DestinationType { get; }
    protected abstract Type Option2DestinationType { get; }

    public Type DestinationSceneType => Option1VoteCount > Option2VoteCount ? Option1DestinationType : Option2DestinationType;

    public VoteSceneBase(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
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

            _ = hubConnection.StartAsync();
        }
        catch (Exception ex)
        {
        }
    }

    protected async Task OpenVote(string title, string option1, string option2, bool isPrizeOnOffer)
    {
        var voteState = new VoteState
        {
            Title = title,
            Option1Label = option1,
            Option2Label = option2,
            IsOpen = true,
            SelectWinner = isPrizeOnOffer
        };

        await hubConnection.SendAsync("OpenVoting", voteState);
    }

    protected async Task CloseVote()
    {
        await hubConnection.SendAsync("CloseVoting");
        await hubConnection.StopAsync();
    }
}
