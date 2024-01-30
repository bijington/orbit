using AirHockey.Shared;
using Orbit.Engine;

namespace AirHockey.GameObjects;

public class GameManager : GameObject
{
    private readonly Game game;
    private readonly GameStateManager gameStateManager;

    public GameManager(GameStateManager gameStateManager)
    {
        // var player = new PlayerState(Guid.NewGuid());

        // game = new Game(Guid.NewGuid(), player)
        // {
        //     PlayerTwo = new PlayerState(Guid.NewGuid())
        // };
        // this.gameStateManager = gameStateManager;
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        // this.gameStateManager.UpdateGame(game, millisecondsSinceLastUpdate);
    }
}
