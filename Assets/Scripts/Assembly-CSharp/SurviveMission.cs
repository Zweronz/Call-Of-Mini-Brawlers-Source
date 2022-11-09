using System;
using System.Collections.Generic;
using Event;

[Serializable]
public class SurviveMission : Mission<SurviveMissionData>
{
	[NonSerialized]
	private ZombieStreetTimer.TimerData timerData;

	[NonSerialized]
	private float maxTime;

	public override void Initialize(int level)
	{
		Reset();
		timerData = new ZombieStreetTimer.TimerData();
		timerData.time = data.time + data.rise * (float)(level - data.minLevel);
		timerData.invokeTimes = 1;
		timerData.ingoreTimeScale = false;
		timerData.handler = TimerHandler;
		maxTime = timerData.time;
	}

	public override void Start()
	{
		ZombieStreetTimer.Instance.AddTimer(timerData);
		EventCenter.Instance.Unregister<HeroDeadEvent>(HandleHeroDead);
		EventCenter.Instance.Register<HeroDeadEvent>(HandleHeroDead);
	}

	public override void Reset(bool resetInfo = true)
	{
		base.State = MissionState.Performing;
	}

	public override void InitializeUI()
	{
		MissionUICreator.Instance.CreateSurviveMissionUI(this);
	}

	public override float GetProcess()
	{
		if (timerData == null)
		{
			return 0f;
		}
		return timerData.time / maxTime;
	}

	public float GetMaxTime()
	{
		return maxTime;
	}

	public override List<object> GetDescData(int level)
	{
		List<object> list = new List<object>();
		list.Add((int)(data.time + data.rise * (float)(level - data.minLevel)));
		return list;
	}

	private void TimerHandler(ZombieStreetTimer.TimerData data)
	{
		base.State = MissionState.Complete;
		ZombieStreetTimer.RemoveTimer(data);
	}

	private void HandleHeroDead(object sender, HeroDeadEvent evt)
	{
		base.State = MissionState.Failure;
		if (timerData != null)
		{
			ZombieStreetTimer.RemoveTimer(timerData);
		}
	}
}
