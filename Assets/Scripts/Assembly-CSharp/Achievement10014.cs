using System;

[Serializable]
public class Achievement10014 : Achievement<AchievementData10014>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetDefeatedFriendCount() * 1f / (float)data.count;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
