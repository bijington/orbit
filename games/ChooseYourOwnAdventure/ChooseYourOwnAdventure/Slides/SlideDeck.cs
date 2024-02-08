using BuildingGames.Slides.Accessibility;
using BuildingGames.Slides.Gaming;
using BuildingGames.Slides.Tutorials;
using BuildingGames.Slides.Voting;

namespace BuildingGames.Slides;

public static class SlideDeck
{
    // TODO: encapsulate this nicely to handle the branches and general navigation flow.
	public static IList<Type> Slides { get; } = new List<Type>()
    {
        typeof(TitleScene),
        typeof(CharacterSelectionScene),
        typeof(PrologueScene),
        typeof(WorldScene), // 8:36
        typeof(TutorialScene),

        // Why .NET MAUI for games?
        typeof(WhyDotnetMauiScene), // 12:46
        typeof(IntroducingOrbitScene),

        typeof(WorldScene),

        // What is powering the talk today?
        typeof(TheGameEngineApproachScene),
        typeof(ProcessUserInputScene),
        typeof(UpdateScene),
        typeof(RenderScene),
        // typeof(WaitScene), // 16:16

        typeof(WorldScene),

        // How to section?
        typeof(HowToUsePartOne),
        typeof(HowToUsePartThree),
        typeof(HowToUsePartFive),
        typeof(HowToUsePartTwo),
        typeof(HowToUsePartFour),

        typeof(WorldScene),

        // Decision 1
        typeof(DecisionTime1Scene),

        // Option 1
        typeof(VotingTutorial0Scene),
        typeof(VotingTutorial1Scene),
        typeof(TutorialPartTwoScene),
        typeof(VotingTutorialDemoScene),
        typeof(VotingTutorialWrapUp1Scene),
        typeof(VotingTutorialWrapUp2Scene),

        // Option 2
        typeof(GamingTutorial0Scene),
        typeof(GamingTutorial1Scene),
        typeof(TutorialPartTwoScene),
        typeof(GamingTutorial2Scene),
        typeof(GamingTutorial3Scene),
        typeof(TutorialPartThreeScene),
        typeof(GamingTutorial4Scene),
        typeof(GamingTutorial5Scene),
        typeof(GamingTutorial6Scene),
        typeof(GamingTutorial7Scene),
        typeof(GamingTutorialDemoScene),
        typeof(GamingTutorialWrapUp0Scene),
        typeof(GamingTutorialWrapUp1Scene),
        typeof(GamingTutorialWrapUp2Scene), // 5:40

        // Summarise this point
        typeof(WorldScene),

        typeof(DecisionTime2Scene),

        // Option 1
        typeof(ColourSchemeScene),
        typeof(DuckTypingScene),
        typeof(VisualAssistanceScene),
        typeof(NonVisualFeedbackScene),

        // Option 2
        typeof(TipsAndTricksSimpleScene),
        typeof(TipsAndTricksSimplePartTwoScene),
        typeof(TipsAndTricksDeviceScene),

        typeof(TheYearOfAIScene),
        //typeof(SummaryScene),
        typeof(WorldScene),
        typeof(Credits) // 6:32
    };

    private static int currentSlideIndex = 0;

    public static Type CurrentSlideType => Slides[currentSlideIndex];

    public static event Action<string> SlideNotesChanged;
    public static event Action<Type> SlideChanged;

    public static int GetSlideIndex(Type type) => Slides.IndexOf(type) + 1;

    private static string notes;
    public static string Notes
    {
        get => notes;
        set
        {
            notes = value;
            SlideNotesChanged?.Invoke(notes);
        }
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
