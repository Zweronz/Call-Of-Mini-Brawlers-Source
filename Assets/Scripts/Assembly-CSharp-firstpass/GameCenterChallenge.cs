using System;
using System.Collections.Generic;

public class GameCenterChallenge
{
	public string issuingPlayerID;

	public string receivingPlayerID;

	public GameCenterChallengeState state;

	public DateTime issueDate;

	public DateTime completionDate;

	public string message;

	public GameCenterScore score;

	public GameCenterAchievement achievement;

	public GameCenterChallenge(Dictionary<string, object> dict)
	{
		if (dict.ContainsKey("issuingPlayerID"))
		{
			issuingPlayerID = dict["issuingPlayerID"] as string;
		}
		if (dict.ContainsKey("receivingPlayerID"))
		{
			receivingPlayerID = dict["receivingPlayerID"] as string;
		}
		if (dict.ContainsKey("state"))
		{
			int num = int.Parse(dict["state"].ToString());
			state = (GameCenterChallengeState)num;
		}
		if (dict.ContainsKey("issueDate"))
		{
			double value = double.Parse(dict["issueDate"].ToString());
			issueDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(value);
		}
		if (dict.ContainsKey("completionDate"))
		{
			double value2 = double.Parse(dict["completionDate"].ToString());
			completionDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(value2);
		}
		if (dict.ContainsKey("message"))
		{
			message = dict["message"] as string;
		}
		if (dict.ContainsKey("score"))
		{
			score = new GameCenterScore(dict["score"] as Dictionary<string, object>);
		}
		if (dict.ContainsKey("achievement"))
		{
			achievement = new GameCenterAchievement(dict["achievement"] as Dictionary<string, object>);
		}
	}

	public override string ToString()
	{
		return string.Format("<Challenge> issuingPlayerID: {0}, receivingPlayerID: {1}, message: {2}, state: {3}, score: {4}, achievement: {5}", issuingPlayerID, receivingPlayerID, message, state, score, achievement);
	}
}
