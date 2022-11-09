public class EscapeMissionUIModel : MissionUIModel<EscapeMission, EscapeMissionData>
{
	public TUISlider slider;

	private EscapeMission data;

	public override void Initialize(EscapeMission data)
	{
		this.data = data;
		slider.sliderValue = data.GetProcess();
	}

	private void LateUpdate()
	{
		slider.sliderValue = data.GetProcess();
	}
}
