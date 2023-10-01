namespace BuildingGames;

public class Achievement
{
	public string Name { get; init; }

	public string Description { get; init; }

	public double Progress { get; set; }
}

public static class AchievementNames
{
	public const string FirstDecision = "Decision maker";

	public const string NextSpeaker = "Next!";
}