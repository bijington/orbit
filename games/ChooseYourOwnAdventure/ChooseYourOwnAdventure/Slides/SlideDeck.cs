namespace BuildingGames.Slides;

public static class SlideDeck
{
    // TODO: encapsulate this nicely to handle the branches and general navigation flow.
	public static IList<Type> Slides { get; } = new List<Type>()
    {
        typeof(TitleScene),
        typeof(CharacterSelectionScene),
        typeof(PrologueScene),
        typeof(TutorialScene),
        typeof(TutorialPartTwoScene),
        typeof(HowToUseSignalRScene),

        // Decision Time
        typeof(VotingSystemOrDrawingGameScene),

        // Option 1
        typeof(DemoTimePoliticiansScene),

        // Option 2
        typeof(DemoTimeArtistsScene),

        // Decision Time
        typeof(ChooseDifficultyScene),
        //typeof(RedVersusBluePillScene), // SlideLottie or TheGameEngineApproachScene

        // Option 1 - Feels like it doesn't really fit
        typeof(SlideLottie),
        typeof(SlideAnimations),
        typeof(SlideAnimationsPartTwo),
        typeof(SlideParticleEffects),

        // Option 2
        typeof(TheGameEngineApproachScene),
        typeof(DotnetMauiGraphicsScene),
        typeof(FittingItIntoDotnetMauiScene),
        typeof(SketchIntoGameScene),
        typeof(TheBirthOfAnotherDistractionScene),

        // Decision - Background on the game engine, or just show me how to use it?

        // Single vs Multi player? Look at Orbit vs Drawing game?
        

        // Decision to look at code behind this or accessibility? - Perhaps make it simple for you or the user?

        typeof(HowToUsePartOne),
        typeof(HowToUsePartTwo),
        typeof(HowToUsePartThree),
        typeof(HowToUsePartFour),
        typeof(HowToUsePartFive),

        typeof(TipsAndTricksSimpleScene),
        typeof(TipsAndTricksSimplePartTwoScene),
        typeof(TipsAndTricksDeviceScene),

        typeof(TheFinalBossScene),
        typeof(SummaryScene),
        typeof(Credits)
    };

    private static int currentSlideIndex = 0;

    public static Type CurrentSlideType => Slides[currentSlideIndex];

    public static void SetCurrentSlideType(Type type)
    {
        currentSlideIndex = Slides.IndexOf(type);
    }

    public static Type GetNextSlideType()
    {
        var nextSceneIndex = currentSlideIndex + 1;

        if (nextSceneIndex < Slides.Count)
        {
            currentSlideIndex = nextSceneIndex;

            return Slides[currentSlideIndex];
        }

        return null;
    }

    public static Type GetPreviousSlideType()
    {
        var previousSceneIndex = currentSlideIndex - 1;

        if (previousSceneIndex >= 0)
        {
            currentSlideIndex = previousSceneIndex;

            return Slides[currentSlideIndex];
        }

        return null;
    }
}
