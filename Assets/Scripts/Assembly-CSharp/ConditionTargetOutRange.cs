using BehaviorTree;
using UnityEngine;

public class ConditionTargetOutRange : Condition
{
	private GameObject obj;

	private float radius;

	public ConditionTargetOutRange(GameObject obj, float radius)
	{
		this.obj = obj;
		this.radius = radius;
	}

	public override Status Update()
	{
		Target component = obj.GetComponent<Target>();
		if (null != component.target && !Tool.InArea(obj, component.target, radius))
		{
			return Status.Success;
		}
		return Status.Failure;
	}
}
