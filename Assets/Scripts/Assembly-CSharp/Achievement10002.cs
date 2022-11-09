using System;

[Serializable]
public class Achievement10002 : Achievement<AchievementData10002>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetKillAllZombieCount() * 1f / (float)data.zombieCount;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
