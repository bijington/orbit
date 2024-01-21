using BuildingGames.GameObjects;
using ChooseYourOwnAdventure;

namespace BuildingGames.Slides;

public class WorldScene : SlideSceneBase
{
    public WorldScene(
        Pointer pointer,
        WorldMap worldMap) : base(pointer)
    {
        Add(worldMap);
    }

    public override string Notes => 
        @"I would like you all to use some imagination now, we need to imagine that we are a junior developer and we have been tasked with building a game.
        
        Joke about how the audience (including myself) don't look like juniors?";
}
