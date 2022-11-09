public class GameOverEvent
{
	public bool Completed { get; private set; }

	public int AllGold { get; private set; }

	public int Gold { get; private set; }

	public int Kills { get; private set; }

	public int Bonus { get; private set; }

	public GameOverEvent(bool completed, int allgold, int gold, int kills, int bonus)
	{
		Completed = completed;
		AllGold = allgold;
		Gold = gold;
		Kills = kills;
		Bonus = bonus;
	}
}
