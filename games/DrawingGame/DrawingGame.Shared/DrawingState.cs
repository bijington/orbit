namespace DrawingGame.Shared;

public class DrawingState
{
    public TimeSpan TimeRemaining { get; set; }

    public IList<DrawingPathState> Paths { get; set; } = new List<DrawingPathState>();
}
