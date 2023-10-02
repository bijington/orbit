namespace BuildingGames;

public class Decisions
{
    public IList<string> History { get; } = new List<string>();

    public void RecordDecision(string decision)
	{
        History.Add(decision);
	}
}
