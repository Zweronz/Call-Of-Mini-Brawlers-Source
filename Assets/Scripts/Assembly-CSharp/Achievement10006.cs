using System;

[Serializable]
public class Achievement10006 : Achievement<AchievementData10006>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetLessHpCompletedMission(data.rate) * 1f / data.levelCount;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
