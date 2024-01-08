using BuildingGames.GameObjects;
using ChooseYourOwnAdventure;

namespace BuildingGames.Slides;

public class WorldScene : SlideSceneBase
{
    private readonly AchievementManager achievementManager;
    private readonly WorldMap worldMap;
    private float startAlpha = 1f;
    private float increment = -0.05f;

    public WorldScene(
        Pointer pointer,
        AchievementManager achievementManager,
        WorldMap worldMap) : base(pointer)
    {
        this.achievementManager = achievementManager;
        this.worldMap = worldMap;

        Add(worldMap);
    }
}