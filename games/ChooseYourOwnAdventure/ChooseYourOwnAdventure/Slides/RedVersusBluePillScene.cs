using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class RedVersusBluePillScene : VoteSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 2;
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly AchievementManager achievementManager;
    private string decision;

    protected override Type Option1DestinationType => typeof(TheGameEngineApproachScene);
    protected override Type Option2DestinationType => typeof(SlideLottie);

    public RedVersusBluePillScene(Pointer pointer, AchievementManager achievementManager) : base(pointer)
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
            await OpenVote("Red or blue pill?", "Red pill", "Blue pill", false);
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
                this.decision = "You chose the Red pill. Let's go and check out some fun game engine based concepts.";
            }
            else
            {
                this.decision = "You chose the Blue pill. Let's go and check out how we can apply common concepts in a .NET MAUI app.";
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
            canvas.FontSize = (float)Styling.ScaledFontSize(0.3);

            canvas.FontColor = Colors.Red;
            canvas.DrawString(
                Option1VoteCount.ToString(),
                new RectF(0, 0, dimensions.Width / 2, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Bottom,
                TextFlow.OverflowBounds);

            canvas.FontColor = Colors.Blue;
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
