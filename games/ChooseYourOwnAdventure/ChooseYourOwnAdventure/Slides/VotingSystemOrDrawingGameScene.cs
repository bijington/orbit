using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class VotingSystemOrDrawingGameScene : VoteSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 3;
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly AchievementManager achievementManager;
    private readonly Decisions decisions;
    private string decision;
    private const string option1 = "Democracy";
    private const string option2 = "The arts";

    protected override Type Option1DestinationType => typeof(DemoTimePoliticiansScene);
    protected override Type Option2DestinationType => typeof(DemoTimeArtistsScene);

    public VotingSystemOrDrawingGameScene(Pointer pointer, AchievementBanner achievement, AchievementManager achievementManager, Decisions decisions) : base(pointer, achievement)
    {
        image = LoadImage("voting_site_qrcode.png");
        this.achievementManager = achievementManager;
        this.decisions = decisions;
    }

    public override async void Progress()
    {
        // If we are complete then fire the Next event.
        if (currentTransition == transitions)
        {
            base.Progress();
        }

        currentTransition++;

        if (currentTransition == 2)
        {
            await OpenVote("Where will you invest your knowledge?", option1, option2, false);
        }
        if (currentTransition == 3)
        {
            await CloseVote();

            if (Option1VoteCount == Option2VoteCount)
            {
                this.achievementManager.UpdateProgress(AchievementNames.StaleMate, 100);
                this.decision = "You chouldn't decide.";
                this.decisions.RecordDecision($"You chouldn't decide who to help.");
            }
            else if (Option1VoteCount > Option2VoteCount)
            {
                this.achievementManager.UpdateProgress(AchievementNames.FirstDecision, 100);
                this.decision = $"You chose '{option1}'. Let's take a look at how this voting system has been built.";
                this.decisions.RecordDecision($"'{option1}'");
            }
            else
            {
                this.achievementManager.UpdateProgress(AchievementNames.FirstDecision, 100);
                this.decision = $"You chose '{option2}'. Let's take a look at how to build a real-time drawing game.";
                this.decisions.RecordDecision($"'{option2}'");
            }
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        if (currentTransition == 0)
        {
            Styling.RenderTitle("Decision time!", canvas, dimensions);
        }

        var introduction = @$"Now that you have gained some knowledge of the power of SignalR the .NET elders would like to task you with using that power to help one of the following:
{option1} - Understand how the voting system within todays talk is functioning.

{option2} - See how to use SignalR to build a real-time drawing game.";

        canvas.Alpha = 1.0f;
        canvas.Font = Styling.Font;
        canvas.FontSize = (float)Styling.ScaledFontSize(0.05);
        canvas.FontColor = Styling.TitleColor;

        if (currentTransition is > 0 and < 3)
        {
            canvas.DrawString(
                introduction,
                new RectF(20, 20, dimensions.Width - 40, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Top,
                TextFlow.OverflowBounds);
        }

        if (currentTransition == 0)
        {
            var size = Math.Min(dimensions.Width, dimensions.Height) / 2;

            canvas.DrawImage(image, (dimensions.Width - size) / 2, (dimensions.Height - size) * 0.75f, size, size);
        }

        if (currentTransition == 2)
        {
            canvas.Alpha = 1.0f;
            canvas.Font = Styling.Font;
            canvas.FontSize = (float)Styling.ScaledFontSize(0.08);

            canvas.DrawString(
                option1,
                new RectF(0, 0, dimensions.Width / 2, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                TextFlow.OverflowBounds);
            canvas.DrawString(
                option2,
                new RectF(dimensions.Width / 2, 0, dimensions.Width / 2, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                TextFlow.OverflowBounds);

            canvas.FontSize = (float)Styling.ScaledFontSize(0.3);

            canvas.DrawString(
                Option1VoteCount.ToString(),
                new RectF(0, 0, dimensions.Width / 2, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Bottom,
                TextFlow.OverflowBounds);

            canvas.DrawString(
                Option2VoteCount.ToString(),
                new RectF(dimensions.Width / 2, 0, dimensions.Width / 2, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Bottom,
                TextFlow.OverflowBounds);
        }

        if (currentTransition == 3)
        {
            canvas.DrawString(
                @$"Voting is now closed!
{this.decision}",
                new RectF(20, 20, dimensions.Width - 40, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                TextFlow.OverflowBounds);
        }

        base.Render(canvas, dimensions);
    }
}
