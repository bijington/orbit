using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class MagicianScene : VoteSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 3;
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly AchievementManager achievementManager;
    private readonly Decisions decisions;
    private string decision;
    private const string option1 = "Sure what's 5 minutes";
    private const string option2 = "No I need to carry on";

    protected override Type Option1DestinationType => typeof(SlideLottie);
    protected override Type Option2DestinationType => typeof(TheGameEngineApproachScene);

    public MagicianScene(Pointer pointer, AchievementBanner achievement, AchievementManager achievementManager, Decisions decisions) : base(pointer, achievement)
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
            await OpenVote("Do you want to learn some quick tricks?", option1, option2, false);
        }
        if (currentTransition == 3)
        {
            await CloseVote();

            if (Option1VoteCount == Option2VoteCount)
            {
                this.achievementManager.UpdateProgress(AchievementNames.StaleMate, 100);
                this.decision = "You chouldn't decide.";
                this.decisions.RecordDecision($"You chouldn't decide who to join.");
            }
            else if (Option1VoteCount > Option2VoteCount)
            {
                this.achievementManager.UpdateProgress(AchievementNames.FirstDecision, 100);
                this.decision = @$"You chose '{option1}'. You put down your things and move in closer to hear what the magician has to say.

Turn to page {SlideDeck.GetSlideIndex(DestinationSceneType)}";
                this.decisions.RecordDecision("You put down your things and move in closer to hear what the magician has to say.");
            }
            else
            {
                this.achievementManager.UpdateProgress(AchievementNames.FirstDecision, 100);
                this.decision = @$"You chose '{option2}'. You smile, decline politely and carry on walking.

Turn to page {SlideDeck.GetSlideIndex(DestinationSceneType)}";
                this.decisions.RecordDecision("You smile, decline politely and carry on walking.");
            }
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        if (currentTransition == 0)
        {
            Styling.RenderTitle("Decision time!", canvas, dimensions);
        }

        var introduction = @$"You meet a magician on your journey, for the simple cost of 5 minutes of your time they offer to show you some quick tricks to make even a basic application look and feel alive:

{option1}

{option2}";

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
