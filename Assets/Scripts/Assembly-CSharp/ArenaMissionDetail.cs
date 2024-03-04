using System;
using System.Collections.Generic;
using Event;

public class ArenaMissionDetail
{
	[Serializable]
	public class Data
	{
		public int level;

		public string missionId;

		public bool isCompleted;

		public double getGold;

		public double getExp;

		public double bonus;

		public int heroId;

		public int enterLevel;

		public int levelUp;

		public float residualHp;

		public float maxHp;

		public SerializableDictionary<string, int> usedGun;

		public SerializableDictionary<string, int> usedMeleeWeapon;

		public SerializableDictionary<string, int> usedItem;

		public SerializableDictionary<int, int> killedZombiesByZombieId;

		public SerializableDictionary<string, int> killedZombiesByGunId;

		public SerializableDictionary<string, int> killedZombiesByMeleeWeaponId;

		public SerializableDictionary<string, int> killedZombiesByItemId;

		public int tapOnScreen;

		public int destroyedChest;

		public int destroyedDangerousChest;

		public int reviveCount;

		public long arenaScore;

		public HashSet<string> defeatFriends;
	}

	public Data MissionDetailData { get; private set; }

	public ArenaMissionDetail()
	{
		MissionDetailData = new Data();
		MissionDetailData.usedGun = new SerializableDictionary<string, int>();
		MissionDetailData.usedMeleeWeapon = new SerializableDictionary<string, int>();
		MissionDetailData.usedItem = new SerializableDictionary<string, int>();
		MissionDetailData.killedZombiesByZombieId = new SerializableDictionary<int, int>();
		MissionDetailData.killedZombiesByGunId = new SerializableDictionary<string, int>();
		MissionDetailData.killedZombiesByMeleeWeaponId = new SerializableDictionary<string, int>();
		MissionDetailData.killedZombiesByItemId = new SerializableDictionary<string, int>();
		MissionDetailData.defeatFriends = new HashSet<string>();
		EventCenter.Instance.Register<ZombieDeadEvent>(HandleZombieDeadEvent);
		EventCenter.Instance.Register<UseGunEvent>(HandleUseGunEvent);
		EventCenter.Instance.Register<UseMeleeWeaponEvent>(HandleUseMeleeWeaponEvent);
		EventCenter.Instance.Register<UseItemEvent>(HandleUseItemEvent);
		EventCenter.Instance.Register<DestroyChestEvent>(HandleDestroyChestEvent);
		EventCenter.Instance.Register<DestroyDangerousChestEvent>(HandleDestroyDangerousChestEvent);
		EventCenter.Instance.Register<TapOnScreenEvent>(HandleTapOnScreen);
	}

	public void DestroyListener()
	{
		EventCenter.Instance.Unregister<ZombieDeadEvent>(HandleZombieDeadEvent);
		EventCenter.Instance.Unregister<UseGunEvent>(HandleUseGunEvent);
		EventCenter.Instance.Unregister<UseMeleeWeaponEvent>(HandleUseMeleeWeaponEvent);
		EventCenter.Instance.Unregister<UseItemEvent>(HandleUseItemEvent);
		EventCenter.Instance.Unregister<DestroyChestEvent>(HandleDestroyChestEvent);
		EventCenter.Instance.Unregister<DestroyDangerousChestEvent>(HandleDestroyDangerousChestEvent);
		EventCenter.Instance.Unregister<TapOnScreenEvent>(HandleTapOnScreen);
	}

	public void ChooseDefeatFriends(List<GameCenterModel.FriendScore> fsList, long score)
	{
		fsList.ForEach(delegate(GameCenterModel.FriendScore fs)
		{
			if (fs.score.value < score)
			{
				MissionDetailData.defeatFriends.Add(fs.score.playerId);
			}
		});
		if (MissionDetailData.defeatFriends.Contains(GameCenterBinding.playerIdentifier()))
		{
			MissionDetailData.defeatFriends.Remove(GameCenterBinding.playerIdentifier());
		}
	}

