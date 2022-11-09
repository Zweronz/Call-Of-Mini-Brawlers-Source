using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Event;
using UnityEngine;

[RequireComponent(typeof(ZombieCreator))]
public class ArenaRefreshZombies : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CChooseCurrentMeterRule_003Ec__AnonStorey22
	{
		internal int meter;

		internal bool _003C_003Em__17(int mt)
		{
			return mt > meter;
		}
	}

	[SerializeField]
	protected List<Transform> refreshPoints;

	public float interval = 10f;

	public float specialInterval = 10f;

	public int maxZombieCount = 9;

	private ZombieCreator creator;

	private List<Zombie> zombies = new List<Zombie>();

	private List<Transform> refreshPointsT = new List<Transform>();

	private Transform currentRefreshPoint;

	private int level;

	private ArenaMission arenaMission;

	private ArenaMissionData.MeterRule currentMeterRule;

	private List<int> meters = new List<int>();

	private Transform startPoint;

	private Transform target;

	[CompilerGenerated]
	private static Action<Zombie> _003C_003Ef__am_0024cacheE;

	[CompilerGenerated]
	private static Comparison<int> _003C_003Ef__am_0024cacheF;

	[CompilerGenerated]
	private static Predicate<Zombie> _003C_003Ef__am_0024cache10;

	[CompilerGenerated]
	private static Action<Zombie> _003C_003Ef__am_0024cache11;

	public void AddRefreshPoints(params Transform[] points)
	{
		if (refreshPoints == null)
		{
			refreshPoints = new List<Transform>();
		}
		refreshPoints.AddRange(points);
	}

	public void StopAndLock()
	{
		StopAllCoroutines();
		ClearDeadZombie();
		LockAllZombie();
	}

	public void Restart(float interval = 10f, bool killAllZombies = true)
	{
		if (killAllZombies)
		{
			ClearDeadZombie();
			List<Zombie> list = zombies;
			if (_003C_003Ef__am_0024cacheE == null)
			{
				_003C_003Ef__am_0024cacheE = _003CRestart_003Em__13;
			}
			list.ForEach(_003C_003Ef__am_0024cacheE);
		}
		StartCoroutine(Refresh(interval));
	}

	public void StartRefresh(ArenaMission mission, Transform startPoint, Transform target, int level)
	{
		arenaMission = mission;
		this.startPoint = startPoint;
		this.target = target;
		this.level = level;
		meters.Clear();
		meters.AddRange(arenaMission.data.MeterRules.Keys);
		List<int> list = meters;
		if (_003C_003Ef__am_0024cacheF == null)
		{
			_003C_003Ef__am_0024cacheF = _003CStartRefresh_003Em__14;
		}
		list.Sort(_003C_003Ef__am_0024cacheF);
		StartCoroutine(Refresh(0f));
	}

	private void Awake()
	{
		EventCenter.Instance.Register<CreateEnemy>(HandleCreateEnemyEvent);
		creator = GetComponent<ZombieCreator>();
	}

	private void ClearDeadZombie()
	{
		List<Zombie> list = zombies;
		if (_003C_003Ef__am_0024cache10 == null)
		{
			_003C_003Ef__am_0024cache10 = _003CClearDeadZombie_003Em__15;
		}
		list.RemoveAll(_003C_003Ef__am_0024cache10);
	}

	private void LockAllZombie()
	{
		List<Zombie> list = zombies;
		if (_003C_003Ef__am_0024cache11 == null)
		{
			_003C_003Ef__am_0024cache11 = _003CLockAllZombie_003Em__16;
		}
		list.ForEach(_003C_003Ef__am_0024cache11);
	}

	private IEnumerator Refresh(float time)
	{
		yield return new WaitForSeconds(time);
		ChooseCurrentMeterRule();
		ClearDeadZombie();
		if (currentMeterRule != null && currentMeterRule.refreshRules != null && currentMeterRule.refreshRules.Count > 0 && zombies.Count < currentMeterRule.maxZombies)
		{
			RefreshRuleData data = RandomRules(currentMeterRule.refreshRules);
			RefreshEnemyData[] enemies = data.enemies;
			foreach (RefreshEnemyData enemy in enemies)
			{
				for (int i = 0; i < enemy.number; i++)
				{
					if (zombies.Count < maxZombieCount)
					{
						Zombie zombie = creator.Create(new ZombieData(DataCenter.Instance.BaseEnemies.Find(enemy.id), DataCenter.Instance.BaseEnemiesHpDmg.Find(level)), currentMeterRule.enemyRate);
						Transform tran = RandomPoint();
						zombie.Appear(tran);
						zombies.Add(zombie);
					}
				}
			}
		}
		if (currentMeterRule != null)
		{
			StartCoroutine(Refresh(currentMeterRule.interval));
		}
		else
		{
			StartCoroutine(Refresh(interval));
		}
	}

	private RefreshRuleData RandomRules(List<string> refreshRules)
	{
		int num = ZombieStreetCommon.Random(0, refreshRules.Count);
		if (num >= refreshRules.Count)
		{
			num--;
		}
		return DataCenter.Instance.Rules.Find(refreshRules[num]);
	}

	private Transform RandomPoint()
	{
		if (refreshPointsT.Count == 0)
		{
			refreshPointsT.AddRange(refreshPoints);
			if (null != currentRefreshPoint)
			{
				refreshPointsT.Remove(currentRefreshPoint);
			}
		}
		if (refreshPointsT.Count > 0)
		{
			currentRefreshPoint = refreshPointsT[UnityEngine.Random.Range(0, refreshPointsT.Count)];
			refreshPointsT.Remove(currentRefreshPoint);
			return currentRefreshPoint;
		}
		return null;
	}

	private void HandleCreateEnemyEvent(object sender, CreateEnemy evt)
	{
		Zombie zombie = creator.Create(new ZombieData(DataCenter.Instance.BaseEnemies.Find(evt.ID), DataCenter.Instance.BaseEnemiesHpDmg.Find(level)));
		zombie.Appear(evt.Position, evt.Rotation);
		zombies.Add(zombie);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<CreateEnemy>(HandleCreateEnemyEvent);
	}

	private void ChooseCurrentMeterRule()
	{
		if (meters.Count > 0)
		{
			_003CChooseCurrentMeterRule_003Ec__AnonStorey22 _003CChooseCurrentMeterRule_003Ec__AnonStorey = new _003CChooseCurrentMeterRule_003Ec__AnonStorey22();
			_003CChooseCurrentMeterRule_003Ec__AnonStorey.meter = Mathf.FloorToInt((target.position - startPoint.position).magnitude);
			int num = 0;
			if (_003CChooseCurrentMeterRule_003Ec__AnonStorey.meter >= meters[meters.Count - 1])
			{
				num = meters.Count - 1;
			}
			else
			{
				num = meters.FindIndex(_003CChooseCurrentMeterRule_003Ec__AnonStorey._003C_003Em__17) - 1;
				if (num < 0)
				{
					num = 0;
				}
			}
			currentMeterRule = arenaMission.data.MeterRules[meters[num]];
		}
		else
		{
			currentMeterRule = null;
		}
	}

	[CompilerGenerated]
	private static void _003CRestart_003Em__13(Zombie zombie)
	{
		zombie.Disappear();
	}

	[CompilerGenerated]
	private static int _003CStartRefresh_003Em__14(int meter1, int meter2)
	{
		return meter1.CompareTo(meter2);
	}

	[CompilerGenerated]
	private static bool _003CClearDeadZombie_003Em__15(Zombie zombie)
	{
		return null == zombie;
	}

	[CompilerGenerated]
	private static void _003CLockAllZombie_003Em__16(Zombie zombie)
	{
		zombie.Lock();
	}
}
