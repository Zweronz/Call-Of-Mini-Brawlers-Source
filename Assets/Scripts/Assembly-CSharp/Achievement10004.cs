using System;
using System.Collections.Generic;

[Serializable]
public class Achievement10004 : Achievement<AchievementData10004>
{
	protected override void DoProcess()
	{
		base.Progress = 0f;
		GunData gunData = DataCenter.Instance.Guns.Find(data.gunId);
		if (Player.Instance.ContainsGunType(gunData.typeName))
		{
			string gunIdByType = Player.Instance.GetGunIdByType(gunData.typeName);
			List<GunData> list = DataCenter.Instance.Guns.FindByTypeName(gunData.typeName);
			if (list.IndexOf(DataCenter.Instance.Guns.Find(gunIdByType)) >= list.IndexOf(gunData))
			{
				base.Progress = 1f;
			}
		}
	}
}
