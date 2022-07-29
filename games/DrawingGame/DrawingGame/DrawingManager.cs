using System.Collections.ObjectModel;
using DrawingGame.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace DrawingGame;

public class DrawingManager : BindableObject
{
    private readonly IList<DrawingPath> paths = new List<DrawingPath>();
    private DrawingPath currentPath;
    private string groupName;
    private HubConnection hubConnection;
    private Color selectedColor = Colors.Black;
    private TimeSpan timeRemaining;

    private const string PlayerConnectedName = "PlayerConnected";
    private const string SessionStartedName = "SessionStarted";
    private const string UpdateMethodName = "UpdateDrawingState";

    public bool IsPrimary { get; set; }

    public bool IsViewing => !IsPrimary;

    public float LineThickness { get; set; } = 5f;

    public string Word { get; private set; }

    public Color SelectedColor
    {
        get => selectedColor;
        set
        {
            selectedColor = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan TimeRemaining
    {
        get => timeRemaining;
        set
        {
            timeRemaining = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Player> Players { get; } = new ObservableCollection<Player>();

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

    public async Task StartGame(string name, string groupName, bool isCreatingGame)
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://drawinggame-server.azurewebsites.net/Game")
            .Build();

        this.groupName = groupName;

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

        hubConnection.On<Player>(PlayerConnectedName, player =>
        {
            Players.Add(player);
        });

        hubConnection.On<SessionStarted>(SessionStartedName, session =>
        {
            Word = session.Word;
            Dispatcher.Dispatch(async () => await Shell.Current.GoToAsync("main"));
        });

        // TODO: Guess message
        // TODO: Assign IsViewing?

        await hubConnection.StartAsync();

        await hubConnection.SendAsync(
            PlayerConnectedName,
            new Player
            {
                GroupName = groupName,
                Name = name
            });
    }

    public async Task StartSession(string word)
    {
        Word = word;

        await hubConnection.SendAsync(
            SessionStartedName,
            new SessionStarted
            {
                Word = word,
                GroupName = groupName
            });
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
            }).ToList(),
            TimeRemaining = TimeRemaining,
            GroupName = groupName
        };

        await hubConnection.SendAsync(UpdateMethodName, state);
    }
}
