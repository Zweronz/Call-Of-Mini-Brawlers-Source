using System;

[Serializable]
public class Achievement10008 : Achievement<AchievementData10008>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetDontDestroyChestCompletedMission() * 1f / (float)data.levelCount;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
