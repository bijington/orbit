namespace BuildingGames.Slides;

public static class SlideDeck
{
	public static IList<Type> Slides { get; } = new List<Type>()
    {
        typeof(TitleScene),
        typeof(CharacterSelectionScene),
        typeof(TutorialScene),
        typeof(RedVersusBluePillScene), // SlideLottie or TheGameEngineApproachScene

        // Option 2
        typeof(SlideLottie),
        typeof(SlideAnimations),
        typeof(SlideAnimationsPartTwo),
        typeof(WhatsOurProgressScene), // Need to get back on track

        // Option 1
        typeof(TheGameEngineApproachScene),
        typeof(DotnetMauiGraphicsScene),
        typeof(FittingItIntoDotnetMauiScene),
        typeof(SketchIntoGameScene),
        typeof(TheBirthOfAnotherDistractionScene), // Need to get back on track

        // Single vs Multi player? Look at Orbit vs Drawing game?

        // Decision to look at code behind this or accessibility?

        typeof(HowToUsePartOne),
        typeof(HowToUsePartTwo),
        typeof(HowToUsePartThree),
        typeof(HowToUsePartFour),
        typeof(HowToUsePartFive),
        typeof(TipsAndTricksSimpleScene),
        typeof(TipsAndTricksSimplePartTwoScene),
        typeof(TipsAndTricksDeviceScene),
        typeof(TheFinalBossScene),
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
