using System;

[Serializable]
public class Achievement10013 : Achievement<AchievementData10013>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetCompleteMissionCount(data.missionType) * 1f / (float)data.count;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
