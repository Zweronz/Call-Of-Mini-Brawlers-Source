using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
	public int level;

	public string missionID;

	public MeleeWeaponAssembly meleeWeaponAssembly;

	public GunAssembly gunAssembly;

	public HeroCreator heroCreator;

	public RefreshZombies refreshZombies;

	public RefreshChest refreshChest;

	public Vector3 listenLocalPos;

	public GameStartOver gameso;

	protected WorldCreator worldCreator;

	protected Hero hero;

	protected IMission mission;

	protected bool isOver;

	protected MissionDetail detail;

	protected virtual void Awake()
	{
		level = Player.Instance.GameLevel;
		missionID = ChooseMission.missionId;
		EventCenter.Instance.Register<GameStartEndEvent>(HandleGameStartEndEvent);
		EventCenter.Instance.Register<GameOverEndEvent>(HandleGameOverEndEvent);
		EventCenter.Instance.Register<ZombieDeadEvent>(HandleZombieDeadEvent);
		EventCenter.Instance.Register<GameRetreatEvent>(HandleGameRetreatEvent);
		EventCenter.Instance.Register<GameCloseEvent>(HandleGameCloseEvent);
	}

	private void Start()
	{
		OpenClikPlugin.Hide();
		InitLevel();
	}

	private void Update()
	{
		if (isOver)
		{
			return;
		}
		TAudioManager.instance.AudioListener.transform.position = listenLocalPos + hero.transform.position;
		if (mission != null)
		{
			switch (mission.State)
			{
			case MissionState.Complete:
				isOver = true;
				GameOver(true);
				break;
			case MissionState.Failure:
				isOver = true;
				GameOver(false);
				break;
			}
		}
	}

	public virtual void InitLevel()
	{
		Time.timeScale = 1f;
		Player.Instance.TempData();
		mission = DataCenter.Instance.Missions.Find(missionID);
		worldCreator = FindWorldCreatorInScene();
		worldCreator.CreateScene();
		WeaponArsenal weaponArsenal = worldCreator.CreateArsenal();
		worldCreator.SetEndPoint(mission.SceneLength);
		refreshZombies.AddRefreshPoints(worldCreator.refreshPoints.ToArray());
		List<string> list = new List<string>();
		list.AddRange(Player.Instance.Guns.ToArray());
		list.RemoveAll((string gunId) => string.IsNullOrEmpty(gunId));
		weaponArsenal.TakeOver(gunAssembly.Create(DataCenter.Instance.Guns.Find(list.ToArray()).ToArray()).ToArray());
		weaponArsenal.TakeOver(meleeWeaponAssembly.Create(DataCenter.Instance.MeleeWeapons.Find(Player.Instance.CurrentHero.meleeWeapon)));
		hero = heroCreator.Create(DataCenter.Instance.Heros.Find(Player.Instance.CurrentHero.id));
		hero.SetOriginPoint(worldCreator.heroPoint);
		hero.Instantiate(DataCenter.Instance.Heros.Find(Player.Instance.CurrentHero.id), Player.Instance.HeroLevel);
		CameraFollowHero component = Camera.main.GetComponent<CameraFollowHero>();
		component.hero = hero.gameObject;
		detail = new MissionDetail();
		detail.MissionDetailData.level = level;
		detail.MissionDetailData.missionId = missionID;
		detail.MissionDetailData.heroId = Player.Instance.CurrentHero.id;
		detail.MissionDetailData.enterLevel = Player.Instance.HeroLevel;
		CharacterInputJudgment.Instance.Lock();
		mission.Reset();
		mission.Initialize(level);
		mission.InitializeUI();
		PublishHeroGoldAndExpChangeEvent();
		StartCoroutine(GameStart());
		MyFlurry.MissionBegin(level);
	}

	public static WorldCreator FindWorldCreatorInScene()
	{
		return GameObject.FindWithTag("WorldCreator").GetComponent<WorldCreator>();
	}

	private void RetreatGame()
	{
		Player.Instance.RollbackData();
		MyFlurry.MissionEnd(level, Player.Instance.HeroLevel, MyFlurry.MissionEndResult.Retreat);
		Application.LoadLevel(Application.loadedLevel);
	}

	private void CloseGame(bool reroll)
	{
		Time.timeScale = 1f;
		if (reroll)
		{
			Player.Instance.RollbackData();
		}
		OpenClikPlugin.Hide();
		MyFlurry.MissionEnd(level, Player.Instance.HeroLevel, MyFlurry.MissionEndResult.Retreat);
		GameLoading.loadingScene = "EquipmentUI";
		Application.LoadLevel("LoadingUI");
	}

	protected virtual void GameOver(bool completed)
	{
		MyFlurry.MissionEnd(level, detail.MissionDetailData.enterLevel, (!completed) ? MyFlurry.MissionEndResult.Lose : MyFlurry.MissionEndResult.Win);
		detail.MissionDetailData.isCompleted = completed;
		if (completed)
		{
			detail.MissionDetailData.bonus = DataCenter.Instance.BaseEnemiesHpDmg.Find(detail.MissionDetailData.level).bonus;
			Player.Instance.GameLevel++;
			Player.Instance.RemoveMission(ChooseMission.mapPointId);
		}
		hero.isGod = true;
		Player.Instance.AddGold(detail.MissionDetailData.bonus);
		detail.MissionDetailData.levelUp = Player.Instance.HeroLevel - detail.MissionDetailData.enterLevel;
		detail.MissionDetailData.maxHp = hero.MaxHp;
		detail.MissionDetailData.residualHp = hero.Hp;
		Player.Instance.AddMissionDeatilData(detail.MissionDetailData);
		AchievementTool.CalculateAchievements();
		Player.Instance.LastMissionData = detail.MissionDetailData;
		if (detail.MissionDetailData.levelUp > 0)
		{
			MyFlurry.HeroLevelUp(detail.MissionDetailData.levelUp);
		}
		if (completed)
		{
			MyFlurry.MoneyStage(level, Player.Instance.Gold);
		}
		CharacterInputJudgment.Instance.Lock();
		refreshZombies.StopAndLock();
		if (completed)
		{
			hero.Stand();
		}
		OnGameOverEnd();
	}

	protected virtual void OnDestroy()
	{
		if (detail != null)
		{
			detail.DestroyListener();
		}
		EventCenter.Instance.Unregister<GameStartEndEvent>(HandleGameStartEndEvent);
		EventCenter.Instance.Unregister<GameOverEndEvent>(HandleGameOverEndEvent);
		EventCenter.Instance.Unregister<ZombieDeadEvent>(HandleZombieDeadEvent);
		EventCenter.Instance.Unregister<GameRetreatEvent>(HandleGameRetreatEvent);
		EventCenter.Instance.Unregister<GameCloseEvent>(HandleGameCloseEvent);
	}

	protected virtual void OnGameStartEnd()
	{
		refreshZombies.interval = mission.RefreshZombieInterval;
		refreshZombies.StartRefresh(mission.RefreshRules, level);
		if (mission.NeedSpecialRefresh)
		{
			refreshZombies.specialInterval = mission.SpecialRefreshZombieInterval;
			refreshZombies.StartSpecialRefresh(mission.SpecialRefreshRules, level, false);
		}
		mission.Start();
		refreshChest.Instantiate(worldCreator.heroPoint, hero.transform, mission.SceneLength);
		CharacterInputJudgment.Instance.ClearLock();
	}

	protected virtual void OnGameOverEnd()
	{
		Player.Instance.Save();
		MyGameCenter.Report();
		OpenClikPlugin.Show(false);
		ChartBoostAndroid.showInterstitial(null);
		Time.timeScale = 1f;
		int num = 0;
		foreach (int value in detail.MissionDetailData.killedZombiesByZombieId.Values)
		{
			num += value;
		}
		EventCenter.Instance.Publish(this, new GameOverEvent(detail.MissionDetailData.isCompleted, (int)detail.MissionDetailData.getGold + (int)detail.MissionDetailData.bonus, (int)detail.MissionDetailData.getGold, num, (int)detail.MissionDetailData.bonus));
	}

	private void HandleGameStartEndEvent(object sender, GameStartEndEvent evt)
	{
		OnGameStartEnd();
	}

	private void HandleGameOverEndEvent(object sender, GameOverEndEvent evt)
	{
		OnGameOverEnd();
	}

	protected virtual void HandleZombieDeadEvent(object sender, ZombieDeadEvent evt)
	{
		HeroData heroData = DataCenter.Instance.Heros.Find(detail.MissionDetailData.heroId);
		EnemyBaseData enemyBaseData = DataCenter.Instance.BaseEnemies.Find(evt.ZombieID);
		EnemyBaseHpDmgData enemyBaseHpDmgData = DataCenter.Instance.BaseEnemiesHpDmg.Find(detail.MissionDetailData.level);
		int num = (int)((enemyBaseData.coefficientOfGold * enemyBaseHpDmgData.gold + Random.Range(0f, enemyBaseHpDmgData.extra)) * heroData.coefficientOfGold);
		int num2 = (int)((enemyBaseData.coefficientOfGold * enemyBaseHpDmgData.gold + Random.Range(0f, enemyBaseHpDmgData.extra)) * heroData.coefficientOfGold);
		Player.Instance.AddGold(num);
		detail.MissionDetailData.getGold += num;
		int num3 = Player.Instance.AddExp(num2);
		detail.MissionDetailData.getExp += num2;
		detail.MissionDetailData.levelUp = num3;
		if (num3 > 0)
		{
			hero.LevelUp();
		}
		PublishHeroGoldAndExpChangeEvent();
	}

	private void HandleGameRetreatEvent(object seneder, GameRetreatEvent evt)
	{
		RetreatGame();
	}

	private void HandleGameCloseEvent(object seneder, GameCloseEvent evt)
	{
		CloseGame(evt.RerollPlayer);
	}

	protected void PublishHeroGoldAndExpChangeEvent()
	{
		EventCenter.Instance.Publish(this, new GoldChangeEvent(Player.Instance.Gold));
		float num = -1f;
		HeroLevelUpExpData heroLevelUpExpData = DataCenter.Instance.HeroLevelUpDatas.Find(Player.Instance.HeroLevel);
		if (heroLevelUpExpData != null)
		{
			num = DataCenter.Instance.HeroLevelUpDatas.Find(Player.Instance.HeroLevel).exp;
		}
		EventCenter.Instance.Publish(this, new ExpChangeEvent(Player.Instance.Exp, (!(num > 0f)) ? (-1f) : num));
	}

	protected virtual IEnumerator GameStart()
	{
		yield return new WaitForEndOfFrame();
		gameso.GameStart("Level " + level);
	}
}
