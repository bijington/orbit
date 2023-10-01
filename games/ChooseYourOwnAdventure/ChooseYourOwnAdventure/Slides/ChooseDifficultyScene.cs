using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class ChooseDifficultyScene : VoteSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 2;
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly AchievementManager achievementManager;
    private string decision;
    private const string option1 = "I'm Too Young To Die";
    private const string option2 = "Hurt Me Plenty";

    protected override Type Option1DestinationType => typeof(SlideLottie);
    protected override Type Option2DestinationType => typeof(TheGameEngineApproachScene);

    public ChooseDifficultyScene(Pointer pointer, AchievementBanner achievement, AchievementManager achievementManager) : base(pointer, achievement)
    {
        image = LoadImage("voting_site_qrcode.png");
        this.achievementManager = achievementManager;
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
            await OpenVote("Choose your difficulty", option1, option2, true);
        }
        if (currentTransition == 2)
        {
            await CloseVote();

            if (Option1VoteCount == Option2VoteCount)
            {
                this.achievementManager.UpdateProgress(AchievementNames.StaleMate, 100);
                this.decision = "You chouldn't decide.";
            }
            else if (Option1VoteCount > Option2VoteCount)
            {
                this.decision = $"You chose '{option1}'. Let's ease ourselves in gently.";
            }
            else
            {
                this.decision = $"You chose '{option2}'. Let's get stuck right in.";
            }
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        var introduction = "You are approached by a person, they offer you the choice between a red and a blue pill. Will you take the red pill and choose to learn about building engine based games inside .NET MAUI, or will you remain in the contented experience of ordinary application styles with the blue pill?";

        canvas.Alpha = 1.0f;
        canvas.Font = Styling.Font;
        canvas.FontSize = (float)Styling.ScaledFontSize(0.05);
        canvas.FontColor = Styling.TitleColor;

        if (currentTransition < 2)
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
