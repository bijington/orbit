namespace BuildingGames.Models;

public class Character
{
	public string Name { get; init; }

	public IReadOnlyList<string> Strengths { get; init; }

    public IReadOnlyList<string> Weaknesses { get; init; }

	public string ImageName { get; init; }

	public bool IsLocked { get; init; }

	public string UnlockCriteria { get; init; }
}
