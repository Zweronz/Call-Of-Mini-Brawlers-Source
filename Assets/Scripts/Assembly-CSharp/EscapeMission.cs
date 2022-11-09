using System;
using System.Collections.Generic;
using Event;

[Serializable]
public class EscapeMission : Mission<EscapeMissionData>
{
	[NonSerialized]
	private EscapeMissionAssist ema;

	public override void Initialize(int level)
	{
		Reset();
		WorldCreator worldCreator = GameLevel.FindWorldCreatorInScene();
		ema = worldCreator.GetComponent<EscapeMissionAssist>();
		if (null == ema)
		{
			ema = worldCreator.gameObject.AddComponent<EscapeMissionAssist>();
		}
		ema.distance = data.distance + data.rise * (float)(level - data.minLevel);
		ema.startPoint = worldCreator.heroPoint;
		ema.targetTag = "Hero";
		ema.action = HandleEscapeMissionAssistAction;
		ema.isStarted = false;
		ema.process = 0f;
	}

	public override void Start()
	{
		ema.isStarted = true;
		EventCenter.Instance.Unregister<HeroDeadEvent>(HandleHeroDead);
		EventCenter.Instance.Register<HeroDeadEvent>(HandleHeroDead);
	}

	public override void Reset(bool resetInfo = true)
	{
		base.State = MissionState.Performing;
	}

	public override void InitializeUI()
	{
		MissionUICreator.Instance.CreateEscapeMissionUI(this);
	}

	public override float GetProcess()
	{
		if (null == ema)
		{
			return 0f;
		}
		return ema.process;
	}

	public override List<object> GetDescData(int level)
	{
		List<object> list = new List<object>();
		list.Add((int)(data.distance + data.rise * (float)(level - data.minLevel)));
		return list;
	}

	private void HandleEscapeMissionAssistAction()
	{
		base.State = MissionState.Complete;
	}

	private void HandleHeroDead(object sender, HeroDeadEvent evt)
	{
		base.State = MissionState.Failure;
	}
}
