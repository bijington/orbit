using DrawingGame.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace DrawingGame;

public class DrawingManager
{
    private readonly IList<DrawingPath> paths = new List<DrawingPath>();
    private DrawingPath currentPath;
    private HubConnection hubConnection;
    private const string UpdateMethodName = "UpdateDrawingState";

    public bool IsViewing { get; }

    public float LineThickness { get; set; } = 5f;

    public Color SelectedColor { get; set; } = Colors.Black;

    public IReadOnlyList<DrawingPath> Paths => paths.ToList();

    public Task StartGame()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7030/Game")
            .Build();

        hubConnection.On<DrawingState>(UpdateMethodName, msg =>
        {
            //try
            //{
            //    Console.WriteLine($"Received message {msg.X}");
            //    this.onUpdatePlayerState?.Invoke(msg);
            //    //Console.WriteLine($"From {msg.UserName}");
            //    //UpdateProperties(msg);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Failed to receive message");
            //    Console.WriteLine(ex.Message);
            //}
        });
        // Guess message
        // Assign IsViewing?

        return hubConnection.StartAsync();
    }

    public void StartDrawing(PointF startLocation)
    {
        if (IsViewing)
        {
            return;
        }

        currentPath = new DrawingPath(SelectedColor, LineThickness);
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
                Color = p.Color.ToHex(),
                Thickness = (int)p.Thickness,
                Points = p.Path.Points.Select(point => new System.Drawing.Point((int)point.X, (int)point.Y)).ToList()
            }).ToList()
        };

        //await hubConnection.SendAsync(UpdateMethodName, state);
    }
}
