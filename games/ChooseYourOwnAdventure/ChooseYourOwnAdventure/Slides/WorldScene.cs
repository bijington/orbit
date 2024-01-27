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

    public override string BackgroundMusic => "adventure.mp3";

    public override string Notes => 
"""
I would like to set the scene on our adventure today... You are a junior developer and you have been tasked by the seniors in your team to build a game.

Your journey will take you through some tutorials on the fundamental building blocks that can be used for the game.

There will be forks in your path as you navigate this 8 bit representation of the professional office. I won't spoil more of the story.

So let's begin our journey.
""";
}
