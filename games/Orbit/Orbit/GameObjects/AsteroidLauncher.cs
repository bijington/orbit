using Orbit.Engine;

namespace Orbit.GameObjects;

public class AsteroidLauncher : GameObject
{
    private readonly IServiceProvider serviceProvider;
    private readonly IGameSceneManager gameSceneManager;
    DateTime lastSpawn;
    DateTime lastUpdate;
    readonly Random random;
    double spawnMaximum = 5;
    double nextSpawn;

    public AsteroidLauncher(
        IServiceProvider serviceProvider,
        IGameSceneManager gameSceneManager)
    {
        this.serviceProvider = serviceProvider;
        this.gameSceneManager = gameSceneManager;
        random = new Random();

        nextSpawn = random.NextDouble() * spawnMaximum;
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        if ((lastUpdate - lastSpawn).TotalSeconds > 2)
        {
            Asteroid asteroid = serviceProvider.GetRequiredService<Asteroid>();

            // Calculate random angle between 0 and 359
            var angle = random.Next(0, 360);

            double radians = angle * (Math.PI / 180);

            var halfScreenHeight = 0.5f;
            var halfScreenWidth = 0.5f;

            PointF origin;
            PointF destination;
            PointF speed;

            float x = (float)Math.Sqrt(Math.Pow(halfScreenWidth, 2) + Math.Pow(halfScreenHeight, 2));
            float cosX = (float)Math.Cos(radians);
            float sinX = (float)Math.Sin(radians);

            destination = new PointF(halfScreenWidth, halfScreenHeight);

            if (angle >= 0 && angle <= 90)
            {
                origin = new PointF(
                    x * sinX + halfScreenWidth,
                    x * cosX + halfScreenHeight);
            }
            else if (angle > 90 && angle <= 180)
            {
                origin = new PointF(
                    x * sinX + halfScreenWidth,
                    x * cosX - halfScreenHeight);
            }
            else if (angle > 180 && angle <= 270)
            {
                origin = new PointF(
                    x * sinX + halfScreenWidth,
                    x * cosX + halfScreenHeight);
            }
            else
            {
                origin = new PointF(
                    x * sinX + halfScreenWidth,
                    x * cosX + halfScreenHeight);
            }

            speed = new PointF(
                (destination.X - origin.X) / 1000f,
                (destination.Y - origin.Y) / 1000f);


            asteroid.SetMovement(new Movement(origin, destination, speed));

            Console.WriteLine($"Spawning at {origin} heading to {destination} at speed {speed}");

            CurrentScene.Add(asteroid);

            lastSpawn = DateTime.UtcNow;
            nextSpawn = double.MaxValue;//random.NextDouble() * spawnMaximum;
        }

        lastUpdate = DateTime.UtcNow;
    }
}
