using System.Collections;

public class MyFlurry
{
	public enum MissionEndResult
	{
		Win = 0,
		Lose = 1,
		Retreat = 2
	}

	public static void Initialize()
	{
		FlurryPlugin.StartSession("FK8NKV3WJY6D4VM4YTKH");
	}

	public static void BuyItem(string itemId)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Item", itemId);
		FlurryPlugin.logEvent("Buy_Item", hashtable);
	}

	public static void UseItem(string itemId, int gameLevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Item", itemId);
		hashtable.Add("Mission", gameLevel);
		FlurryPlugin.logEvent("Use_Item", hashtable);
	}

	public static void MissionBegin(int gameLevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Mission", gameLevel);
		FlurryPlugin.logEvent("Mission", hashtable, true);
	}

	public static void MissionEnd(int gameLevel, int heroLevel, MissionEndResult result)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Result", result.ToString());
		FlurryPlugin.endTimedEvent("Mission", hashtable);
		switch (result)
		{
		case MissionEndResult.Win:
			MissionVictory(gameLevel, heroLevel);
			break;
		case MissionEndResult.Lose:
			MissionDefeat(gameLevel, heroLevel);
			break;
		}
	}

	public static void MissionVictory(int gameLevel, int heroLevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Mission", gameLevel);
		hashtable.Add("Player_Level", heroLevel);
		FlurryPlugin.logEvent("Mission_Victory", hashtable);
	}

	public static void MissionDefeat(int gameLevel, int heroLevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Mission", gameLevel);
		hashtable.Add("Player_Level", heroLevel);
		FlurryPlugin.logEvent("Mission_Defeat", hashtable);
	}

	public static void BuyWeapon(string weaponTypeName)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Weapon", weaponTypeName);
		FlurryPlugin.logEvent("Weapon_Buy", hashtable);
	}

	public static void UpgradeWeapon(string weaponTypeName, int level, int heroLevel, int gameLevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Weapon", weaponTypeName);
		hashtable.Add("Upgrade_Level", level);
		FlurryPlugin.logEvent("Weapon_Upgrade", hashtable);
		switch (level)
		{
		case 5:
			UpgradeWeaponTo5(weaponTypeName, heroLevel, gameLevel);
			break;
		case 10:
			UpgradeWeaponTo10(weaponTypeName, heroLevel, gameLevel);
			break;
		}
	}

	public static void UpgradeWeaponTo5(string weaponTypeName, int heroLevel, int gameLevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Weapon", weaponTypeName);
		hashtable.Add("Player_Level", heroLevel);
		hashtable.Add("Current_Mission", gameLevel);
		FlurryPlugin.logEvent("Weapon_Level_5", hashtable);
	}

	public static void UpgradeWeaponTo10(string weaponTypeName, int heroLevel, int gameLevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Weapon", weaponTypeName);
		hashtable.Add("Player_Level", heroLevel);
		hashtable.Add("Current_Mission", gameLevel);
		FlurryPlugin.logEvent("Weapon_Level_10", hashtable);
	}

	public static void HeroLevelUp(int heroLevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Level", heroLevel);
		FlurryPlugin.logEvent("Level_Up", hashtable);
	}

	public static void BuyHero(string heroName)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Avatar", heroName);
		FlurryPlugin.logEvent("Avatar_Buy", hashtable);
	}

	public static void AchievementComplete(string achievementId)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Achievement", achievementId);
		FlurryPlugin.logEvent("Achievement", hashtable);
	}

	public static void Jailbreak()
	{
		FlurryPlugin.logEvent("Jailbreak");
	}

	public static void MoneyStage(int level, double money)
	{
		if (level >= 10 && level % 10 == 0)
		{
			Hashtable hashtable = new Hashtable();
			string empty = string.Empty;
			empty = ((money <= 4999.0) ? "0-4999" : ((money <= 9999.0) ? "5000-9999" : ((money <= 19999.0) ? "10000-19999" : ((money <= 49999.0) ? "20000-49999" : ((money <= 99999.0) ? "50000-99999" : ((!(money <= 199999.0)) ? "200000-99999999" : "100000-199999"))))));
			hashtable.Add("Money", empty);
			FlurryPlugin.logEvent("Money_Stage_" + level, hashtable);
		}
	}
}
