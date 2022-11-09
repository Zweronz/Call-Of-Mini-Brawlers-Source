using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

public class Player
{
	[Serializable]
	public class Data
	{
		[Serializable]
		public enum SpecialIAPState
		{
			NotShow = 0,
			Showing = 1,
			Showed = 2
		}

		[Serializable]
		public class HeroData
		{
			public int id;

			public string meleeWeapon;
		}

		[Serializable]
		public class LevelDetailData
		{
			public int level;

			public List<MissionDetail.Data> missions;
		}

		[Serializable]
		public class DetailData
		{
			public SerializableDictionary<int, LevelDetailData> LevelDetails;
		}

		public string version;

		public double gold;

		public double crystal;

		public int gameLevel;

		public int heroLevel;

		public double exp;

		public int currentHeroID;

		public List<HeroData> heros;

		public List<string> guns;

		public List<string> items;

		public SerializableDictionary<string, string> ownGuns;

		public SerializableDictionary<string, string> ownMeleeWeapons;

		public SerializableDictionary<int, string> completedAchievements;

		public SerializableDictionary<string, int> ownItems;

		public SerializableDictionary<int, string> missions;

		public SerializableDictionary<string, int> iaps;

		public HashSet<string> unlockedGuns;

		public bool refreshedMission;

		public bool showedReview;

		public int showedReviewCount;

		public bool activeSpecialIAPShowed;

		public int currentSpecialIAP;

		public long specialIAPTime;

		public long lastLoginTime;

		public DetailData detail;

		public List<ArenaMissionDetail.Data> aneraDetail;

		public long arenaScore;

		public List<GameCenterModel.FriendScore> friends;

		public bool hasUpdateExp;
	}

	[CompilerGenerated]
	private sealed class _003CFindHero_003Ec__AnonStorey21
	{
		internal int id;

		internal bool _003C_003Em__D(Data.HeroData hd)
		{
			return id == hd.id;
		}
	}

	private static Player instance;

	private bool isDirty;

	private Data data;

	private DataReadWriteModel deadWriteModel;

	private Data.HeroData currentHero;

	private bool todayIsActive;

	private long todayTicks;

	[NonSerialized]
	private MissionDetail.Data lastMissionData;

	[NonSerialized]
	private ArenaMissionDetail.Data lastArenaMissionData;

	[NonSerialized]
	private HashSet<string> newUnlocked = new HashSet<string>();

	private Data tempData;

	[CompilerGenerated]
	private static Predicate<SpecailIAPData> _003C_003Ef__am_0024cacheD;

