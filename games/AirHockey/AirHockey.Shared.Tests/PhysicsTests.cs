using AirHockey.Shared;

namespace AirHockey.Shared.Tests;

public class PhysicsTests
{
    [Test]
    public void Test1()
    {
        var player = new PlayerState(Guid.NewGuid())
        {
            X = 0.1,
            Y = 0.1
        };
        var puck = new PuckState
        { 
            VelocityX = 0.01,
            VelocityY = 0.001
        };

        puck.X = (player.X + player.Size / 2);
        puck.Y = player.Y + (puck.Size / 3);

        Console.WriteLine($"player {player.X},{player.Y} - {player.Size}");
        Console.WriteLine($"puck {puck.X},{puck.Y} - {puck.Size}");

        Console.WriteLine($"puck velocity {puck.VelocityX},{puck.VelocityY}");

        var angle = Physics.CalculateAngleAfterCollision(player, puck);

        Console.WriteLine(angle);

        Physics.ApplyForceAfterCollision(player, puck, angle);

        Console.WriteLine($"puck velocity {puck.VelocityX},{puck.VelocityY}");
    }
}