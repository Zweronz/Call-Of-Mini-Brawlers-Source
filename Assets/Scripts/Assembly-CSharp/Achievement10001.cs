using System;

[Serializable]
public class Achievement10001 : Achievement<AchievementData10001>
{
	protected override void DoProcess()
	{
		base.Progress = ((Player.Instance.GameLevel >= data.levelId + 1) ? 1 : 0);
	}
}
