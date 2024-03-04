using System;
using System.Collections.Generic;
using Prime31;

[Serializable]
public class GameCenterScore
{
	public string category;

	public string formattedValue;

	public long value;

	public long context;

	public DateTime date;

	public string playerId;

	public int rank;

	public bool isFriend;

	public string alias;

	public int maxRange;

	public GameCenterScore()
	{
	}

	public GameCenterScore(Dictionary<string, object> ht)
	{
		if (ht.ContainsKey("category"))
		{
			category = ht["category"] as string;
		}
		if (ht.ContainsKey("formattedValue"))
		{
			formattedValue = ht["formattedValue"] as string;
		}
		if (ht.ContainsKey("value"))
		{
			value = long.Parse(ht["value"].ToString());
		}
		if (ht.ContainsKey("context"))
		{
			context = long.Parse(ht["context"].ToString());
		}
		if (ht.ContainsKey("playerId"))
		{
			playerId = ht["playerId"] as string;
		}
		if (ht.ContainsKey("rank"))
		{
			rank = int.Parse(ht["rank"].ToString());
		}
		if (ht.ContainsKey("isFriend"))
		{
			isFriend = (bool)ht["isFriend"];
		}
		if (ht.ContainsKey("alias"))
		{
			alias = ht["alias"] as string;
		}
		else
		{
			alias = "Anonymous";
		}
		if (ht.ContainsKey("maxRange"))
		{
			maxRange = int.Parse(ht["maxRange"].ToString());
		}
		if (ht.ContainsKey("date"))
		{
			double num = double.Parse(ht["date"].ToString());
			date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(num);
		}
	}

	public static List<GameCenterScore> fromJSON(string json)
	{
		List<GameCenterScore> list = new List<GameCenterScore>();
		List<object> list2 = json.listFromJson();
		foreach (Dictionary<string, object> item in list2)
		{
			list.Add(new GameCenterScore(item));
		}
		return list;
	}

	public override string ToString()
	{
		return string.Format("<Score> category: {0}, formattedValue: {1}, date: {2}, rank: {3}, alias: {4}, maxRange: {5}", category, formattedValue, date, rank, alias, maxRange);
	}
}
