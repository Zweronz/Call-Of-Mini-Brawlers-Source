using System;
using System.Collections.Generic;
using Event;

[Serializable]
public class SlaughterMission : Mission<SlaughterMissionData>
{
	[NonSerialized]
	private Dictionary<int, int> realTargets;

	[NonSerialized]
	private float process;

	[NonSerialized]
	private int enemySum;

	[NonSerialized]
	private int killedEnemyCount;

	public override void Initialize(int level)
	{
		Reset();
		foreach (SlaughterMissionData.SlaughterTargetData target in data.targets)
		{
			if (realTargets.ContainsKey(target.id))
			{
				Dictionary<int, int> dictionary;
				Dictionary<int, int> dictionary2 = (dictionary = realTargets);
				int id;
				int key = (id = target.id);
				id = dictionary[id];
				dictionary2[key] = id + (target.count + (int)target.rise * (level - data.minLevel));
			}
			else
			{
				realTargets.Add(target.id, target.count + (int)target.rise * (level - data.minLevel));
			}
			enemySum += target.count + (int)target.rise * (level - data.minLevel);
		}
	}

	public override void Start()
	{
		EventCenter.Instance.Unregister<ZombieDeadEvent>(HandleEnemyDead);
		EventCenter.Instance.Register<ZombieDeadEvent>(HandleEnemyDead);
		EventCenter.Instance.Unregister<HeroDeadEvent>(HandleHeroDead);
		EventCenter.Instance.Register<HeroDeadEvent>(HandleHeroDead);
	}

	public override void Reset(bool resetInfo = true)
	{
		base.State = MissionState.Performing;
		if (resetInfo)
		{
			if (realTargets == null)
			{
				realTargets = new Dictionary<int, int>();
			}
			else
			{
				realTargets.Clear();
			}
			process = 0f;
			enemySum = 0;
			killedEnemyCount = 0;
		}
	}

	public override void InitializeUI()
	{
		MissionUICreator.Instance.CreateSlaughterMissionUI(this);
	}

	public override float GetProcess()
	{
		return process;
	}

	public int GetKilledEnemySum()
	{
		return killedEnemyCount;
	}

	public int GetEnemySum()
	{
		return enemySum;
	}

	public override List<object> GetDescData(int level)
	{
		List<object> list = new List<object>();
		Reset();
		foreach (SlaughterMissionData.SlaughterTargetData target in data.targets)
		{
			if (realTargets.ContainsKey(target.id))
			{
				Dictionary<int, int> dictionary;
				Dictionary<int, int> dictionary2 = (dictionary = realTargets);
				int id;
				int key = (id = target.id);
				id = dictionary[id];
				dictionary2[key] = id + (target.count + (int)target.rise * (level - data.minLevel));
			}
			else
			{
				realTargets.Add(target.id, target.count + (int)target.rise * (level - data.minLevel));
			}
			enemySum += target.count + (int)target.rise * (level - data.minLevel);
		}
		list.Add(enemySum);
		return list;
	}

	private void HandleEnemyDead(object sender, ZombieDeadEvent evt)
	{
		if (realTargets == null)
		{
			return;
		}
		if (realTargets.ContainsKey(evt.ZombieID) && realTargets[evt.ZombieID] > 0)
		{
			Dictionary<int, int> dictionary;
			Dictionary<int, int> dictionary2 = (dictionary = realTargets);
			int zombieID;
			int key = (zombieID = evt.ZombieID);
			zombieID = dictionary[zombieID];
			dictionary2[key] = zombieID - 1;
		}
		if (realTargets.ContainsKey(0) && realTargets[0] > 0)
		{
			Dictionary<int, int> dictionary3;
			Dictionary<int, int> dictionary4 = (dictionary3 = realTargets);
			int zombieID;
			int key2 = (zombieID = 0);
			zombieID = dictionary3[zombieID];
			dictionary4[key2] = zombieID - 1;
		}
		int num = 0;
		foreach (int value in realTargets.Values)
		{
			num += value;
		}
		if (num == 0)
		{
			base.State = MissionState.Complete;
		}
		process = 1f - (float)num * 1f / (float)enemySum;
		killedEnemyCount = enemySum - num;
	}

	private void HandleHeroDead(object sender, HeroDeadEvent evt)
	{
		base.State = MissionState.Failure;
	}
}
