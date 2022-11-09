using System.Collections.Generic;
using Prime31;

public class GameCenterLeaderboard
{
	public string leaderboardId;

	public string title;

	public GameCenterLeaderboard(string leaderboardId, string title)
	{
		this.leaderboardId = leaderboardId;
		this.title = title;
	}

	public static List<GameCenterLeaderboard> fromJSON(string json)
	{
		List<GameCenterLeaderboard> list = new List<GameCenterLeaderboard>();
		Dictionary<string, object> dictionary = json.dictionaryFromJson();
		foreach (KeyValuePair<string, object> item in dictionary)
		{
			list.Add(new GameCenterLeaderboard(item.Value as string, item.Key));
		}
		return list;
	}

	public override string ToString()
	{
		return string.Format("<Leaderboard> leaderboardId: {0}, title: {1}", leaderboardId, title);
	}
}
