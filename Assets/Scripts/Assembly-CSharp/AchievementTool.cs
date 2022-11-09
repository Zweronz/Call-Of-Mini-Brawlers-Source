using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public static class AchievementTool
{
	[CompilerGenerated]
	private static Action<IAchievement> _003C_003Ef__am_0024cache0;

	[CompilerGenerated]
	private static Action<IAchievement> _003C_003Ef__am_0024cache1;

	public static void InitializeAllAchievements()
	{
		DataCenter.Instance.Achievements.InitializeAllAchievements();
	}

	public static void CalculateAchievements()
	{
		List<IAchievement> all = DataCenter.Instance.Achievements.GetAll();
		if (_003C_003Ef__am_0024cache0 == null)
		{
			_003C_003Ef__am_0024cache0 = _003CCalculateAchievements_003Em__2;
		}
		all.ForEach(_003C_003Ef__am_0024cache0);
		InitializeAllAchievements();
	}

	public static void ClearAchievements()
	{
		List<IAchievement> all = DataCenter.Instance.Achievements.GetAll();
		if (_003C_003Ef__am_0024cache1 == null)
		{
			_003C_003Ef__am_0024cache1 = _003CClearAchievements_003Em__3;
		}
		all.ForEach(_003C_003Ef__am_0024cache1);
	}

	public static int GetKillAllZombieCount()
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				foreach (int value2 in mission.killedZombiesByZombieId.Values)
				{
					num += value2;
				}
			}
		}
		return num;
	}

	public static int GetOnlyUseGunCompletedMissionCount(string typeName)
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				if (mission.isCompleted && mission.usedMeleeWeapon.Keys.Count == 0)
				{
					List<string> list = new List<string>();
					list.AddRange(mission.usedGun.Keys);
					if (list.Count == 1 && typeName.Equals(DataCenter.Instance.Guns.Find(list[0]).typeName))
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	public static int GetTapOnScreen()
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				num += mission.tapOnScreen;
			}
		}
		return num;
	}

	public static int GetLessHpCompletedMission(float rate)
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				if (mission.isCompleted)
				{
					float num2 = mission.residualHp / mission.maxHp;
					if (num2 <= rate)
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	public static int GetDontDestroyChestCompletedMission()
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				if (mission.isCompleted && mission.destroyedChest == 0)
				{
					num++;
				}
			}
		}
		return num;
	}

	public static int GetDestroyDangerousChest()
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				num += mission.destroyedDangerousChest;
			}
		}
		return num;
	}

	public static int GetUsedItemCount(string itemId)
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				if (mission.usedItem.ContainsKey(itemId))
				{
					num += mission.usedItem[itemId];
				}
			}
		}
		return num;
	}

	public static int GetOnlyUseMeleeWeaponCompletedMissionCount(string typeName)
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				if (mission.isCompleted && mission.usedGun.Keys.Count == 0)
				{
					List<string> list = new List<string>();
					list.AddRange(mission.usedMeleeWeapon.Keys);
					if (list.Count == 1 && typeName.Equals(DataCenter.Instance.MeleeWeapons.Find(list[0]).typeName))
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	public static int GetCompleteMissionCount(int missionType)
	{
		int num = 0;
		Player.Data.DetailData detailData = Player.Instance.DetailData;
		foreach (Player.Data.LevelDetailData value in detailData.LevelDetails.Values)
		{
			foreach (MissionDetail.Data mission in value.missions)
			{
				if (mission.isCompleted && DataCenter.Instance.Missions.Find(mission.missionId).Type == missionType)
				{
					num++;
				}
			}
		}
		return num;
	}

	public static int GetDefeatedFriendCount()
	{
		int num = 0;
		foreach (ArenaMissionDetail.Data item in Player.Instance.ArenaDetail)
		{
			if (item.defeatFriends != null)
			{
				num += item.defeatFriends.Count;
			}
		}
		return num;
	}

	[CompilerGenerated]
	private static void _003CCalculateAchievements_003Em__2(IAchievement achievement)
	{
		achievement.Process();
	}

	[CompilerGenerated]
	private static void _003CClearAchievements_003Em__3(IAchievement achievement)
	{
		achievement.Clear();
	}
}
