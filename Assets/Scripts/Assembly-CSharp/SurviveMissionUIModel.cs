public class SurviveMissionUIModel : MissionUIModel<SurviveMission, SurviveMissionData>
{
	public TUILabel label;

	private SurviveMission data;

	public override void Initialize(SurviveMission data)
	{
		this.data = data;
	}

	private void LateUpdate()
	{
		label.Text = ZombieStreetCommon.Time2Str((int)(data.GetMaxTime() * data.GetProcess() * 1000f));
	}
}
