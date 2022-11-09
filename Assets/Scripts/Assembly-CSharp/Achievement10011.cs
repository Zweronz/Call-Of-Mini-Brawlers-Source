using System;

[Serializable]
public class Achievement10011 : Achievement<AchievementData10011>
{
	protected override void DoProcess()
	{
		base.Progress = (float)AchievementTool.GetOnlyUseMeleeWeaponCompletedMissionCount(data.meleeWeaponTypeName) * 1f / (float)data.levelCount;
		if (Progress > 1f)
		{
			base.Progress = 1f;
		}
	}
}
