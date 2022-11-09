using System;

[Serializable]
public class Achievement10007 : Achievement<AchievementData10007>
{
	protected override void DoProcess()
	{
		base.Progress = (float)Player.Instance.HeroLevel * 1f / (float)data.heroLevel;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
