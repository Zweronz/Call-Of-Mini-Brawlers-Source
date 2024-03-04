using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;

[RequireComponent(typeof(ZombieCreator))]
public class RefreshZombies : MonoBehaviour
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

	private List<string> refreshRules = new List<string>();

	private List<string> specialRefreshRules = new List<string>();

	private int level;

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

	public void StartRefresh(List<string> refreshRules, int level)
	{
		this.refreshRules.Clear();
		if (refreshRules != null)
		{
			this.refreshRules.AddRange(refreshRules);
		}
		this.level = level;
		StartCoroutine(Refresh(0f));
	}

	public void StartSpecialRefresh(List<string> refreshRules, int level, bool refreshNow = true)
	{
		specialRefreshRules.Clear();
		if (refreshRules != null)
		{
			specialRefreshRules.AddRange(refreshRules);
		}
		this.level = level;
		StartCoroutine(SpecialRefresh((!refreshNow) ? specialInterval : 0f));
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
		ClearDeadZombie();
		if (refreshRules.Count > 0 && zombies.Count < maxZombieCount)
		{
			RefreshRuleData data = RandomRules(refreshRules);
			RefreshEnemyData[] enemies = data.enemies;
			foreach (RefreshEnemyData enemy in enemies)
			{
				for (int i = 0; i < enemy.number; i++)
				{
					if (zombies.Count < maxZombieCount)
					{
						Zombie zombie = creator.Create(new ZombieData(DataCenter.Instance.BaseEnemies.Find(enemy.id), DataCenter.Instance.BaseEnemiesHpDmg.Find(level)));
						Transform tran = RandomPoint();
						zombie.Appear(tran);
						zombies.Add(zombie);
					}
				}
			}
		}
		StartCoroutine(Refresh(interval));
	}

	private IEnumerator SpecialRefresh(float time)
	{
		yield return new WaitForSeconds(time);
		ClearDeadZombie();
		if (specialRefreshRules.Count > 0 && zombies.Count < maxZombieCount)
		{
			RefreshRuleData data = RandomRules(specialRefreshRules);
			RefreshEnemyData[] enemies = data.enemies;
			foreach (RefreshEnemyData enemy in enemies)
			{
				for (int i = 0; i < enemy.number; i++)
				{
					Zombie zombie = creator.Create(new ZombieData(DataCenter.Instance.BaseEnemies.Find(enemy.id), DataCenter.Instance.BaseEnemiesHpDmg.Find(level)));
					Transform tran = RandomPoint();
					zombie.Appear(tran);
					zombies.Add(zombie);
				}
			}
		}
		StartCoroutine(SpecialRefresh(specialInterval));
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
}
