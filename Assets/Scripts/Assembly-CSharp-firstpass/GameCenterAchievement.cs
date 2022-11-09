using System;
using System.Collections.Generic;
using Prime31;

public class GameCenterAchievement
{
	public string identifier;

	public bool isHidden;

	public bool completed;

	public DateTime lastReportedDate;

	public float percentComplete;

	public GameCenterAchievement(Dictionary<string, object> dict)
	{
		if (dict.ContainsKey("identifier"))
		{
			identifier = dict["identifier"] as string;
		}
		if (dict.ContainsKey("hidden"))
		{
			isHidden = (bool)dict["hidden"];
		}
		if (dict.ContainsKey("completed"))
		{
			completed = (bool)dict["completed"];
		}
		if (dict.ContainsKey("percentComplete"))
		{
			percentComplete = float.Parse(dict["percentComplete"].ToString());
		}
		if (dict.ContainsKey("lastReportedDate"))
		{
			double value = double.Parse(dict["lastReportedDate"].ToString());
			lastReportedDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(value);
		}
	}

	public static List<GameCenterAchievement> fromJSON(string json)
	{
		List<GameCenterAchievement> list = new List<GameCenterAchievement>();
		List<object> list2 = json.listFromJson();
		foreach (Dictionary<string, object> item in list2)
		{
			list.Add(new GameCenterAchievement(item));
		}
		return list;
	}

	public override string ToString()
	{
		return string.Format("<Achievement> identifier: {0}, hidden: {1}, completed: {2}, percentComplete: {3}, lastReported: {4}", identifier, isHidden, completed, percentComplete, lastReportedDate);
	}
}
