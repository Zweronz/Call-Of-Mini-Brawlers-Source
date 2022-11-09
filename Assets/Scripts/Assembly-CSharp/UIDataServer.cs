using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Event;
using UnityEngine;

public class UIDataServer
{
	[CompilerGenerated]
	private sealed class _003CCreateAllGunInfo_003Ec__AnonStorey2A
	{
		internal GunData gun;

		internal bool _003C_003Em__46(GunData data)
		{
			return data.typeName.Equals(gun.typeName);
		}
	}

	[CompilerGenerated]
	private sealed class _003CCreateAllMeleeWeaponInfo_003Ec__AnonStorey2B
	{
		internal MeleeWeaponData meleeWeapon;

		internal bool _003C_003Em__47(MeleeWeaponData data)
		{
			return data.typeName.Equals(meleeWeapon.typeName);
		}
	}

	[CompilerGenerated]
	private sealed class _003CHandleZS_PublishSpecialIAPEvent_003Ec__AnonStorey2C
	{
		internal ZS_PublishSpecialIAPEvent evt;

		internal bool _003C_003Em__4D(SpecailIAPData data)
		{
			return data.notifyDay == evt.NotifyDay;
		}
	}

	private static UIDataServer instance;

	private readonly string linkString = "_&_";

	private readonly string isGun = "__GUN__";

	private readonly string isMeleeWeapon = "__MeleeWeapon__";

	private ZombieStreetTimer.TimerData timerData;

	private IAPData iapData;

	[CompilerGenerated]
	private static Predicate<GunData> _003C_003Ef__am_0024cache6;

	[CompilerGenerated]
	private static Predicate<HeroData> _003C_003Ef__am_0024cache7;

	[CompilerGenerated]
	private static Predicate<IItem> _003C_003Ef__am_0024cache8;

	[CompilerGenerated]
	private static Predicate<IItem> _003C_003Ef__am_0024cache9;

	[CompilerGenerated]
	private static Predicate<IItem> _003C_003Ef__am_0024cacheA;

	[CompilerGenerated]
	private static Predicate<IAPData> _003C_003Ef__am_0024cacheB;

	[CompilerGenerated]
	private static Predicate<Crystal2Gold> _003C_003Ef__am_0024cacheC;

	[CompilerGenerated]
	private static Predicate<IAchievement> _003C_003Ef__am_0024cacheD;

	[CompilerGenerated]
	private static Predicate<IAPData> _003C_003Ef__am_0024cacheE;

