using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;

[RequireComponent(typeof(ZombieCreator))]
public class ArenaRefreshZombies : MonoBehaviour
{
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
			zombies.ForEach(delegate(Zombie zombie)
			{
				zombie.Disappear();
			});
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
		meters.Sort((int meter1, int meter2) => meter1.CompareTo(meter2));
		StartCoroutine(Refresh(0f));
	}

	private void Awake()
	{
		EventCenter.Instance.Register<CreateEnemy>(HandleCreateEnemyEvent);
		creator = GetComponent<ZombieCreator>();
	}

	private void ClearDeadZombie()
	{
		zombies.RemoveAll((Zombie zombie) => null == zombie);
	}

	private void LockAllZombie()
	{
		zombies.ForEach(delegate(Zombie zombie)
		{
			zombie.Lock();
		});
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
			currentRefreshPoint = refreshPointsT[Random.Range(0, refreshPointsT.Count)];
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
			int meter = Mathf.FloorToInt((target.position - startPoint.position).magnitude);
			int num = 0;
			if (meter >= meters[meters.Count - 1])
			{
				num = meters.Count - 1;
			}
			else
			{
				num = meters.FindIndex((int mt) => mt > meter) - 1;
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
}