	public static Player Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Player();
				instance.Load();
			}
			return instance;
		}
	}

	public Data.HeroData CurrentHero
	{
		get
		{
			return currentHero;
		}
	}

	public List<string> Guns
	{
		get
		{
			return data.guns;
		}
	}

	public List<string> Items
	{
		get
		{
			return data.items;
		}
	}

	public int GameLevel
	{
		get
		{
			return data.gameLevel;
		}
		set
		{
			data.gameLevel = value;
			isDirty = true;
		}
	}

	public double Gold
	{
		get
		{
			return data.gold;
		}
	}

	public double Crystal
	{
		get
		{
			return data.crystal;
		}
	}

	public int HeroLevel
	{
		get
		{
			return data.heroLevel;
		}
		set
		{
			data.heroLevel = value;
			isDirty = true;
		}
	}

	public double Exp
	{
		get
		{
			return data.exp;
		}
	}

	public bool NeedRefreshMission
	{
		get
		{
			return !data.refreshedMission;
		}
		set
		{
			if (data.refreshedMission == value)
			{
				data.refreshedMission = !value;
				isDirty = true;
			}
		}
	}

	public bool ShowedReview
	{
		get
		{
			return data.showedReview;
		}
		set
		{
			if (data.showedReview != value)
			{
				data.showedReview = value;
				isDirty = true;
			}
		}
	}

	public int ShowedReviewCount
	{
		get
		{
			return data.showedReviewCount;
		}
		set
		{
			if (data.showedReviewCount != value)
			{
				data.showedReviewCount = value;
				isDirty = true;
			}
		}
	}

	public SerializableDictionary<int, string> Missions
	{
		get
		{
			if (data.missions == null)
			{
				data.missions = new SerializableDictionary<int, string>();
			}
			return data.missions;
		}
	}

	public Data.DetailData DetailData
	{
		get
		{
			return data.detail;
		}
	}

	public MissionDetail.Data LastMissionData
	{
		get
		{
			return lastMissionData;
		}
		set
		{
			lastMissionData = value;
		}
	}

	public ArenaMissionDetail.Data LastArenaMissionData
	{
		get
		{
			return lastArenaMissionData;
		}
		set
		{
			lastArenaMissionData = value;
		}
	}

	public List<ArenaMissionDetail.Data> ArenaDetail
	{
		get
		{
			return data.aneraDetail;
		}
	}

	public int CurrentSpecialIAP
	{
		get
		{
			return data.currentSpecialIAP;
		}
		set
		{
			if (value != data.currentSpecialIAP)
			{
				data.currentSpecialIAP = value;
				isDirty = true;
			}
		}
	}

	public bool ActiveSpecialIAPShowed
	{
		get
		{
			return data.activeSpecialIAPShowed;
		}
		set
		{
			if (value != data.activeSpecialIAPShowed)
			{
				data.activeSpecialIAPShowed = value;
				isDirty = true;
			}
		}
	}

	private long LastLoginTime
	{
		get
		{
			return data.lastLoginTime;
		}
		set
		{
			if (value != data.lastLoginTime)
			{
				data.lastLoginTime = value;
				isDirty = true;
			}
		}
	}

	public long SpecialIAPTime
	{
		get
		{
			return data.specialIAPTime;
		}
		set
		{
			if (value != data.specialIAPTime)
			{
				data.specialIAPTime = value;
				isDirty = true;
			}
		}
	}

	public bool IsActivePlayer { get; private set; }

	public string Version
	{
		get
		{
			return data.version;
		}
		private set
		{
			data.version = value;
		}
	}

	public long ArenaScore
	{
		get
		{
			return data.arenaScore;
		}
		set
		{
			if (value > data.arenaScore)
			{
				data.arenaScore = value;
				isDirty = true;
			}
		}
	}

	public List<GameCenterModel.FriendScore> Friends
	{
		get
		{
			return data.friends;
		}
		set
		{
			data.friends.Clear();
			if (value != null)
			{
				data.friends.AddRange(value);
			}
			isDirty = true;
		}
	}

	public HashSet<string> NewUnlockedGuns
	{
		get
		{
			return newUnlocked;
		}
	}

	public bool IsNewVersion { get; private set; }

	public void AddIAP(IAPData data)
	{
		AddGold(data.gold);
		AddCrystal(data.crystal);
		AddIAPCount(data.id, 1);
		isDirty = true;
	}

	public int AddExp(double exp)
	{
		int num = 0;
		data.exp += exp;
		while (data.exp > 0.0)
		{
			HeroLevelUpExpData heroLevelUpExpData = DataCenter.Instance.HeroLevelUpDatas.Find(HeroLevel);
			if (heroLevelUpExpData != null && data.exp >= (double)heroLevelUpExpData.exp)
			{
				num++;
				HeroLevel++;
				data.exp -= heroLevelUpExpData.exp;
				continue;
			}
			break;
		}
		isDirty = true;
		return num;
	}

	public void AddGold(double gold)
	{
		double num = data.gold + gold;
		if (num >= 99999999.0)
		{
			data.gold = 99999999.0;
		}
		else
		{
			data.gold = num;
		}
		isDirty = true;
	}

	public void AddCrystal(double crystal)
	{
		data.crystal += crystal;
		isDirty = true;
	}

	public void AddHero(Data.HeroData hero)
	{
		if (FindHero(hero.id) != null)
		{
			return;
		}
		HeroData heroData = DataCenter.Instance.Heros.Find(hero.id);
		foreach (string item in heroData.gunsWithBirth)
		{
			GunData gunData = DataCenter.Instance.Guns.Find(item);
			if (ContainsGunType(gunData.typeName))
			{
				List<GunData> list = DataCenter.Instance.Guns.FindByTypeName(gunData.typeName);
				int num = list.IndexOf(gunData);
				int num2 = list.IndexOf(DataCenter.Instance.Guns.Find(data.ownGuns[gunData.typeName]));
				if (num > num2)
				{
					UpdateGun(data.ownGuns[gunData.typeName], gunData.id);
				}
			}
			else
			{
				AddGun(gunData.id);
			}
		}
		MeleeWeaponData meleeWeaponData = DataCenter.Instance.MeleeWeapons.Find(hero.meleeWeapon);
		if (ContainsMeleeWeaponType(meleeWeaponData.typeName))
		{
			List<MeleeWeaponData> list2 = DataCenter.Instance.MeleeWeapons.FindByTypeName(meleeWeaponData.typeName);
			int num3 = list2.IndexOf(meleeWeaponData);
			int num4 = list2.IndexOf(DataCenter.Instance.MeleeWeapons.Find(data.ownMeleeWeapons[meleeWeaponData.typeName]));
			if (num3 > num4)
			{
				UpdateMeleeWeapon(data.ownMeleeWeapons[meleeWeaponData.typeName], meleeWeaponData.id);
			}
		}
		else
		{
			AddMeleeWeapon(meleeWeaponData.id);
		}
		data.heros.Add(hero);
		isDirty = true;
	}

	public int GetItemCount(string itemId)
	{
		return data.ownItems.ContainsKey(itemId) ? data.ownItems[itemId] : 0;
	}

	public void AddItem(string itemId, int count)
	{
		if (!string.IsNullOrEmpty(itemId))
		{
			if (data.ownItems.ContainsKey(itemId))
			{
				SerializableDictionary<string, int> ownItems;
				SerializableDictionary<string, int> serializableDictionary = (ownItems = data.ownItems);
				string key;
				string key2 = (key = itemId);
				int num = ownItems[key];
				serializableDictionary[key2] = num + count;
			}
			else
			{
				data.ownItems.Add(itemId, count);
			}
		}
		isDirty = true;
	}

	public void RemoveItem(string itemId)
	{
		if (data.ownItems.ContainsKey(itemId))
		{
			data.ownItems.Remove(itemId);
		}
		int num = data.items.IndexOf(itemId);
		if (num >= 0)
		{
			data.items[num] = string.Empty;
		}
	}

	public void AddItem2Hero(int heroId, int slot, string itemId)
	{
		for (int num = slot - (data.items.Count - 1); num > 0; num--)
		{
			data.items.Add(string.Empty);
		}
		data.items[slot] = itemId;
		isDirty = true;
	}

	public bool ContainsGunType(string typeName)
	{
		return data.ownGuns != null && data.ownGuns.ContainsKey(typeName);
	}

	public string GetGunIdByType(string typeName)
	{
		return data.ownGuns[typeName];
	}

	public void AddGun(string gunId)
	{
		GunData gunData = DataCenter.Instance.Guns.Find(gunId);
		if (data.ownGuns == null)
		{
			data.ownGuns = new SerializableDictionary<string, string>();
		}
		data.ownGuns.Add(gunData.typeName, gunData.id);
		isDirty = true;
	}

	public void AddGun2Hero(int heroId, int slot, string gunId)
	{
		for (int num = slot - (data.guns.Count - 1); num > 0; num--)
		{
			data.guns.Add(string.Empty);
		}
		data.guns[slot] = gunId;
		isDirty = true;
	}

	public void UpdateGun(string gunId, string nextGunId)
	{
		GunData gunData = DataCenter.Instance.Guns.Find(gunId);
		data.ownGuns[gunData.typeName] = nextGunId;
		if (data.guns.Contains(gunId))
		{
			data.guns[data.guns.IndexOf(gunId)] = nextGunId;
		}
		isDirty = true;
	}

	public bool ContainsMeleeWeaponType(string typeName)
	{
		return data.ownMeleeWeapons != null && data.ownMeleeWeapons.ContainsKey(typeName);
	}

	public string GetMeleeWeaponIdByType(string typeName)
	{
		return data.ownMeleeWeapons[typeName];
	}

	public void AddMeleeWeapon(string meleeWeaponId)
	{
		MeleeWeaponData meleeWeaponData = DataCenter.Instance.MeleeWeapons.Find(meleeWeaponId);
		if (data.ownMeleeWeapons == null)
		{
			data.ownMeleeWeapons = new SerializableDictionary<string, string>();
		}
		data.ownMeleeWeapons.Add(meleeWeaponData.typeName, meleeWeaponData.id);
		isDirty = true;
	}

	public void AddMeleeWeapon2Hero(int heroId, string meleeWeaponId)
	{
		Data.HeroData heroData = FindHero(heroId);
		heroData.meleeWeapon = meleeWeaponId;
		isDirty = true;
	}

	public void UpdateMeleeWeapon(string meleeWeaponId, string nextMeleeWeaponId)
	{
		MeleeWeaponData meleeWeaponData = DataCenter.Instance.MeleeWeapons.Find(nextMeleeWeaponId);
		data.ownMeleeWeapons[meleeWeaponData.typeName] = nextMeleeWeaponId;
		foreach (Data.HeroData hero in data.heros)
		{
			if (hero.meleeWeapon.Equals(meleeWeaponId))
			{
				hero.meleeWeapon = nextMeleeWeaponId;
			}
		}
		isDirty = true;
	}

	public void SetCurrentHero(int id)
	{
		data.currentHeroID = id;
		currentHero = FindHero(id);
		isDirty = true;
	}

	public bool ContainscompletedAchievementType(int type)
	{
		return data.completedAchievements != null && data.completedAchievements.ContainsKey(type);
	}

	public string GetCompletedAchievementIdByType(int type)
	{
		return data.completedAchievements[type];
	}

	public void AddCompletedAchievement(string achievementId)
	{
		IAchievement achievement = DataCenter.Instance.Achievements.Find(achievementId);
		if (ContainscompletedAchievementType(achievement.Type))
		{
			List<IAchievement> list = DataCenter.Instance.Achievements.FindByType(achievement.Type);
			if (list.IndexOf(DataCenter.Instance.Achievements.Find(GetCompletedAchievementIdByType(achievement.Type))) < list.IndexOf(achievement))
			{
				data.completedAchievements[achievement.Type] = achievement.ID;
			}
		}
		else
		{
			data.completedAchievements.Add(achievement.Type, achievement.ID);
		}
	}

	public void AddMissionDeatilData(MissionDetail.Data data)
	{
		Data.LevelDetailData levelDetailData;
		if (!DetailData.LevelDetails.ContainsKey(data.level))
		{
			levelDetailData = new Data.LevelDetailData();
			levelDetailData.level = data.level;
			DetailData.LevelDetails.Add(data.level, levelDetailData);
		}
		else
		{
			levelDetailData = DetailData.LevelDetails[data.level];
		}
		if (levelDetailData.missions == null)
		{
			levelDetailData.missions = new List<MissionDetail.Data>();
		}
		levelDetailData.missions.Add(data);
		isDirty = true;
	}

	public void AddAneraMissionDeatilData(ArenaMissionDetail.Data data)
	{
		this.data.aneraDetail.Add(data);
		isDirty = true;
	}

	public void Load()
	{
		if (File.Exists(ZombieStreetCommon.SavePath + "/Player.data"))
		{
			SetReadWriteModel();
			data = deadWriteModel.Deserialize<Data>();
		}
		else if (data == null)
		{
			CreateNewData();
		}
		long ticks = DateTime.Now.Ticks;
		IsActivePlayer = IsActive(LastLoginTime, ticks);
		LastLoginTime = ticks;
		ProduceData();
		bool flag = false;
		if (!ZombieStreetConstants.version.Equals(Version))
		{
			SpecialIAPDataRepository specialIAPs = DataCenter.Instance.SpecialIAPs;
			if (_003C_003Ef__am_0024cacheD == null)
			{
				_003C_003Ef__am_0024cacheD = _003CLoad_003Em__C;
			}
			List<SpecailIAPData> list = specialIAPs.FindAll(_003C_003Ef__am_0024cacheD);
			if (list != null && list.Count > 0)
			{
				foreach (SpecailIAPData item in list)
				{
					flag = !data.iaps.ContainsKey(item.id) || data.iaps[item.id] == 0;
				}
			}
			Version = ZombieStreetConstants.version;
			IsNewVersion = true;
		}
		else
		{
			IsNewVersion = false;
		}
		if (IsNewVersion && !data.hasUpdateExp)
		{
			ExpUpdate(1.4f);
			data.hasUpdateExp = true;
		}
		if (flag)
		{
			ActiveSpecialIAPShowed = false;
		}
	}

	public void Reset()
	{
		double crystal = Crystal;
		CreateNewData();
		data.crystal = crystal;
		ProduceData();
		data.detail.LevelDetails.Clear();
		data.completedAchievements.Clear();
	}

	public void Save(bool force = false)
	{
		if (force || isDirty)
		{
			if (!Directory.Exists(ZombieStreetCommon.SavePath))
			{
				Directory.CreateDirectory(ZombieStreetCommon.SavePath);
			}
			UnionNewUnlockedGuns();
			SetReadWriteModel();
			deadWriteModel.Serialize(data);
			isDirty = false;
		}
	}

	public Data.HeroData FindHero(int id)
	{
		_003CFindHero_003Ec__AnonStorey21 _003CFindHero_003Ec__AnonStorey = new _003CFindHero_003Ec__AnonStorey21();
		_003CFindHero_003Ec__AnonStorey.id = id;
		if (data.heros == null)
		{
			data.heros = new List<Data.HeroData>();
		}
		return data.heros.Find(_003CFindHero_003Ec__AnonStorey._003C_003Em__D);
	}

	public int GetBoughtIapCount(string iapId)
	{
		int result = 0;
		if (data.iaps != null && data.iaps.ContainsKey(iapId))
		{
			result = data.iaps[iapId];
		}
		return result;
	}

	public List<Data.HeroData> GetHeros()
	{
		return data.heros;
	}

	private void SetReadWriteModel()
	{
		if (deadWriteModel == null)
		{
			deadWriteModel = new DataReadWriteModel(XmlDataReadWrite.Instance, ZombieStreetCommon.SavePath + "/Player.data", true);
		}
	}

	private Data ReadDefault()
	{
		Data result = null;
		DataReadWriteModel dataReadWriteModel = new DataReadWriteModel(XmlDataReadWrite.Instance, "Data/default", false);
		try
		{
			result = dataReadWriteModel.Deserialize<Data>();
			return result;
		}
		catch
		{
			return result;
		}
	}

	private void CreateNewData()
	{
		data = new Data();
		data.heros = new List<Data.HeroData>();
		data.currentHeroID = 0;
		Data.HeroData heroData = new Data.HeroData();
		heroData.id = 1;
		HeroData heroData2 = DataCenter.Instance.Heros.Find(heroData.id);
		heroData.meleeWeapon = heroData2.meleeWeaponWithBirth;
		data.guns = new List<string>();
		data.guns.AddRange(heroData2.gunsWithBirth);
		data.items = new List<string>();
		data.gameLevel = 1;
		data.heroLevel = 1;
		data.exp = 0.0;
		data.gold = 0.0;
		data.crystal = 0.0;
		data.items = new List<string>();
		data.ownItems = new SerializableDictionary<string, int>();
		data.ownItems.Add("Item004", 1);
		data.ownItems.Add("Item006", 1);
		AddHero(heroData);
		AddItem2Hero(1, 0, "Item004");
		AddItem2Hero(1, 1, "Item006");
		data.hasUpdateExp = true;
	}

	private void ProduceData()
	{
		if (data.heros == null)
		{
			data.heros = new List<Data.HeroData>();
		}
		if (data.currentHeroID <= 0)
		{
			data.currentHeroID = data.heros[0].id;
		}
		currentHero = FindHero(data.currentHeroID);
		if (data.ownGuns == null)
		{
			data.ownGuns = new SerializableDictionary<string, string>();
		}
		if (data.ownMeleeWeapons == null)
		{
			data.ownMeleeWeapons = new SerializableDictionary<string, string>();
		}
		if (data.completedAchievements == null)
		{
			data.completedAchievements = new SerializableDictionary<int, string>();
		}
		if (data.detail == null)
		{
			data.detail = new Data.DetailData();
			data.detail.LevelDetails = new SerializableDictionary<int, Data.LevelDetailData>();
		}
		if (data.iaps == null)
		{
			data.iaps = new SerializableDictionary<string, int>();
		}
		if (data.ownItems == null)
		{
			data.ownItems = new SerializableDictionary<string, int>();
		}
		if (data.items == null)
		{
			data.items = new List<string>();
		}
		if (data.guns == null)
		{
			data.guns = new List<string>();
		}
		if (data.aneraDetail == null)
		{
			data.aneraDetail = new List<ArenaMissionDetail.Data>();
		}
		if (data.unlockedGuns == null)
		{
			data.unlockedGuns = new HashSet<string>();
		}
		InitUnlockedGuns();
		if (data.friends == null)
		{
			data.friends = new List<GameCenterModel.FriendScore>();
		}
		if (data.heroLevel > 101)
		{
			data.heroLevel = 101;
		}
	}

	public void TempData()
	{
		tempData = new Data();
		tempData.crystal = data.crystal;
		tempData.gold = data.gold;
		tempData.heroLevel = data.heroLevel;
		tempData.gameLevel = data.gameLevel;
		tempData.exp = data.exp;
		tempData.ownItems = new SerializableDictionary<string, int>();
		foreach (KeyValuePair<string, int> ownItem in data.ownItems)
		{
			tempData.ownItems.Add(ownItem.Key, ownItem.Value);
		}
	}

	private void AddIAPCount(string iapId, int count)
	{
		if (data.iaps.ContainsKey(iapId))
		{
			SerializableDictionary<string, int> iaps;
			SerializableDictionary<string, int> serializableDictionary = (iaps = data.iaps);
			string key;
			string key2 = (key = iapId);
			int num = iaps[key];
			serializableDictionary[key2] = num + count;
		}
		else
		{
			data.iaps.Add(iapId, count);
		}
		isDirty = true;
	}

	public void RollbackData()
	{
		data.crystal = tempData.crystal;
		data.gold = tempData.gold;
		data.heroLevel = tempData.heroLevel;
		data.gameLevel = tempData.gameLevel;
		data.exp = tempData.exp;
		data.ownItems.Clear();
		foreach (KeyValuePair<string, int> ownItem in tempData.ownItems)
		{
			data.ownItems.Add(ownItem.Key, ownItem.Value);
		}
	}

	public void AddMission(int pointId, string missionId)
	{
		if (Missions.ContainsKey(pointId))
		{
			Missions[pointId] = missionId;
		}
		else
		{
			Missions.Add(pointId, missionId);
		}
		isDirty = true;
	}

	public void RemoveMission(int pointId)
	{
		if (Missions.ContainsKey(pointId))
		{
			Missions.Remove(pointId);
			NeedRefreshMission = true;
			isDirty = true;
		}
	}

	public void ClearMission()
	{
		if (Missions.Count > 0)
		{
			Missions.Clear();
			NeedRefreshMission = true;
			isDirty = true;
		}
	}

	private static bool IsActive(long lastLoginTime, long currentLoginTime)
	{
		DateTime dateTime = new DateTime(lastLoginTime);
		DateTime dateTime2 = new DateTime(currentLoginTime);
		return (dateTime2 - dateTime.Date).Days == 1;
	}

	public void UpdateActive()
	{
		DateTime now = DateTime.Now;
		if (todayTicks > 0)
		{
			DateTime date = new DateTime(todayTicks).Date;
			if ((now - date).Days >= 1)
			{
				todayIsActive = false;
			}
		}
		if (!todayIsActive)
		{
			IsActivePlayer = IsActive(LastLoginTime, now.Ticks);
			LastLoginTime = now.Ticks;
			if (IsActivePlayer)
			{
				todayIsActive = true;
				todayTicks = now.Date.Ticks;
			}
		}
	}

	private void InitUnlockedGuns()
	{
		if (data.unlockedGuns.Count != 0)
		{
			return;
		}
		List<GunData> list = DataCenter.Instance.Guns.FindAll(_003CInitUnlockedGuns_003Em__E);
		if (list == null || list.Count <= 0)
		{
			return;
		}
		foreach (GunData item in list)
		{
			data.unlockedGuns.Add(item.typeName);
		}
		isDirty = true;
	}

	private HashSet<string> CalculateUnlockedGuns()
	{
		HashSet<string> hashSet = new HashSet<string>();
		List<GunData> list = DataCenter.Instance.Guns.FindAll(_003CCalculateUnlockedGuns_003Em__F);
		if (list != null && list.Count > 0)
		{
			foreach (GunData item in list)
			{
				hashSet.Add(item.typeName);
			}
			return hashSet;
		}
		return hashSet;
	}

	public void CalculateNewUnlockedGuns()
	{
		newUnlocked.UnionWith(CalculateUnlockedGuns());
		newUnlocked.ExceptWith(data.unlockedGuns);
	}

	public void UnionNewUnlockedGuns()
	{
		data.unlockedGuns.UnionWith(newUnlocked);
		isDirty = newUnlocked.Count > 0;
		newUnlocked.Clear();
	}

	private void ExpUpdate(float rate)
	{
		double num = data.exp;
		for (int i = 1; i < data.heroLevel; i++)
		{
			HeroLevelUpExpData heroLevelUpExpData = DataCenter.Instance.HeroLevelUpDatas.Find(i);
			num += (double)heroLevelUpExpData.exp;
		}
		num *= (double)rate;
		num = (int)num;
		data.heroLevel = 1;
		data.exp = 0.0;
		AddExp(num);
	}

	[CompilerGenerated]
	private static bool _003CLoad_003Em__C(SpecailIAPData data)
	{
		return data.notifyDay == ZombieStreetConstants.specailIAPDataNotifyDay;
	}

	[CompilerGenerated]
	private bool _003CInitUnlockedGuns_003Em__E(GunData gun)
	{
		return gun.unLockLevel <= data.heroLevel;
	}

	[CompilerGenerated]
	private bool _003CCalculateUnlockedGuns_003Em__F(GunData gun)
	{
		return gun.unLockLevel <= data.heroLevel;
	}
}
