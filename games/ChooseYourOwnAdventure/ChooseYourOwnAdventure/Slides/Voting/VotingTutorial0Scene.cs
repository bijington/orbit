using BuildingGames.GameObjects;
using ChooseYourOwnAdventure.GameObjects;

namespace BuildingGames.Slides.Voting;

public class VotingTutorial0Scene : SlideSceneBase
{
    public override string Notes => 
"""
So what are we building? We want to provide the ability to

We will need to be device independent, by this we mean that we will allow for different device sizes to play together. After all .NET MAUI supports a variety of platforms and devices.

We will also need a central rule enforcer. This reminds me of one of our attempts to stay sane during lockdown a few years ago, we arranged for our eldest daughter to play Guess Who with one of her school friends. Things got a bit heated towards the end of the game, questions of cheating only for us to discover that there are different people in some sets. Who knew??
""";

    public VotingTutorial0Scene(Pointer pointer, AchievementManager achievementManager) : base(pointer)
    {
        achievementManager.UpdateProgress(AchievementNames.KnowItAll, 100);

        Character.Position = Character.Positions.Democracy1;
        Character.Position = Character.Positions.Democracy2;
        Character.Position = Character.Positions.Democracy3;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("What are we building?", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Provide a voting system

- Allow for a vote to be opened and closed

- Allow for a client to vote

- Allow for a client to see the results of the vote
""",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        base.Render(canvas, dimensions);
    }
}