	private void HandleZombieDeadEvent(object sender, ZombieDeadEvent evt)
	{
		if (MissionDetailData.killedZombiesByZombieId.ContainsKey(evt.ZombieID))
		{
			SerializableDictionary<int, int> killedZombiesByZombieId;
			SerializableDictionary<int, int> serializableDictionary = (killedZombiesByZombieId = MissionDetailData.killedZombiesByZombieId);
			int zombieID;
			int key = (zombieID = evt.ZombieID);
			zombieID = killedZombiesByZombieId[zombieID];
			serializableDictionary[key] = zombieID + 1;
		}
		else
		{
			MissionDetailData.killedZombiesByZombieId.Add(evt.ZombieID, 1);
		}
		Dictionary<string, int> dictionary;
		switch (evt.Type)
		{
		case ZombieDeadEvent.WeaponType.Item:
			dictionary = MissionDetailData.killedZombiesByItemId;
			break;
		case ZombieDeadEvent.WeaponType.MeleeWeapon:
			dictionary = MissionDetailData.killedZombiesByMeleeWeaponId;
			break;
		default:
			dictionary = MissionDetailData.killedZombiesByGunId;
			break;
		}
		if (dictionary.ContainsKey(evt.WeaponID))
		{
			Dictionary<string, int> dictionary2;
			Dictionary<string, int> dictionary3 = (dictionary2 = dictionary);
			string weaponID;
			string key2 = (weaponID = evt.WeaponID);
			int zombieID = dictionary2[weaponID];
			dictionary3[key2] = zombieID + 1;
		}
		else
		{
			dictionary.Add(evt.WeaponID, 1);
		}
	}

	private void HandleUseGunEvent(object sender, UseGunEvent evt)
	{
		if (MissionDetailData.usedGun.ContainsKey(evt.GunID))
		{
			SerializableDictionary<string, int> usedGun;
			SerializableDictionary<string, int> serializableDictionary = (usedGun = MissionDetailData.usedGun);
			string gunID;
			string key = (gunID = evt.GunID);
			int num = usedGun[gunID];
			serializableDictionary[key] = num + 1;
		}
		else
		{
			MissionDetailData.usedGun.Add(evt.GunID, 1);
		}
	}

	private void HandleUseMeleeWeaponEvent(object sender, UseMeleeWeaponEvent evt)
	{
		if (MissionDetailData.usedMeleeWeapon.ContainsKey(evt.MeleeWeaponID))
		{
			SerializableDictionary<string, int> usedMeleeWeapon;
			SerializableDictionary<string, int> serializableDictionary = (usedMeleeWeapon = MissionDetailData.usedMeleeWeapon);
			string meleeWeaponID;
			string key = (meleeWeaponID = evt.MeleeWeaponID);
			int num = usedMeleeWeapon[meleeWeaponID];
			serializableDictionary[key] = num + 1;
		}
		else
		{
			MissionDetailData.usedMeleeWeapon.Add(evt.MeleeWeaponID, 1);
		}
	}

	private void HandleUseItemEvent(object sender, UseItemEvent evt)
	{
		if (MissionDetailData.usedItem.ContainsKey(evt.ItemID))
		{
			SerializableDictionary<string, int> usedItem;
			SerializableDictionary<string, int> serializableDictionary = (usedItem = MissionDetailData.usedItem);
			string itemID;
			string key = (itemID = evt.ItemID);
			int num = usedItem[itemID];
			serializableDictionary[key] = num + 1;
		}
		else
		{
			MissionDetailData.usedItem.Add(evt.ItemID, 1);
		}
	}

	private void HandleDestroyChestEvent(object sneder, DestroyChestEvent evt)
	{
		MissionDetailData.destroyedChest++;
	}

	private void HandleDestroyDangerousChestEvent(object sneder, DestroyDangerousChestEvent evt)
	{
		MissionDetailData.destroyedDangerousChest++;
	}

	private void HandleTapOnScreen(object sneder, TapOnScreenEvent evt)
	{
		MissionDetailData.tapOnScreen++;
	}

	private void HandleDestroyFriendChestEvent(object sneder, DestroyFriendChestEvent evt)
	{
		if (!MissionDetailData.defeatFriends.Contains(evt.Friend.playerId))
		{
			MissionDetailData.defeatFriends.Add(evt.Friend.playerId);
		}
	}
}
