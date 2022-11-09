using UnityEngine;

public class SlaughterMissionUIModel : MissionUIModel<SlaughterMission, SlaughterMissionData>
{
	public TUIMeshSprite icon;

	public TUILabel label;

	private SlaughterMission data;

	public override void Initialize(SlaughterMission data)
	{
		this.data = data;
		if (data.Icon == "shaguaimoshi")
		{
			icon.texture = "guai";
		}
		else
		{
			icon.texture = "xiaochou";
		}
		SetIconLabelPos();
		label.Text = data.GetKilledEnemySum() + "/" + data.GetEnemySum();
	}

	private void LateUpdate()
	{
		label.Text = data.GetKilledEnemySum() + "/" + data.GetEnemySum();
	}

	private void SetIconLabelPos()
	{
		string str = data.GetEnemySum() + "/" + data.GetEnemySum();
		Bounds bounds = label.CalculateBounds(str);
		float num = icon.texInfo.rect.width / 2f;
		if (TUI.IsRetina())
		{
			num /= 2f;
		}
		Vector3 position = icon.transform.position;
		position.x = label.transform.TransformPoint(bounds.min.x, 0f, 0f).x - num;
		icon.transform.position = position;
	}
}
