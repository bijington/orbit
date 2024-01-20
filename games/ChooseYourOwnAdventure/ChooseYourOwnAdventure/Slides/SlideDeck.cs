namespace BuildingGames.Slides;

public static class SlideDeck
{
    // TODO: encapsulate this nicely to handle the branches and general navigation flow.
	public static IList<Type> Slides { get; } = new List<Type>()
    {
        typeof(WorldScene),
        typeof(TitleScene),
        typeof(CharacterSelectionScene),
        typeof(PrologueScene),
        typeof(TutorialScene),
        typeof(WorldScene),
        typeof(DecisionTime1Scene),

        typeof(TutorialPartTwoScene),
        typeof(HowToUseSignalRScene),

        // Decision Time
        typeof(VotingSystemOrDrawingGameScene), // Roughly 17 minutes - 14 minutes

        // Option 1
        typeof(DemoTimePoliticiansScene), // Roughly 10 minutes - 14 minutes

        // Option 2
        typeof(DemoTimeArtistsScene),

        // Summarise this point
        typeof(Stage1SummaryScene),

        // Decision Time
        typeof(MagicianScene),
        //typeof(ChooseDifficultyScene), // Maybe offer a slight detour on our journey to learn these things?
        //typeof(RedVersusBluePillScene), // SlideLottie or TheGameEngineApproachScene

        // Optional extra from MagicianScene - might not fit but it offers some nice insights.
        typeof(SlideLottie),
        typeof(SlideAnimations),
        typeof(SlideAnimationsPartTwo),
        typeof(SlideParticleEffects),
        typeof(SlideCombined), // Roughly 5 minutes

        // What is powering the talk today?

        typeof(TheGameEngineApproachScene),
        typeof(ProcessUserInputScene),
        typeof(ProcessUserInputPartTwoScene),
        typeof(UpdateScene),
        typeof(RenderScene),
        typeof(WaitScene), // 14 minutes

        // Decision deep dive into Orbit game or look at the engine behind it?
        typeof(OrbitGameOrOrbitEngineScene),
        typeof(GameDemoScene),

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

    public static event Action<string> SlideNotesChanged;
    public static event Action<Type> SlideChanged;

    public static int GetSlideIndex(Type type) => Slides.IndexOf(type) + 1;

    public static void SetSlideNotes(string notes)
    {
        SlideNotesChanged?.Invoke(notes);
    }

    public static void SetCurrentSlideType(Type type)
    {
        currentSlideIndex = Slides.IndexOf(type);

        SlideChanged?.Invoke(Slides[currentSlideIndex]);
    }

    public static Type GetNextSlideType()
    {
        var nextSceneIndex = currentSlideIndex + 1;

        if (nextSceneIndex < Slides.Count)
        {
            currentSlideIndex = nextSceneIndex;

            SlideChanged?.Invoke(Slides[currentSlideIndex]);

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

            SlideChanged?.Invoke(Slides[currentSlideIndex]);

            return Slides[currentSlideIndex];
        }

        return null;
    }
}
