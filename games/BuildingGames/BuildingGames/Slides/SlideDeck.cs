namespace BuildingGames.Slides;

public static class SlideDeck
{
	public static IList<Type> Slides { get; } = new List<Type>()
    {
        typeof(Slide01),
        typeof(Slide02),
        typeof(Slide03),
        typeof(Slide04),
        typeof(SlideLottie),
        typeof(SlideAnimations),
        typeof(SlideAnimationsPartTwo),
        typeof(SlideParticleEffects)
    };

    private static int currentSlideIndex = 0;

    public static Type CurrentSlideType => Slides[currentSlideIndex];

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
