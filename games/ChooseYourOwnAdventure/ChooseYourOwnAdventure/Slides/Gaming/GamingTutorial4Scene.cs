using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial4Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorial4Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_tutorial_4.png");
	}

    public override string Notes => 
"""
We mentioned that a Background Service is great for performing background tasks.
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Creating a Background Service", canvas, dimensions);

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Center.X - imageWidth / 2, dimensions.Center.Y - imageHeight / 2, imageWidth, imageHeight);

        base.Render(canvas, dimensions);

        var a = @"
public class GameWorker : BackgroundService
{
    private readonly ILogger<GameWorker> logger;
    private readonly GameManager gameManager;
    private readonly IHubContext<GameHub> hubContext;

    public GameWorker(ILogger<GameWorker> logger, GameManager gameManager, IHubContext<GameHub> hubContext)
    {
        this.logger = logger;
        this.gameManager = gameManager;
        this.hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            int delayInMilliseconds = 5;

            var game = this.gameManager.Games.FirstOrDefault();

            if (game is not null)
            {
                await ProcessGame(game);
            }

            await Task.Delay(delayInMilliseconds, stoppingToken);
        }
    }
}";

        var link = "https://learn.microsoft.com/azure/well-architected/reliability/background-jobs";
        var link2 = "https://learn.microsoft.com/aspnet/core/fundamentals/host/hosted-services";
    }
}
