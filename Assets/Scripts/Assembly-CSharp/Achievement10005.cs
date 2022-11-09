using System;

[Serializable]
public class Achievement10005 : Achievement<AchievementData10005>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetTapOnScreen() * 1f / (float)data.count;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
