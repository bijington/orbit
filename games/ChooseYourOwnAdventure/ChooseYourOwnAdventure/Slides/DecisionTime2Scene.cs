using BuildingGames.GameObjects;
using BuildingGames.Slides.Accessibility;
using BuildingGames.Slides.Gaming;
using BuildingGames.Slides.Voting;

namespace BuildingGames.Slides;

public class DecisionTime2Scene : VoteSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 3;
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly AchievementManager achievementManager;
    private readonly Decisions decisions;
    private string decision;
    private const string option1 = "Stop and help";
    private const string option2 = "Skip on";

    protected override Type Option1DestinationType => typeof(ColourSchemeScene);
    protected override Type Option2DestinationType => typeof(TipsAndTricksSimpleScene);

    public DecisionTime2Scene(Pointer pointer, AchievementManager achievementManager, Decisions decisions) : base(pointer)
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
            await OpenVote("Will you allocate some time to helping this traveler?", option1, option2, false);
        }
        else if (currentTransition == 3)
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
                this.achievementManager.UpdateProgress(AchievementNames.Paragon, 100);
                this.decision = $"You chose to '{option1}'.";
                this.decisions.RecordDecision($"'{option1}'");
            }
            else
            {
                this.achievementManager.UpdateProgress(AchievementNames.GoalOriented, 100);
                this.decision = $"You chose to '{option2}'.";
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

        // Opt to seek knowledge of democracy from the gods or the Dark arts of gaming from the mages.

        var introduction = @$"On your journey you come across a lonely traveler who is clearly in need of help. Do you
{option1} - Learn how you can assist this traveler and others.

{option2} - My goal is too important!";

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

            canvas.DrawString(
                "https://choose-your-own-adventure-poll.azurewebsites.net",
                new RectF(20, dimensions.Height * 0.2f, dimensions.Width - 40, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Top,
                TextFlow.OverflowBounds);
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
