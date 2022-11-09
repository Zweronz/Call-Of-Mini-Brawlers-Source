using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Event;
using UnityEngine;

public class ArenaGameLevel : GameLevel
{
	public int min;

	public int interval;

	public ArenaRefreshZombies arenaRefreshZombies;

	public RefreshFriendChest refreshFriendChest;

	private static List<GameCenterModel.FriendScore> fsList = new List<GameCenterModel.FriendScore>();

	private ArenaMissionDetail arenaDetail;

	private int reviveCount;

	[CompilerGenerated]
	private static Predicate<string> _003C_003Ef__am_0024cache7;

	[CompilerGenerated]
	private static Comparison<GameCenterModel.FriendScore> _003C_003Ef__am_0024cache8;

	public static void SetFriendScoreList(List<GameCenterModel.FriendScore> fsList)
	{
		if (fsList != null)
		{
			ArenaGameLevel.fsList.Clear();
			ArenaGameLevel.fsList.AddRange(fsList);
		}
		if (ArenaGameLevel.fsList.Count == 0)
		{
			ArenaGameLevel.fsList.Clear();
			ArenaGameLevel.fsList.AddRange(GameCenterModel.LastFriendScores);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		EventCenter.Instance.Register<HeroReviveEvent>(HandleHeroReviveEvent);
		EventCenter.Instance.Register<SkipReviveEvent>(HandleSkipReviveEvent);
	}

	public override void InitLevel()
	{
		Time.timeScale = 1f;
		Player.Instance.TempData();
		mission = ArenaMission.Instance;
		worldCreator = GameLevel.FindWorldCreatorInScene();
		worldCreator.CreateScene();
		WeaponArsenal weaponArsenal = worldCreator.CreateArsenal();
		worldCreator.SetEndPoint(mission.SceneLength);
		arenaRefreshZombies.AddRefreshPoints(worldCreator.refreshPoints.ToArray());
		List<string> list = new List<string>();
		list.AddRange(Player.Instance.Guns.ToArray());
		if (_003C_003Ef__am_0024cache7 == null)
		{
			_003C_003Ef__am_0024cache7 = _003CInitLevel_003Em__11;
		}
		list.RemoveAll(_003C_003Ef__am_0024cache7);
		weaponArsenal.TakeOver(gunAssembly.Create(DataCenter.Instance.Guns.Find(list.ToArray()).ToArray()).ToArray());
		weaponArsenal.TakeOver(meleeWeaponAssembly.Create(DataCenter.Instance.MeleeWeapons.Find(Player.Instance.CurrentHero.meleeWeapon)));
		hero = heroCreator.Create(DataCenter.Instance.Heros.Find(Player.Instance.CurrentHero.id));
		hero.SetOriginPoint(worldCreator.heroPoint);
		hero.Instantiate(DataCenter.Instance.Heros.Find(Player.Instance.CurrentHero.id), Player.Instance.HeroLevel);
		CameraFollowHero component = Camera.main.GetComponent<CameraFollowHero>();
		component.hero = hero.gameObject;
		arenaDetail = new ArenaMissionDetail();
		arenaDetail.MissionDetailData.level = level;
		arenaDetail.MissionDetailData.missionId = mission.ID;
		arenaDetail.MissionDetailData.heroId = Player.Instance.CurrentHero.id;
		arenaDetail.MissionDetailData.enterLevel = Player.Instance.HeroLevel;
		CharacterInputJudgment.Instance.Lock();
		mission.Reset();
		mission.Initialize(level);
		mission.InitializeUI();
		PublishHeroGoldAndExpChangeEvent();
		StartCoroutine(GameStart());
		MyFlurry.MissionBegin(level);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (arenaDetail != null)
		{
			arenaDetail.DestroyListener();
		}
		EventCenter.Instance.Unregister<HeroReviveEvent>(HandleHeroReviveEvent);
		EventCenter.Instance.Unregister<SkipReviveEvent>(HandleSkipReviveEvent);
	}

	protected override void GameOver(bool completed)
	{
		CleanSaliva();
		CharacterInputJudgment.Instance.Lock();
		arenaRefreshZombies.StopAndLock();
		int num = (reviveCount + 1) * 2;
		if (num > 10)
		{
			num = 10;
		}
		EventCenter.Instance.Publish(null, new ShowReviveEvent(num));
	}

	protected void RealGameOver()
	{
		bool flag = false;
		MyFlurry.MissionEnd(level, arenaDetail.MissionDetailData.enterLevel, (!flag) ? MyFlurry.MissionEndResult.Lose : MyFlurry.MissionEndResult.Win);
		arenaDetail.MissionDetailData.isCompleted = flag;
		hero.isGod = true;
		Player.Instance.AddGold(arenaDetail.MissionDetailData.bonus);
		arenaDetail.MissionDetailData.levelUp = Player.Instance.HeroLevel - arenaDetail.MissionDetailData.enterLevel;
		arenaDetail.MissionDetailData.maxHp = hero.MaxHp;
		arenaDetail.MissionDetailData.residualHp = hero.Hp;
		Vector3 lhs = hero.transform.position - worldCreator.heroPoint.position;
		int num = Mathf.FloorToInt(lhs.magnitude);
		if (Vector3.Dot(lhs, worldCreator.heroPoint.forward) < 0f)
		{
			num = 0;
		}
		arenaDetail.MissionDetailData.arenaScore = num;
		Player.Instance.ArenaScore = arenaDetail.MissionDetailData.arenaScore;
		arenaDetail.ChooseDefeatFriends(fsList, arenaDetail.MissionDetailData.arenaScore);
		Player.Instance.AddAneraMissionDeatilData(arenaDetail.MissionDetailData);
		AchievementTool.CalculateAchievements();
		Player.Instance.LastArenaMissionData = arenaDetail.MissionDetailData;
		if (arenaDetail.MissionDetailData.levelUp > 0)
		{
			MyFlurry.HeroLevelUp(arenaDetail.MissionDetailData.levelUp);
		}
		if (flag)
		{
			MyFlurry.MoneyStage(level, Player.Instance.Gold);
		}
		if (flag)
		{
			hero.Stand();
		}
		OnGameOverEnd();
	}

	protected override IEnumerator GameStart()
	{
		yield return new WaitForEndOfFrame();
		gameso.GameStart(TUITextManager.Instance().GetString("Text081"));
	}

	protected override void OnGameStartEnd()
	{
		mission.Start();
		arenaRefreshZombies.StartRefresh(mission as ArenaMission, worldCreator.heroPoint, hero.transform, level);
		refreshChest.Instantiate(worldCreator.heroPoint, hero.transform, mission.SceneLength);
		CharacterInputJudgment.Instance.ClearLock();
		List<GameCenterModel.FriendScore> list = new List<GameCenterModel.FriendScore>();
		List<GameCenterModel.FriendScore> list2 = new List<GameCenterModel.FriendScore>();
		list2.AddRange(fsList);
		if (_003C_003Ef__am_0024cache8 == null)
		{
			_003C_003Ef__am_0024cache8 = _003COnGameStartEnd_003Em__12;
		}
		list2.Sort(_003C_003Ef__am_0024cache8);
		long num = min;
		for (int i = 0; i < list2.Count; i++)
		{
			if (num <= list2[i].score.value)
			{
				num = list2[i].score.value + interval;
				list.Add(list2[i]);
			}
		}
		refreshFriendChest.Instantiate(worldCreator.heroPoint, hero.transform, list);
	}

	protected override void OnGameOverEnd()
	{
		Player.Instance.Save();
		MyGameCenter.Report();
		OpenClikPlugin.Show(false);
		ChartBoostAndroid.showInterstitial(null);
		Time.timeScale = 1f;
		int num = 0;
		foreach (int value in arenaDetail.MissionDetailData.killedZombiesByZombieId.Values)
		{
			num += value;
		}
		EventCenter.Instance.Publish(this, new ArenaGameOverEvent(arenaDetail));
	}

	protected override void HandleZombieDeadEvent(object sender, ZombieDeadEvent evt)
	{
		HeroData heroData = DataCenter.Instance.Heros.Find(arenaDetail.MissionDetailData.heroId);
		EnemyBaseData enemyBaseData = DataCenter.Instance.BaseEnemies.Find(evt.ZombieID);
		EnemyBaseHpDmgData enemyBaseHpDmgData = DataCenter.Instance.BaseEnemiesHpDmg.Find(arenaDetail.MissionDetailData.level);
		int num = (int)((enemyBaseData.coefficientOfGold * enemyBaseHpDmgData.gold + UnityEngine.Random.Range(0f, enemyBaseHpDmgData.extra)) * heroData.coefficientOfGold * ((evt.Rate == null) ? 1f : evt.Rate.goldRate));
		int num2 = (int)((enemyBaseData.coefficientOfGold * enemyBaseHpDmgData.gold + UnityEngine.Random.Range(0f, enemyBaseHpDmgData.extra)) * heroData.coefficientOfGold * ((evt.Rate == null) ? 1f : evt.Rate.expRate));
		Player.Instance.AddGold(num);
		arenaDetail.MissionDetailData.getGold += num;
		int num3 = Player.Instance.AddExp(num2);
		arenaDetail.MissionDetailData.getExp += num2;
		arenaDetail.MissionDetailData.levelUp = num3;
		if (num3 > 0)
		{
			hero.LevelUp();
		}
		PublishHeroGoldAndExpChangeEvent();
	}

	protected virtual void HandleHeroReviveEvent(object sender, HeroReviveEvent evt)
	{
		if (isOver)
		{
			mission.Reset(false);
			hero.Revive();
			isOver = false;
			Time.timeScale = 1f;
			arenaRefreshZombies.Restart(3f);
			reviveCount++;
			CharacterInputJudgment.Instance.Unlock();
		}
	}

	protected virtual void HandleSkipReviveEvent(object sender, SkipReviveEvent evt)
	{
		RealGameOver();
	}

	private void CleanSaliva()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Saliva");
		if (array == null)
		{
			return;
		}
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (null != gameObject)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	[CompilerGenerated]
	private static bool _003CInitLevel_003Em__11(string gunId)
	{
		return string.IsNullOrEmpty(gunId);
	}

	[CompilerGenerated]
	private static int _003COnGameStartEnd_003Em__12(GameCenterModel.FriendScore fs1, GameCenterModel.FriendScore fs2)
	{
		return fs1.score.value.CompareTo(fs2.score.value);
	}
}
