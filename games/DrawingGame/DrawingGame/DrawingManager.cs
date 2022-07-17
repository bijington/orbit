using DrawingGame.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace DrawingGame;

public class DrawingManager
{
    private readonly IList<DrawingPath> paths = new List<DrawingPath>();
    private DrawingPath currentPath;
    private HubConnection hubConnection;
    private const string UpdateMethodName = "UpdateDrawingState";

    public bool IsViewing { get; } = false;

    public float LineThickness { get; set; } = 5f;

    public Color SelectedColor { get; set; } = Colors.Black;

    public IList<Color> SupportedColors { get; } = new List<Color>
    {
        Colors.Black,
        Colors.Red,
        Colors.Orange,
        Colors.Yellow,
        Colors.Green,
        Colors.Blue,
        Colors.Indigo,
        Colors.Violet,
        Colors.White
    };

    public IReadOnlyList<DrawingPath> Paths => paths.ToList();

    public Task StartGame()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://drawinggame-server.azurewebsites.net/Game")
            .Build();

        hubConnection.On<DrawingState>(UpdateMethodName, state =>
        {
            try
            {
                paths.Clear();

                foreach (var path in state.Paths)
                {
                    var drawingPath = new DrawingPath(path.ColorIndex, path.Thickness);
                    foreach (var point in path.Points)
                    {
                        drawingPath.Add(new PointF(point.X, point.Y));
                    }
                    paths.Add(drawingPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to receive state");
                Console.WriteLine(ex);
            }
        });

        // TODO: Guess message
        // TODO: Assign IsViewing?

        return hubConnection.StartAsync();
    }

    public void StartDrawing(PointF startLocation)
    {
        if (IsViewing)
        {
            return;
        }



        currentPath = new DrawingPath((short)SupportedColors.IndexOf(SelectedColor), LineThickness);
        currentPath.Add(startLocation);
        paths.Add(currentPath);

        SendUpdate();
    }

    public void UpdateDrawing(PointF currentLocation)
    {
        if (IsViewing)
        {
            return;
        }

        currentPath.Add(currentLocation);

        SendUpdate();
    }

    public void EndDrawing(PointF endLocation)
    {
        if (IsViewing)
        {
            return;
        }

        currentPath.Add(endLocation);

        SendUpdate();
    }

    public void Clear()
    {
        if (IsViewing)
        {
            return;
        }

        paths.Clear();

        SendUpdate();
    }

    private async void SendUpdate()
    {
        var state = new DrawingState
        {
            Paths = paths.Select(p => new DrawingPathState
            {
                ColorIndex = p.ColorIndex,
                Thickness = (int)p.Thickness,
                Points = p.Path.Points.Select(point => new System.Drawing.Point((int)point.X, (int)point.Y)).ToList()
            }).ToList()
        };

        await hubConnection.SendAsync(UpdateMethodName, state);
    }
}
