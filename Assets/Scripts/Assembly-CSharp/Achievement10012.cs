using System;

[Serializable]
public class Achievement10012 : Achievement<AchievementData10012>
{
	protected override void DoProcess()
	{
		base.Progress = (float)Player.Instance.ArenaScore * 1f / (float)data.score;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
