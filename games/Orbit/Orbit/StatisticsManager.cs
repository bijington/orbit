namespace Orbit;

public class StatisticsManager
{
	public int Score { get; private set; }

	public void RegisterScore(int score)
	{
		Score += score;
	}
}
