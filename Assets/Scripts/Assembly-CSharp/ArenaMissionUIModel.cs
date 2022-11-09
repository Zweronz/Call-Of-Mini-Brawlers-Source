using UnityEngine;

public class ArenaMissionUIModel : MissionUIModel<ArenaMission, ArenaMissionData>
{
	public TUILabel label;

	private ArenaMission data;

	private Transform start;

	private Transform target;

	public override void Initialize(ArenaMission data)
	{
		this.data = data;
		start = GameLevel.FindWorldCreatorInScene().heroPoint;
		target = GameObject.FindWithTag("Hero").transform;
	}

	private void LateUpdate()
	{
		Vector3 lhs = target.position - start.position;
		int num = Mathf.FloorToInt(lhs.magnitude);
		if (Vector3.Dot(lhs, start.forward) < 0f)
		{
			num = 0;
		}
		label.Text = num + "m";
	}
}
