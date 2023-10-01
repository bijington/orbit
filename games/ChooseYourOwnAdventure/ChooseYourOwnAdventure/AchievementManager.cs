namespace BuildingGames;

public class AchievementManager
{
	private readonly IList<Achievement> achievements;

	public event Action<Achievement> AchievementUnlocked;

	public AchievementManager()
	{
		this.achievements = new List<Achievement>
		{
			new Achievement { Name = AchievementNames.FirstDecision, Description = "Made your first decision" },
            new Achievement { Name = AchievementNames.StaleMate, Description = "You couldn't decide either?" },
            new Achievement { Name = "Social gamer", Description = "" },
			new Achievement { Name = AchievementNames.NextSpeaker, Description = "Unlocked the next speaker"}
        };
	}

	public void UpdateProgress(string achievementTitle, double progress)
	{
		var achievement = this.achievements.FirstOrDefault(a => a.Name == achievementTitle);

		if (achievement is null)
		{
			return;
		}

		if (achievement.Progress == 100)
		{
			return;
		}

		achievement.Progress = progress;

		if (progress == 100)
		{
			this.AchievementUnlocked?.Invoke(achievement);
		}
	}
}