	public static UIDataServer Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new UIDataServer();
			}
			return instance;
		}
	}

	public static string ReviewURL
	{
		get
		{
			return "market://details?id=com.trinitigame.android.combrawlers";
		}
	}

	public void Initialize()
	{
		RegisterUIDataEventHandler();
	}

	private void RegisterUIDataEventHandler()
	{
		EventCenter.Instance.Register<ZS_PublishAllAvatarPhotoInfoEvent>(HandleZS_PublishAllAvatarPhotoInfoEvent);
		EventCenter.Instance.Register<ZS_PublishCurrentAvatarEvent>(HandleZS_PublishCurrentAvatarEvent);
		EventCenter.Instance.Register<ZS_PublishAllEquipEvent>(HandleZS_PublishAllEquipEvent);
		EventCenter.Instance.Register<ZS_PublishAllGunEvent>(HandleZS_PublishAllGunEvent);
		EventCenter.Instance.Register<ZS_PublishAllSwardEvent>(HandleZS_PublishAllSwardEvent);
		EventCenter.Instance.Register<ZS_PublishUsingEquipEvent>(HandleZS_PublishUsingEquipEvent);
		EventCenter.Instance.Register<ZS_PublishRongyuEvent>(HandleZS_PublishRongyuEvent);
		EventCenter.Instance.Register<ZS_PublishMissRewardEvent>(HandleZS_PublishMissRewardEvent);
		EventCenter.Instance.Register<ZS_PublishAllItemEvent>(HandleZS_PublishAllItemEvent);
		EventCenter.Instance.Register<ZS_PublishUsingItemEvent>(HandleZS_PublishUsingItemEvent);
		EventCenter.Instance.Register<ZS_PublishIAPEvent>(HandleZS_PublishIAPEvent);
		EventCenter.Instance.Register<ZS_PublishOptionInfoEvent>(HandleZS_PublishOptionInfoEvent);
		EventCenter.Instance.Register<ZS_PublishResetDataEvent>(HandleZS_PublishResetDataEvent);
		EventCenter.Instance.Register<ZS_PublishIAPGoldEvent>(HandleZS_PublishIAPGoldEvent);
		EventCenter.Instance.Register<ZS_PublishEquipEvent>(HandleZS_PublishEquipEvent);
		EventCenter.Instance.Register<ZS_PublishSpecialIAPEvent>(HandleZS_PublishSpecialIAPEvent);
		EventCenter.Instance.Register<GetReviveIAPEvent>(HandleGetReviveIAPEvent);
		InitializeIAB();
	}

	private ZS_AvatarPhotoInfo CreateZS_AvatarPhotoInfoByHeroId(int heroId)
	{
		ZS_AvatarPhotoInfo zS_AvatarPhotoInfo = new ZS_AvatarPhotoInfo();
		Player.Data.HeroData heroData = Player.Instance.FindHero(heroId);
		HeroData heroData2 = DataCenter.Instance.Heros.Find(heroId);
		if (heroData != null)
		{
			zS_AvatarPhotoInfo.level = Player.Instance.HeroLevel;
			zS_AvatarPhotoInfo.hp = (int)(heroData2.hp + heroData2.hpIncrease * (float)(Player.Instance.HeroLevel - 1));
		}
		zS_AvatarPhotoInfo.isLock = heroData2.unlock == HeroData.UnLockType.level && null == heroData;
		zS_AvatarPhotoInfo.isCanBuy = heroData2.unlock == HeroData.UnLockType.crystal || heroData2.unlock == HeroData.UnLockType.gold;
		zS_AvatarPhotoInfo.model = heroData2.modelName;
		zS_AvatarPhotoInfo.name = heroData2.nameId;
		zS_AvatarPhotoInfo.image = heroData2.icon;
		zS_AvatarPhotoInfo.id = heroData2.id.ToString();
		string empty = string.Empty;
		empty = ((heroData == null) ? heroData2.meleeWeaponWithBirth : heroData.meleeWeapon);
		zS_AvatarPhotoInfo.MeleeWeapon = DataCenter.Instance.MeleeWeapons.Find(empty).modelName;
		zS_AvatarPhotoInfo.specialId = heroData2.specialId;
		ZS_Money zS_Money = new ZS_Money(0.0, 0.0);
		switch (heroData2.unlock)
		{
		case HeroData.UnLockType.crystal:
			zS_Money.Tcystal = (int)heroData2.price;
			break;
		case HeroData.UnLockType.gold:
			zS_Money.Gold = (int)heroData2.price;
			break;
		}
		zS_AvatarPhotoInfo.money = zS_Money;
		zS_AvatarPhotoInfo.BuyAvatarCallBack = BuyAvatarDelegate;
		zS_AvatarPhotoInfo.UseAvatarCallBack = ChooseAvatarDelegate;
		zS_AvatarPhotoInfo.data = heroData2.id;
		return zS_AvatarPhotoInfo;
	}

	private ZS_EquipmentInfo GunData2ZS_EquipmentInfo(GunData data)
	{
		ZS_EquipmentInfo zS_EquipmentInfo = new ZS_EquipmentInfo();
		zS_EquipmentInfo.Data = data.id + linkString + isGun;
		zS_EquipmentInfo.Image = data.icon;
		zS_EquipmentInfo.Ammo = data.maxOfBullets;
		zS_EquipmentInfo.Attack = (int)data.damage;
		zS_EquipmentInfo.BuyMoney = new ZS_Money(data.gold, data.crystal);
		zS_EquipmentInfo.CanUpdate = !string.IsNullOrEmpty(data.nextId);
		zS_EquipmentInfo.CriticalChance = data.criticalHitRate * 100f;
		zS_EquipmentInfo.CriticalHit = data.criticalHitDamage * 100f;
		zS_EquipmentInfo.EquipCondition = data.requisiteOfLevel.ToString();
		zS_EquipmentInfo.BuyCondition = data.unLockLevel.ToString();
		zS_EquipmentInfo.Id = data.id;
		zS_EquipmentInfo.Name = data.name;
		if (data.crystal > 0)
		{
			zS_EquipmentInfo.IsCanBuy = true;
		}
		else
		{
			zS_EquipmentInfo.IsCanBuy = data.unLockLevel <= Player.Instance.HeroLevel;
		}
		zS_EquipmentInfo.KnockBack = data.fend;
		if (string.IsNullOrEmpty(data.nextId))
		{
			zS_EquipmentInfo.CanUpdate = false;
			zS_EquipmentInfo.UpdateMoney = new ZS_Money(0.0, 0.0);
		}
		else
		{
			zS_EquipmentInfo.CanUpdate = true;
			GunData gunData = DataCenter.Instance.Guns.Find(data.nextId);
			zS_EquipmentInfo.UpdateMoney = new ZS_Money(gunData.gold, gunData.crystal);
		}
		zS_EquipmentInfo.IsCanEquip = data.requisiteOfLevel <= Player.Instance.HeroLevel;
		zS_EquipmentInfo.Type = 1;
		zS_EquipmentInfo.Model = data.modelName;
		zS_EquipmentInfo.isMaxLevel = string.IsNullOrEmpty(data.nextId);
		zS_EquipmentInfo.Group = data.typeName;
		zS_EquipmentInfo.IsEquiped = Player.Instance.Guns.Contains(data.id);
		zS_EquipmentInfo.IsOwn = Player.Instance.ContainsGunType(data.typeName);
		List<GunData> list = DataCenter.Instance.Guns.FindByTypeName(data.typeName);
		int num = (zS_EquipmentInfo.level = list.IndexOf(data)) + 1;
		int index = 0;
		int num2 = list.Count - 1;
		if (num > num2)
		{
			num = num2;
		}
		zS_EquipmentInfo.NextAmmo = list[num].maxOfBullets;
		zS_EquipmentInfo.NextAttack = (int)list[num].damage;
		zS_EquipmentInfo.NextCriticalChance = list[num].criticalHitRate * 100f;
		zS_EquipmentInfo.NextCriticalHit = list[num].criticalHitDamage * 100f;
		zS_EquipmentInfo.NextKnockBack = list[num].fend;
		zS_EquipmentInfo.minAmmo = list[index].maxOfBullets;
		zS_EquipmentInfo.minAttack = (int)list[index].damage;
		zS_EquipmentInfo.minCriticalChance = list[index].criticalHitRate * 100f;
		zS_EquipmentInfo.minCriticalHit = list[index].criticalHitDamage * 100f;
		zS_EquipmentInfo.minKnockBack = list[index].fend;
		zS_EquipmentInfo.maxAmmo = list[num2].maxOfBullets;
		zS_EquipmentInfo.maxAttack = (int)list[num2].damage;
		zS_EquipmentInfo.maxCriticalChance = list[num2].criticalHitRate * 100f;
		zS_EquipmentInfo.maxCriticalHit = list[num2].criticalHitDamage * 100f;
		zS_EquipmentInfo.maxKnockBack = list[num2].fend;
		zS_EquipmentInfo.BuyCallBack = BuyWeaponDelegate;
		zS_EquipmentInfo.EquipCallBack = EquipWeaponDelegate;
		zS_EquipmentInfo.UnwieldCallBack = UnwieldWeaponDelegate;
		zS_EquipmentInfo.UpGradeCallBack = UpgradeWeaponDelegate;
		return zS_EquipmentInfo;
	}

	private ZS_EquipmentInfo MeleeWeaponData2ZS_EquipmentInfo(MeleeWeaponData data)
	{
		ZS_EquipmentInfo zS_EquipmentInfo = new ZS_EquipmentInfo();
		zS_EquipmentInfo.Data = data.id + linkString + isMeleeWeapon;
		zS_EquipmentInfo.Image = data.icon;
		zS_EquipmentInfo.Ammo = 1;
		zS_EquipmentInfo.Attack = (int)data.damage;
		zS_EquipmentInfo.BuyMoney = new ZS_Money(data.gold, data.crystal);
		zS_EquipmentInfo.CanUpdate = !string.IsNullOrEmpty(data.nextId);
		zS_EquipmentInfo.CriticalChance = data.criticalHitRate * 100f;
		zS_EquipmentInfo.CriticalHit = data.criticalHitDamage * 100f;
		zS_EquipmentInfo.EquipCondition = data.requisiteOfLevel.ToString();
		zS_EquipmentInfo.BuyCondition = data.unLockLevel.ToString();
		zS_EquipmentInfo.Id = data.id;
		zS_EquipmentInfo.Name = data.name;
		if (data.crystal > 0)
		{
			zS_EquipmentInfo.IsCanBuy = true;
		}
		else
		{
			zS_EquipmentInfo.IsCanBuy = data.unLockLevel <= Player.Instance.HeroLevel;
		}
		zS_EquipmentInfo.KnockBack = data.fend;
		if (string.IsNullOrEmpty(data.nextId))
		{
			zS_EquipmentInfo.CanUpdate = false;
			zS_EquipmentInfo.UpdateMoney = new ZS_Money(0.0, 0.0);
		}
		else
		{
			zS_EquipmentInfo.CanUpdate = true;
			MeleeWeaponData meleeWeaponData = DataCenter.Instance.MeleeWeapons.Find(data.nextId);
			zS_EquipmentInfo.UpdateMoney = new ZS_Money(meleeWeaponData.gold, meleeWeaponData.crystal);
		}
		zS_EquipmentInfo.Type = 0;
		zS_EquipmentInfo.Model = data.modelName;
		zS_EquipmentInfo.isMaxLevel = string.IsNullOrEmpty(data.nextId);
		zS_EquipmentInfo.Group = data.typeName;
		zS_EquipmentInfo.IsEquiped = Player.Instance.CurrentHero.meleeWeapon == data.id;
		zS_EquipmentInfo.IsOwn = Player.Instance.ContainsMeleeWeaponType(data.typeName);
		List<MeleeWeaponData> list = DataCenter.Instance.MeleeWeapons.FindByTypeName(data.typeName);
		int num = (zS_EquipmentInfo.level = list.IndexOf(data)) + 1;
		int index = 0;
		int num2 = list.Count - 1;
		if (num > num2)
		{
			num = num2;
		}
		zS_EquipmentInfo.NextAmmo = 1;
		zS_EquipmentInfo.NextAttack = (int)list[num].damage;
		zS_EquipmentInfo.NextCriticalChance = list[num].criticalHitRate * 100f;
		zS_EquipmentInfo.NextCriticalHit = list[num].criticalHitDamage * 100f;
		zS_EquipmentInfo.NextKnockBack = list[num].fend;
		zS_EquipmentInfo.minAmmo = 1;
		zS_EquipmentInfo.minAttack = (int)list[index].damage;
		zS_EquipmentInfo.minCriticalChance = list[index].criticalHitRate * 100f;
		zS_EquipmentInfo.minCriticalHit = list[index].criticalHitDamage * 100f;
		zS_EquipmentInfo.minKnockBack = list[index].fend;
		zS_EquipmentInfo.maxAmmo = 1;
		zS_EquipmentInfo.maxAttack = (int)list[num2].damage;
		zS_EquipmentInfo.maxCriticalChance = list[num2].criticalHitRate * 100f;
		zS_EquipmentInfo.maxCriticalHit = list[num2].criticalHitDamage * 100f;
		zS_EquipmentInfo.maxKnockBack = list[num2].fend;
		zS_EquipmentInfo.BuyCallBack = BuyWeaponDelegate;
		zS_EquipmentInfo.EquipCallBack = EquipWeaponDelegate;
		zS_EquipmentInfo.UnwieldCallBack = UnwieldWeaponDelegate;
		zS_EquipmentInfo.UpGradeCallBack = UpgradeWeaponDelegate;
		return zS_EquipmentInfo;
	}

	private ZS_IapInfo IAPData2ZS_IapInfo(IAPData data)
	{
		ZS_IapInfo zS_IapInfo = new ZS_IapInfo();
		zS_IapInfo.leftPackage = ((data.count != 0f) ? ((int)(data.count - (float)Player.Instance.GetBoughtIapCount(data.id))) : int.MaxValue);
		zS_IapInfo.data = data;
		zS_IapInfo.buyCallBack = IABBuyIAPDelegate;
		return zS_IapInfo;
	}

	private List<ZS_EquipmentInfo> CreateAllGunInfo()
	{
		List<ZS_EquipmentInfo> list = new List<ZS_EquipmentInfo>();
		List<GunData> list2 = new List<GunData>();
		GunRepository guns = DataCenter.Instance.Guns;
		if (_003C_003Ef__am_0024cache6 == null)
		{
			_003C_003Ef__am_0024cache6 = _003CCreateAllGunInfo_003Em__45;
		}
		list2.AddRange(guns.FindAll(_003C_003Ef__am_0024cache6));
		while (list2.Count > 0)
		{
			_003CCreateAllGunInfo_003Ec__AnonStorey2A _003CCreateAllGunInfo_003Ec__AnonStorey2A = new _003CCreateAllGunInfo_003Ec__AnonStorey2A();
			if (Player.Instance.ContainsGunType(list2[0].typeName))
			{
				_003CCreateAllGunInfo_003Ec__AnonStorey2A.gun = DataCenter.Instance.Guns.Find(Player.Instance.GetGunIdByType(list2[0].typeName));
			}
			else
			{
				_003CCreateAllGunInfo_003Ec__AnonStorey2A.gun = DataCenter.Instance.Guns.FindByTypeName(list2[0].typeName)[0];
			}
			if (_003CCreateAllGunInfo_003Ec__AnonStorey2A.gun.canAppear)
			{
				list.Add(GunData2ZS_EquipmentInfo(_003CCreateAllGunInfo_003Ec__AnonStorey2A.gun));
			}
			list2.RemoveAll(_003CCreateAllGunInfo_003Ec__AnonStorey2A._003C_003Em__46);
		}
		return list;
	}

	private List<ZS_EquipmentInfo> CreateAllMeleeWeaponInfo()
	{
		List<ZS_EquipmentInfo> list = new List<ZS_EquipmentInfo>();
		List<MeleeWeaponData> list2 = new List<MeleeWeaponData>();
		list2.Add(DataCenter.Instance.MeleeWeapons.Find(Player.Instance.CurrentHero.meleeWeapon));
		while (list2.Count > 0)
		{
			_003CCreateAllMeleeWeaponInfo_003Ec__AnonStorey2B _003CCreateAllMeleeWeaponInfo_003Ec__AnonStorey2B = new _003CCreateAllMeleeWeaponInfo_003Ec__AnonStorey2B();
			if (Player.Instance.ContainsMeleeWeaponType(list2[0].typeName))
			{
				_003CCreateAllMeleeWeaponInfo_003Ec__AnonStorey2B.meleeWeapon = DataCenter.Instance.MeleeWeapons.Find(Player.Instance.GetMeleeWeaponIdByType(list2[0].typeName));
			}
			else
			{
				_003CCreateAllMeleeWeaponInfo_003Ec__AnonStorey2B.meleeWeapon = DataCenter.Instance.MeleeWeapons.FindByTypeName(list2[0].typeName)[0];
			}
			list.Add(MeleeWeaponData2ZS_EquipmentInfo(_003CCreateAllMeleeWeaponInfo_003Ec__AnonStorey2B.meleeWeapon));
			list2.RemoveAll(_003CCreateAllMeleeWeaponInfo_003Ec__AnonStorey2B._003C_003Em__47);
		}
		return list;
	}

	private ZS_RongyuInfo Achievement2ZS_RongyuInfo(IAchievement achievement)
	{
		ZS_RongyuInfo zS_RongyuInfo = new ZS_RongyuInfo();
		zS_RongyuInfo.money = new ZS_Money(achievement.Gold, achievement.Crystal);
		if (!string.IsNullOrEmpty(achievement.ItemId))
		{
			zS_RongyuInfo.icon = DataCenter.Instance.Items.Find(achievement.ItemId).BaseData.icon + "_y";
		}
		zS_RongyuInfo.rewardCount = achievement.ItemCount;
		zS_RongyuInfo.isCompleted = achievement.State == AchievementState.Completed && string.IsNullOrEmpty(achievement.NextID);
		zS_RongyuInfo.compPercent = achievement.Progress;
		zS_RongyuInfo.callBack = CompleteAchievement;
		zS_RongyuInfo.message = achievement.Desc;
		zS_RongyuInfo.data = achievement;
		zS_RongyuInfo.id = achievement.Type.ToString();
		return zS_RongyuInfo;
	}

	private ZS_ItemInfo Item2ZS_ItemInfo(IItem item)
	{
		ZS_ItemInfo zS_ItemInfo = new ZS_ItemInfo();
		item.BaseData.gold = (float)DataCenter.Instance.ItemPrices.Find(item.BaseData.id, Player.Instance.HeroLevel).price;
		zS_ItemInfo.Id = item.BaseData.id;
		zS_ItemInfo.Image = item.BaseData.icon;
		zS_ItemInfo.Model = item.BaseData.icon;
		zS_ItemInfo.Money = new ZS_Money(item.BaseData.gold, item.BaseData.crystal);
		zS_ItemInfo.Name = item.BaseData.nameId;
		zS_ItemInfo.Desc = item.BaseData.descId;
		zS_ItemInfo.canBuy = item.BaseData.canAppear;
		zS_ItemInfo.data = item;
		int num = (zS_ItemInfo.Count = Player.Instance.GetItemCount(item.BaseData.id));
		zS_ItemInfo.IsOwn = num > 0;
		List<string> items = Player.Instance.Items;
		zS_ItemInfo.IsUsing = items != null && items.Contains(item.BaseData.id);
		zS_ItemInfo.BuyCallBack = BuyItemDelegate;
		zS_ItemInfo.EquipCallBack = EquipItemDelegate;
		zS_ItemInfo.UnwieldCallBack = UnwieldItemDelegate;
		return zS_ItemInfo;
	}

	private void HandleZS_PublishAllAvatarPhotoInfoEvent(object sender, ZS_PublishAllAvatarPhotoInfoEvent evt)
	{
		List<ZS_AvatarPhotoInfo> list = new List<ZS_AvatarPhotoInfo>();
		List<HeroData> list2 = new List<HeroData>();
		HeroDataRepository heros = DataCenter.Instance.Heros;
		if (_003C_003Ef__am_0024cache7 == null)
		{
			_003C_003Ef__am_0024cache7 = _003CHandleZS_PublishAllAvatarPhotoInfoEvent_003Em__48;
		}
		list2.AddRange(heros.FindAll(_003C_003Ef__am_0024cache7));
		foreach (HeroData item in list2)
		{
			list.Add(CreateZS_AvatarPhotoInfoByHeroId(item.id));
		}
		evt.AllAvatarInfoDel(list);
	}

	private void HandleZS_PublishCurrentAvatarEvent(object sender, ZS_PublishCurrentAvatarEvent evt)
	{
		ZS_AvatarInfo zS_AvatarInfo = new ZS_AvatarInfo();
		zS_AvatarInfo.CurrentAvatarPhoto = CreateZS_AvatarPhotoInfoByHeroId(Player.Instance.CurrentHero.id);
		zS_AvatarInfo.Experience = (float)CalculateExp();
		zS_AvatarInfo.Money = new ZS_Money((int)Player.Instance.Gold, (int)Player.Instance.Crystal);
		zS_AvatarInfo.IsAchieveComplete = HasAchievementCompeleted();
		zS_AvatarInfo.CanBuyOrUpdate = HasWeaponCanBuyOrUpdate();
		evt.CurrentAvatarDel(zS_AvatarInfo);
	}

	private void HandleZS_PublishAllEquipEvent(object sender, ZS_PublishAllEquipEvent evt)
	{
		List<ZS_EquipmentInfo> list = new List<ZS_EquipmentInfo>();
		list.AddRange(CreateAllGunInfo());
		list.AddRange(CreateAllMeleeWeaponInfo());
		evt.AllEquipDel(list.ToArray());
	}

	private void HandleZS_PublishAllGunEvent(object sender, ZS_PublishAllGunEvent evt)
	{
		evt.AllGuns(CreateAllGunInfo());
	}

	private void HandleZS_PublishAllSwardEvent(object sender, ZS_PublishAllSwardEvent evt)
	{
		evt.AllSwards(CreateAllMeleeWeaponInfo());
	}

	private void HandleZS_PublishUsingEquipEvent(object sender, ZS_PublishUsingEquipEvent evt)
	{
		List<ZS_EquipmentInfo> list = new List<ZS_EquipmentInfo>();
		list.Add(MeleeWeaponData2ZS_EquipmentInfo(DataCenter.Instance.MeleeWeapons.Find(Player.Instance.CurrentHero.meleeWeapon)));
		int num = evt.Count - 1;
		foreach (string gun in Player.Instance.Guns)
		{
			if (num <= 0)
			{
				break;
			}
			if (string.IsNullOrEmpty(gun))
			{
				list.Add(null);
			}
			else
			{
				list.Add(GunData2ZS_EquipmentInfo(DataCenter.Instance.Guns.Find(gun)));
			}
			num--;
		}
		while (num-- > 0)
		{
			list.Add(null);
		}
		evt.UsingEquipDel(list);
	}

	private void HandleZS_PublishRongyuEvent(object sender, ZS_PublishRongyuEvent evt)
	{
		AchievementTool.InitializeAllAchievements();
		AchievementTool.CalculateAchievements();
		List<ZS_RongyuInfo> list = new List<ZS_RongyuInfo>();
		List<ZS_RongyuInfo> list2 = new List<ZS_RongyuInfo>();
		List<ZS_RongyuInfo> list3 = new List<ZS_RongyuInfo>();
		List<IAchievement> all = DataCenter.Instance.Achievements.GetAll();
		foreach (IAchievement item in all)
		{
			if (item.State == AchievementState.Processing)
			{
				if (item.Progress == 1f)
				{
					list.Add(Achievement2ZS_RongyuInfo(item));
				}
				else
				{
					list2.Add(Achievement2ZS_RongyuInfo(item));
				}
			}
			else if (item.State == AchievementState.Completed && string.IsNullOrEmpty(item.NextID))
			{
				list3.Add(Achievement2ZS_RongyuInfo(item));
			}
		}
		list.AddRange(list2);
		list.AddRange(list3);
		evt.RongYuInfo(list);
	}

	private void HandleZS_PublishMissRewardEvent(object sender, ZS_PublishMissRewardEvent evt)
	{
		if (Player.Instance.LastMissionData == null)
		{
			return;
		}
		ZS_MissRewardInfo zS_MissRewardInfo = new ZS_MissRewardInfo();
		zS_MissRewardInfo.bonus = (int)Player.Instance.LastMissionData.bonus;
		zS_MissRewardInfo.exp = (int)Player.Instance.LastMissionData.getExp;
		zS_MissRewardInfo.money = new ZS_Money(Player.Instance.LastMissionData.getGold, 0.0);
		zS_MissRewardInfo.isVictory = Player.Instance.LastMissionData.isCompleted;
		int num = 0;
		foreach (int value in Player.Instance.LastMissionData.killedZombiesByZombieId.Values)
		{
			num += value;
		}
		zS_MissRewardInfo.killCount = num;
		zS_MissRewardInfo.currentLevel = Player.Instance.HeroLevel;
		zS_MissRewardInfo.levelUp = Player.Instance.LastMissionData.levelUp;
		zS_MissRewardInfo.preExperience = (float)CalculatePreExp(Player.Instance.LastMissionData.levelUp, Player.Instance.LastMissionData.getExp);
		zS_MissRewardInfo.currentExperience = (float)CalculateExp();
		zS_MissRewardInfo.avatarOwnMoney = new ZS_Money(Player.Instance.Gold, Player.Instance.Crystal);
		evt.MissRewardInfoDel(zS_MissRewardInfo);
	}

	private void HandleZS_PublishAllItemEvent(object sender, ZS_PublishAllItemEvent evt)
	{
		List<ZS_ItemInfo> list = new List<ZS_ItemInfo>();
		List<IItem> list2 = new List<IItem>();
		ItemDataRepository items = DataCenter.Instance.Items;
		if (_003C_003Ef__am_0024cache8 == null)
		{
			_003C_003Ef__am_0024cache8 = _003CHandleZS_PublishAllItemEvent_003Em__49;
		}
		list2.AddRange(items.FindAll(_003C_003Ef__am_0024cache8));
		if (_003C_003Ef__am_0024cache9 == null)
		{
			_003C_003Ef__am_0024cache9 = _003CHandleZS_PublishAllItemEvent_003Em__4A;
		}
		List<IItem> collection = list2.FindAll(_003C_003Ef__am_0024cache9);
		if (_003C_003Ef__am_0024cacheA == null)
		{
			_003C_003Ef__am_0024cacheA = _003CHandleZS_PublishAllItemEvent_003Em__4B;
		}
		list2.RemoveAll(_003C_003Ef__am_0024cacheA);
		list2.AddRange(collection);
		foreach (IItem item in list2)
		{
			if (item.BaseData.canAppear || Player.Instance.GetItemCount(item.BaseData.id) >= 1)
			{
				list.Add(Item2ZS_ItemInfo(item));
			}
		}
		evt.AllItemDel(list);
	}

	private void HandleZS_PublishUsingItemEvent(object sender, ZS_PublishUsingItemEvent evt)
	{
		List<ZS_ItemInfo> list = new List<ZS_ItemInfo>();
		int num = evt.Count;
		foreach (string item in Player.Instance.Items)
		{
			if (num <= 0)
			{
				break;
			}
			if (string.IsNullOrEmpty(item))
			{
				list.Add(null);
			}
			else if (!DataCenter.Instance.Items.Find(item).BaseData.canAppear && Player.Instance.GetItemCount(item) < 1)
			{
				Player.Instance.RemoveItem(item);
				list.Add(null);
			}
			else
			{
				list.Add(Item2ZS_ItemInfo(DataCenter.Instance.Items.Find(item)));
			}
			num--;
		}
		while (num-- > 0)
		{
			list.Add(null);
		}
		evt.UsingItemDel(list);
	}

	private void HandleZS_PublishIAPEvent(object sender, ZS_PublishIAPEvent evt)
	{
		List<ZS_IapInfo> list = new List<ZS_IapInfo>();
		IAPDataRepository iAPs = DataCenter.Instance.IAPs;
		if (_003C_003Ef__am_0024cacheB == null)
		{
			_003C_003Ef__am_0024cacheB = _003CHandleZS_PublishIAPEvent_003Em__4C;
		}
		List<IAPData> list2 = iAPs.FindAll(_003C_003Ef__am_0024cacheB);
		foreach (IAPData item in list2)
		{
			list.Add(IAPData2ZS_IapInfo(item));
		}
		evt.allIAPInfo(list);
	}

	private void HandleZS_PublishSpecialIAPEvent(object sender, ZS_PublishSpecialIAPEvent evt)
	{
		_003CHandleZS_PublishSpecialIAPEvent_003Ec__AnonStorey2C _003CHandleZS_PublishSpecialIAPEvent_003Ec__AnonStorey2C = new _003CHandleZS_PublishSpecialIAPEvent_003Ec__AnonStorey2C();
		_003CHandleZS_PublishSpecialIAPEvent_003Ec__AnonStorey2C.evt = evt;
		List<SpecailIAPData> list = DataCenter.Instance.SpecialIAPs.FindAll(_003CHandleZS_PublishSpecialIAPEvent_003Ec__AnonStorey2C._003C_003Em__4D);
		if (list != null && list.Count > 0)
		{
			_003CHandleZS_PublishSpecialIAPEvent_003Ec__AnonStorey2C.evt.SpecialIAPInfo(IAPData2ZS_IapInfo(list[0]));
		}
	}

	private void HandleZS_PublishIAPGoldEvent(object sender, ZS_PublishIAPGoldEvent evt)
	{
		Crystal2GoldDataRepository crystal2GoldData = DataCenter.Instance.Crystal2GoldData;
		if (_003C_003Ef__am_0024cacheC == null)
		{
			_003C_003Ef__am_0024cacheC = _003CHandleZS_PublishIAPGoldEvent_003Em__4E;
		}
		List<Crystal2Gold> list = crystal2GoldData.FindAll(_003C_003Ef__am_0024cacheC);
		List<ZS_IapGoldInfo> list2 = new List<ZS_IapGoldInfo>();
		foreach (Crystal2Gold item in list)
		{
			ZS_IapGoldInfo zS_IapGoldInfo = new ZS_IapGoldInfo();
			zS_IapGoldInfo.data = item;
			zS_IapGoldInfo.crystal = item.crystal;
			zS_IapGoldInfo.count = item.UpgradeByHeroLevel(Player.Instance.HeroLevel);
			zS_IapGoldInfo.buyCallBack = BuyC2GDelegate;
			list2.Add(zS_IapGoldInfo);
		}
		evt.allIAPGoldInfo(list2);
	}

	private void HandleZS_PublishOptionInfoEvent(object sender, ZS_PublishOptionInfoEvent evt)
	{
		evt.optionInfo.isMusicOn = TAudioManager.instance.isMusicOn;
		evt.optionInfo.isSoundOn = TAudioManager.instance.isSoundOn;
		evt.optionInfo.OptionCallBack = SetSoundDelegate;
	}

	private void HandleZS_PublishResetDataEvent(object sender, ZS_PublishResetDataEvent evt)
	{
		ResetPlayerDelegate();
	}

	private void HandleZS_PublishEquipEvent(object sender, ZS_PublishEquipEvent evt)
	{
		if (!Player.Instance.ShowedReview && Player.Instance.GameLevel % 10 == 0 && Player.Instance.GameLevel / 10 > Player.Instance.ShowedReviewCount)
		{
			ShowMessageBox();
			Player.Instance.ShowedReviewCount = Player.Instance.GameLevel / 10;
		}
		Player.Instance.CalculateNewUnlockedGuns();
		List<string> list = new List<string>();
		foreach (string newUnlockedGun in Player.Instance.NewUnlockedGuns)
		{
			list.Add(DataCenter.Instance.Guns.FindByTypeName(newUnlockedGun)[0].icon);
		}
		EventCenter.Instance.Publish(null, new ShowNewWeaponUnlockEvent(list));
		Player.Instance.UnionNewUnlockedGuns();
	}

	private void HandleGetReviveIAPEvent(object sender, GetReviveIAPEvent evt)
	{
		ReviveIAPData reviveIAPData = new ReviveIAPData();
		reviveIAPData.iapEvent = IAPData2ZS_IapInfo(DataCenter.Instance.IAPs.Find(evt.iapId));
		reviveIAPData.reviveAction = RevivePlayer;
		evt.action(reviveIAPData);
	}

	private int BuyAvatarDelegate(ZS_AvatarPhotoInfo info)
	{
		int result = 3;
		int id = (int)info.data;
		HeroData heroData = DataCenter.Instance.Heros.Find(id);
		if (Player.Instance.FindHero(id) == null)
		{
			bool flag = false;
			switch (heroData.unlock)
			{
			case HeroData.UnLockType.gold:
				flag = PayByGold(heroData.price);
				if (!flag)
				{
					result = 1;
				}
				break;
			case HeroData.UnLockType.crystal:
				flag = PayByCrystal(heroData.price);
				if (!flag)
				{
					result = 2;
				}
				break;
			}
			if (flag)
			{
				Player.Data.HeroData heroData2 = new Player.Data.HeroData();
				heroData2.id = id;
				heroData2.meleeWeapon = heroData.meleeWeaponWithBirth;
				Player.Instance.AddHero(heroData2);
				MyFlurry.BuyHero(heroData2.id.ToString());
				result = 0;
			}
		}
		return result;
	}

	private int ChooseAvatarDelegate(ZS_AvatarPhotoInfo info)
	{
		int num = (int)info.data;
		if (Player.Instance.CurrentHero.id != num)
		{
			Player.Instance.SetCurrentHero(num);
			return 0;
		}
		return 3;
	}

	private int BuyWeaponDelegate(ZS_EquipmentInfo info)
	{
		int result = 3;
		string[] array = ((string)info.Data).Split(new string[1] { linkString }, StringSplitOptions.RemoveEmptyEntries);
		if (isGun.Equals(array[1]))
		{
			GunData gunData = DataCenter.Instance.Guns.Find(array[0]);
			bool flag = false;
			if (gunData.gold > 0)
			{
				flag = PayByGold(gunData.gold);
				if (!flag)
				{
					result = 1;
				}
			}
			else if (gunData.crystal > 0)
			{
				flag = PayByCrystal(gunData.crystal);
				if (!flag)
				{
					result = 2;
				}
			}
			if (flag)
			{
				MyFlurry.BuyWeapon(gunData.typeName);
				Player.Instance.AddGun(gunData.id);
				result = 0;
			}
		}
		else if (isMeleeWeapon.Equals(array[1]))
		{
			MeleeWeaponData meleeWeaponData = DataCenter.Instance.MeleeWeapons.Find(array[0]);
			bool flag2 = false;
			if (meleeWeaponData.gold > 0)
			{
				flag2 = PayByGold(meleeWeaponData.gold);
				if (!flag2)
				{
					result = 1;
				}
			}
			else if (meleeWeaponData.crystal > 0)
			{
				flag2 = PayByCrystal(meleeWeaponData.crystal);
				if (!flag2)
				{
					result = 2;
				}
			}
			if (flag2)
			{
				MyFlurry.BuyWeapon(meleeWeaponData.typeName);
				Player.Instance.AddMeleeWeapon(meleeWeaponData.id);
				result = 0;
			}
		}
		return result;
	}

	private int EquipWeaponDelegate(int slot, ZS_EquipmentInfo info)
	{
		int result = 3;
		if (slot >= 0)
		{
			string[] array = ((string)info.Data).Split(new string[1] { linkString }, StringSplitOptions.RemoveEmptyEntries);
			if (isGun.Equals(array[1]) && slot > 0)
			{
				GunData gunData = DataCenter.Instance.Guns.Find(array[0]);
				if (gunData.requisiteOfLevel <= Player.Instance.HeroLevel)
				{
					Player.Instance.AddGun2Hero(Player.Instance.CurrentHero.id, slot - 1, gunData.id);
					result = 0;
				}
			}
			else if (isMeleeWeapon.Equals(array[1]) && slot == 0)
			{
				MeleeWeaponData meleeWeaponData = DataCenter.Instance.MeleeWeapons.Find(array[0]);
				if (meleeWeaponData.requisiteOfLevel <= Player.Instance.HeroLevel)
				{
					Player.Instance.AddMeleeWeapon2Hero(Player.Instance.CurrentHero.id, meleeWeaponData.id);
					result = 0;
				}
			}
		}
		return result;
	}

	private int UnwieldWeaponDelegate(int slot, ZS_EquipmentInfo info)
	{
		int result = 0;
		if (slot > 1 && slot <= Player.Instance.Guns.Count)
		{
			Player.Instance.AddGun2Hero(Player.Instance.CurrentHero.id, slot - 1, string.Empty);
		}
		return result;
	}

	private int UpgradeWeaponDelegate(ZS_EquipmentInfo info)
	{
		int result = 3;
		string[] array = ((string)info.Data).Split(new string[1] { linkString }, StringSplitOptions.RemoveEmptyEntries);
		if (isGun.Equals(array[1]))
		{
			GunData gunData = DataCenter.Instance.Guns.Find(array[0]);
			if (string.IsNullOrEmpty(gunData.nextId))
			{
				return 3;
			}
			GunData gunData2 = DataCenter.Instance.Guns.Find(gunData.nextId);
			bool flag = false;
			if (gunData2.gold > 0)
			{
				flag = PayByGold(gunData2.gold);
				if (!flag)
				{
					result = 1;
				}
			}
			else if (gunData2.crystal > 0)
			{
				flag = PayByCrystal(gunData2.crystal);
				if (!flag)
				{
					result = 2;
				}
			}
			if (flag)
			{
				MyFlurry.UpgradeWeapon(gunData.typeName, DataCenter.Instance.Guns.FindByTypeName(gunData.typeName).IndexOf(gunData) + 1, Player.Instance.HeroLevel, Player.Instance.GameLevel - 1);
				Player.Instance.UpdateGun(gunData.id, gunData2.id);
				result = 0;
			}
		}
		else if (isMeleeWeapon.Equals(array[1]))
		{
			MeleeWeaponData meleeWeaponData = DataCenter.Instance.MeleeWeapons.Find(array[0]);
			if (string.IsNullOrEmpty(meleeWeaponData.nextId))
			{
				return 3;
			}
			MeleeWeaponData meleeWeaponData2 = DataCenter.Instance.MeleeWeapons.Find(meleeWeaponData.nextId);
			bool flag2 = false;
			if (meleeWeaponData2.gold > 0)
			{
				flag2 = PayByGold(meleeWeaponData2.gold);
				if (!flag2)
				{
					result = 1;
				}
			}
			else if (meleeWeaponData2.crystal > 0)
			{
				flag2 = PayByCrystal(meleeWeaponData2.crystal);
				if (!flag2)
				{
					result = 2;
				}
			}
			if (flag2)
			{
				MyFlurry.UpgradeWeapon(meleeWeaponData.typeName, DataCenter.Instance.MeleeWeapons.FindByTypeName(meleeWeaponData.typeName).IndexOf(meleeWeaponData) + 1, Player.Instance.HeroLevel, Player.Instance.GameLevel - 1);
				Player.Instance.UpdateMeleeWeapon(meleeWeaponData.id, meleeWeaponData2.id);
				result = 0;
			}
		}
		return result;
	}

	private ZS_RongyuInfo CompleteAchievement(ZS_RongyuInfo info)
	{
		IAchievement achievement = info.data as IAchievement;
		achievement.GetReward();
		Player.Instance.AddCompletedAchievement(achievement.ID);
		MyFlurry.AchievementComplete(achievement.ID);
		IAchievement achievement2 = ((!string.IsNullOrEmpty(achievement.NextID)) ? DataCenter.Instance.Achievements.Find(achievement.NextID) : achievement);
		achievement2.Initialize();
		achievement2.Process();
		return Achievement2ZS_RongyuInfo(achievement2);
	}

	private int BuyItemDelegate(ZS_ItemInfo info)
	{
		int result = 3;
		IItem item = info.data as IItem;
		if (item != null)
		{
			bool flag = false;
			if (item.BaseData.gold > 0f)
			{
				flag = PayByGold(item.BaseData.gold);
				if (!flag)
				{
					result = 1;
				}
			}
			else
			{
				flag = PayByCrystal(item.BaseData.crystal);
				if (!flag)
				{
					result = 2;
				}
			}
			if (flag)
			{
				MyFlurry.BuyItem(item.BaseData.id);
				Player.Instance.AddItem(item.BaseData.id, 1);
				result = 0;
			}
		}
		return result;
	}

	private int EquipItemDelegate(int slot, ZS_ItemInfo info)
	{
		int result = 3;
		if (slot >= 0)
		{
			IItem item = info.data as IItem;
			Player.Instance.AddItem2Hero(Player.Instance.CurrentHero.id, slot, item.BaseData.id);
			result = 0;
		}
		return result;
	}

	private int UnwieldItemDelegate(int slot, ZS_ItemInfo info)
	{
		int result = 0;
		if (slot >= 0 && slot <= Player.Instance.Items.Count)
		{
			Player.Instance.AddItem2Hero(Player.Instance.CurrentHero.id, slot, string.Empty);
		}
		return result;
	}

	private int BuyIAPDelegate(ZS_IapInfo info)
	{
		int result = 0;
		IAPData iAPData = info.data as IAPData;
		if (!string.IsNullOrEmpty(iAPData.id))
		{
			IAPPlugin.NowPurchaseProduct(iAPData.id, 1.ToString());
			timerData = new ZombieStreetTimer.TimerData();
			timerData.time = 0.1f;
			timerData.invokeTimes = -1;
			timerData.data = iAPData;
			timerData.ingoreTimeScale = true;
			timerData.handler = TestIAPStatus;
			ZombieStreetTimer.Instance.AddTimer(timerData);
		}
		else
		{
			result = 3;
		}
		return result;
	}

	private int AmazonBuyIAPDelegate(ZS_IapInfo info)
	{
		int result = 0;
		IAPData iAPData = info.data as IAPData;
		if (!string.IsNullOrEmpty(iAPData.id))
		{
			iapData = iAPData;
			AmazonIAP.initiatePurchaseRequest(iAPData.id);
		}
		else
		{
			result = 3;
		}
		return result;
	}

	private int IABBuyIAPDelegate(ZS_IapInfo info)
	{
		int result = 0;
		IAPData iAPData = info.data as IAPData;
		if (!string.IsNullOrEmpty(iAPData.id))
		{
			IABAndroid.purchaseProduct(iAPData.id);
		}
		else
		{
			result = 3;
		}
		return result;
	}

	private int BuyC2GDelegate(ZS_IapGoldInfo info)
	{
		int num = 3;
		Crystal2Gold crystal2Gold = info.data as Crystal2Gold;
		if (PayByCrystal(crystal2Gold.crystal))
		{
			Player.Instance.AddGold(crystal2Gold.UpgradeByHeroLevel(Player.Instance.HeroLevel));
			return 0;
		}
		return 2;
	}

	private void SetSoundDelegate(ZS_OptionInfo info)
	{
		TAudioManager.instance.isMusicOn = info.isMusicOn;
		TAudioManager.instance.isSoundOn = info.isSoundOn;
	}

	private void ResetPlayerDelegate()
	{
		Player.Instance.Reset();
		Player.Instance.Save(true);
		DataCenter.Instance.Achievements.InitializeAllAchievements();
		AchievementTool.ClearAchievements();
	}

	private bool RevivePlayer(int crytal)
	{
		bool flag = PayByCrystal(crytal);
		if (flag)
		{
			EventCenter.Instance.Publish(null, new HeroReviveEvent());
		}
		return flag;
	}

	private bool PayByGold(float price)
	{
		bool flag = Player.Instance.Gold >= (double)price;
		if (flag)
		{
			Player.Instance.AddGold(0f - price);
		}
		return flag;
	}

	private bool PayByCrystal(float price)
	{
		bool flag = Player.Instance.Crystal >= (double)price;
		if (flag)
		{
			Player.Instance.AddCrystal(0f - price);
		}
		return flag;
	}

	private bool HasAchievementCompeleted()
	{
		AchievementTool.InitializeAllAchievements();
		AchievementTool.CalculateAchievements();
		AchievementRepository achievements = DataCenter.Instance.Achievements;
		if (_003C_003Ef__am_0024cacheD == null)
		{
			_003C_003Ef__am_0024cacheD = _003CHasAchievementCompeleted_003Em__4F;
		}
		List<IAchievement> list = achievements.FindAll(_003C_003Ef__am_0024cacheD);
		if (list == null)
		{
			return false;
		}
		if (list.Count > 0)
		{
			return true;
		}
		return false;
	}

	private bool HasWeaponCanBuyOrUpdate()
	{
		List<ZS_EquipmentInfo> list = CreateAllGunInfo();
		foreach (ZS_EquipmentInfo item in list)
		{
			string[] array = (item.Data as string).Split(new string[1] { linkString }, StringSplitOptions.RemoveEmptyEntries);
			GunData gunData = DataCenter.Instance.Guns.Find(array[0]);
			if (gunData.gold >= 0 && Player.Instance.Gold >= (double)gunData.gold)
			{
				return true;
			}
		}
		List<ZS_EquipmentInfo> list2 = CreateAllMeleeWeaponInfo();
		foreach (ZS_EquipmentInfo item2 in list2)
		{
			string[] array = (item2.Data as string).Split(new string[1] { linkString }, StringSplitOptions.RemoveEmptyEntries);
			MeleeWeaponData meleeWeaponData = DataCenter.Instance.MeleeWeapons.Find(array[0]);
			if (meleeWeaponData.gold >= 0 && Player.Instance.Gold >= (double)meleeWeaponData.gold)
			{
				return true;
			}
		}
		return false;
	}

	private double CalculateExp()
	{
		double result = 1.0;
		HeroLevelUpExpData heroLevelUpExpData = DataCenter.Instance.HeroLevelUpDatas.Find(Player.Instance.HeroLevel);
		if (heroLevelUpExpData != null)
		{
			result = Player.Instance.Exp / (double)heroLevelUpExpData.exp;
		}
		return result;
	}

	private double CalculatePreExp(int levelup, double getExp)
	{
		double result = 0.0;
		if (levelup < Player.Instance.HeroLevel)
		{
			double num = 0.0;
			for (int num2 = levelup; num2 > 0; num2--)
			{
				int num3 = Player.Instance.HeroLevel - num2;
				num = ((num3 > 0) ? (num + (double)DataCenter.Instance.HeroLevelUpDatas.Find(num3).exp) : num);
			}
			double num4 = num + Player.Instance.Exp - getExp;
			result = num4 / (double)DataCenter.Instance.HeroLevelUpDatas.Find(Player.Instance.HeroLevel - levelup).exp;
		}
		return result;
	}

	private void TestIAPStatus(ZombieStreetTimer.TimerData data)
	{
		if (timerData != data)
		{
			return;
		}
		SpecailIAPData specailIAPData = data.data as SpecailIAPData;
		IAPData data2 = data.data as IAPData;
		switch ((int)Enum.ToObject(typeof(IAPPlugin.Status), IAPPlugin.GetPurchaseStatus()))
		{
		case 1:
			Player.Instance.AddIAP(data2);
			Player.Instance.Save(true);
			if (specailIAPData != null)
			{
				EventCenter.Instance.Publish(this, new ZS_PublishSpecialIAPResultEvent(0));
			}
			else
			{
				EventCenter.Instance.Publish(this, new ZS_PublishIAPResultEvent(0));
			}
			ZombieStreetTimer.RemoveTimer(timerData);
			break;
		case -1:
			if (specailIAPData != null)
			{
				EventCenter.Instance.Publish(this, new ZS_PublishSpecialIAPResultEvent(-1));
			}
			else
			{
				EventCenter.Instance.Publish(this, new ZS_PublishIAPResultEvent(-1));
			}
			ZombieStreetTimer.RemoveTimer(timerData);
			break;
		case -2:
			if (specailIAPData != null)
			{
				EventCenter.Instance.Publish(this, new ZS_PublishSpecialIAPResultEvent(-1));
			}
			else
			{
				EventCenter.Instance.Publish(this, new ZS_PublishIAPResultEvent(-1));
			}
			ZombieStreetTimer.RemoveTimer(timerData);
			break;
		case 0:
			break;
		}
	}

	private void ShowMessageBox()
	{
		int num = MiscPlugin.ShowMessageBox(string.Empty, "If you like this game, please rate it 5 stars", new List<string> { "Remind me later", "Yes,rate it!" });
		if (num != 0 && num == 1)
		{
			Application.OpenURL(ReviewURL);
			Player.Instance.ShowedReview = true;
		}
	}

	private void InitializeAmazonIAP()
	{
		IAPDataRepository iAPs = DataCenter.Instance.IAPs;
		if (_003C_003Ef__am_0024cacheE == null)
		{
			_003C_003Ef__am_0024cacheE = _003CInitializeAmazonIAP_003Em__50;
		}
		List<IAPData> list = iAPs.FindAll(_003C_003Ef__am_0024cacheE);
		List<string> list2 = new List<string>();
		foreach (IAPData item in list)
		{
			list2.Add(item.id);
		}
		AmazonIAP.initiateItemDataRequest(list2.ToArray());
		AmazonIAPManager.purchaseFailedEvent += purchaseFailedEvent;
		AmazonIAPManager.purchaseSuccessfulEvent += purchaseSuccessfulEvent;
	}

	private void InitializeIAB()
	{
		string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgKfFMDbQXosuiCwmcLhrMbip+dZD1TS+9Wd2E42aDKi4MHcQVnY5QVPmOg4xwkEN2jV4Fp/He38Em8iC2yoXKuI0YqwpdGh4fa4+IBSKf+eh33KLofewmX7NEVReA3ssMnLi6ejwZwIu1MqUe/CNaEjSMBiyBK21I99J1bOPiJ7kDbLyS//WTIZdr1u5nIOwonGj/w7Ptvpaubrf2XdmG8cnkXsEYJch985a7k+rJmus6SnOCqYcXj+CF51Vc4DQD/UDRQwKnM66ebLs1P97st0fH9SmDYr4VLhGuvM3+HLwppP9pbgh32HTHtlwKvSUIC17E5rSrppzJ2RWi6wF3wIDAQAB";
		IABAndroid.init(publicKey);
		IABAndroidManager.purchaseFailedEvent += purchaseFailedEventIAB;
		IABAndroidManager.purchaseSucceededEvent += purchaseSucceededEventIAB;
	}

	private void purchaseFailedEvent(string reason)
	{
		if (iapData != null)
		{
			SpecailIAPData specailIAPData = iapData as SpecailIAPData;
			if (specailIAPData != null)
			{
				EventCenter.Instance.Publish(this, new ZS_PublishSpecialIAPResultEvent(-1));
			}
			else
			{
				EventCenter.Instance.Publish(this, new ZS_PublishIAPResultEvent(-1));
			}
		}
	}

	private void purchaseSuccessfulEvent(AmazonReceipt receipt)
	{
		IAPData iAPData = DataCenter.Instance.IAPs.Find(receipt.sku);
		if (iAPData != null)
		{
			SpecailIAPData specailIAPData = iAPData as SpecailIAPData;
			Player.Instance.AddIAP(iAPData);
			Player.Instance.Save(true);
			if (specailIAPData != null)
			{
				EventCenter.Instance.Publish(this, new ZS_PublishSpecialIAPResultEvent(0));
			}
			else
			{
				EventCenter.Instance.Publish(this, new ZS_PublishIAPResultEvent(0));
			}
		}
	}

	private void purchaseSucceededEventIAB(string productId)
	{
		IAPData iAPData = DataCenter.Instance.IAPs.Find(productId);
		if (iAPData != null)
		{
			SpecailIAPData specailIAPData = iAPData as SpecailIAPData;
			Player.Instance.AddIAP(iAPData);
			Player.Instance.Save(true);
			if (specailIAPData != null)
			{
				EventCenter.Instance.Publish(this, new ZS_PublishSpecialIAPResultEvent(0));
			}
			else
			{
				EventCenter.Instance.Publish(this, new ZS_PublishIAPResultEvent(0));
			}
		}
	}

	private void purchaseFailedEventIAB(string productId)
	{
		IAPData iAPData = DataCenter.Instance.IAPs.Find(productId);
		if (iAPData != null)
		{
			SpecailIAPData specailIAPData = iAPData as SpecailIAPData;
			if (specailIAPData != null)
			{
				EventCenter.Instance.Publish(this, new ZS_PublishSpecialIAPResultEvent(-1));
			}
			else
			{
				EventCenter.Instance.Publish(this, new ZS_PublishIAPResultEvent(-1));
			}
		}
	}

	[CompilerGenerated]
	private static bool _003CCreateAllGunInfo_003Em__45(GunData data)
	{
		return true;
	}

	[CompilerGenerated]
	private static bool _003CHandleZS_PublishAllAvatarPhotoInfoEvent_003Em__48(HeroData data)
	{
		return true;
	}

	[CompilerGenerated]
	private static bool _003CHandleZS_PublishAllItemEvent_003Em__49(IItem item)
	{
		return true;
	}

	[CompilerGenerated]
	private static bool _003CHandleZS_PublishAllItemEvent_003Em__4A(IItem item)
	{
		return !item.BaseData.canAppear;
	}

	[CompilerGenerated]
	private static bool _003CHandleZS_PublishAllItemEvent_003Em__4B(IItem item)
	{
		return !item.BaseData.canAppear;
	}

	[CompilerGenerated]
	private static bool _003CHandleZS_PublishIAPEvent_003Em__4C(IAPData data)
	{
		return true;
	}

	[CompilerGenerated]
	private static bool _003CHandleZS_PublishIAPGoldEvent_003Em__4E(Crystal2Gold data)
	{
		return true;
	}

	[CompilerGenerated]
	private static bool _003CHasAchievementCompeleted_003Em__4F(IAchievement a)
	{
		return a.Progress == 1f && a.State == AchievementState.Processing;
	}

	[CompilerGenerated]
	private static bool _003CInitializeAmazonIAP_003Em__50(IAPData data)
	{
		return true;
	}
}
