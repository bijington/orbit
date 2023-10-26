using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class ChooseDifficultyScene : VoteSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 2;
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly AchievementManager achievementManager;
    private readonly Decisions decisions;
    private string decision;
    private const string option1 = "I'm Too Young To Die";
    private const string option2 = "Hurt Me Plenty";

    protected override Type Option1DestinationType => typeof(SlideLottie);
    protected override Type Option2DestinationType => typeof(TheGameEngineApproachScene);

    public ChooseDifficultyScene(Pointer pointer, AchievementManager achievementManager, Decisions decisions) : base(pointer)
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

        if (currentTransition == 1)
        {
            await OpenVote("Choose your difficulty", option1, option2, false);
        }
        if (currentTransition == 2)
        {
            await CloseVote();

            if (Option1VoteCount == Option2VoteCount)
            {
                this.achievementManager.UpdateProgress(AchievementNames.StaleMate, 100);
                this.decision = "You chouldn't decide.";
                this.decisions.RecordDecision($"You chouldn't decide a difficulty.");
            }
            else if (Option1VoteCount > Option2VoteCount)
            {
                this.decision = $"You chose '{option1}'. Let's ease ourselves in gently.";
                this.decisions.RecordDecision($"Difficulty of '{option1}'");
            }
            else
            {
                this.decision = $"You chose '{option2}'. Let's get stuck right in.";
                this.decisions.RecordDecision($"Difficulty of '{option2}'");
            }
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Difficulty select", canvas, dimensions);

        var introduction = @$"{option1} - You are new to .NET MAUI and just want to learn some quick tricks

{option2} - Show me the cool stuff!";

        canvas.Alpha = 1.0f;
        canvas.Font = Styling.Font;
        canvas.FontSize = (float)Styling.ScaledFontSize(0.05);
        canvas.FontColor = Styling.TitleColor;

        if (currentTransition is > 0 and < 2)
        {
            canvas.DrawString(
                introduction,
                new RectF(20, dimensions.Height * 0.18f, dimensions.Width - 40, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Top,
                TextFlow.OverflowBounds);
        }

        if (currentTransition == 0)
        {
            var size = Math.Min(dimensions.Width, dimensions.Height) / 2;

            canvas.DrawImage(image, (dimensions.Width - size) / 2, (dimensions.Height - size) / 2, size, size);
        }

        if (currentTransition == 1)
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

        if (currentTransition == 2)
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
