using System;

[Serializable]
public class Achievement10003 : Achievement<AchievementData10003>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetOnlyUseGunCompletedMissionCount(data.gunTypeName) * 1f / (float)data.levelCount;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
