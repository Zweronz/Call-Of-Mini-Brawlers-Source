using System;
using System.Collections.Generic;
using Event;

[Serializable]
public class ArenaMission : Mission<ArenaMissionData>
{
	public static ArenaMission Instance
	{
		get
		{
			return DataCenter.Instance.Missions.FindAll((IMission mission) => mission.Priority == -1)[0] as ArenaMission;
		}
	}

	public override bool IsAvailable(int level)
	{
		return false;
	}

	public override void Start()
	{
		Reset();
		EventCenter.Instance.Unregister<HeroDeadEvent>(HandleHeroDead);
		EventCenter.Instance.Register<HeroDeadEvent>(HandleHeroDead);
	}

	public override void Reset(bool resetInfo = true)
	{
		base.State = MissionState.Performing;
	}

	public override void Initialize(int level)
	{
	}

	public override void InitializeUI()
	{
		MissionUICreator.Instance.CreateArenaMissionUI(this);
	}

	public override float GetProcess()
	{
		return 0f;
	}

	public override List<object> GetDescData(int level)
	{
		return new List<object>();
	}

	private void HandleHeroDead(object sender, HeroDeadEvent evt)
	{
		base.State = MissionState.Failure;
	}
}
