using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Event;

[Serializable]
public class ArenaMission : Mission<ArenaMissionData>
{
	[CompilerGenerated]
	private static Predicate<IMission> _003C_003Ef__am_0024cache0;

	public static ArenaMission Instance
	{
		get
		{
			MissionRepository missions = DataCenter.Instance.Missions;
			if (_003C_003Ef__am_0024cache0 == null)
			{
				_003C_003Ef__am_0024cache0 = _003Cget_Instance_003Em__4;
			}
			return missions.FindAll(_003C_003Ef__am_0024cache0)[0] as ArenaMission;
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

	[CompilerGenerated]
	private static bool _003Cget_Instance_003Em__4(IMission mission)
	{
		return mission.Priority == -1;
	}
}
