using System;

[Serializable]
public class Achievement10009 : Achievement<AchievementData10009>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetDestroyDangerousChest() * 1f / (float)data.count;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
