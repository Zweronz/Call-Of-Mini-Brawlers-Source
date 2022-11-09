using BehaviorTree;
using UnityEngine;

public class ConditionHaveTarget : Condition
{
	private GameObject obj;

	public ConditionHaveTarget(GameObject obj)
	{
		this.obj = obj;
	}

	public override Status Update()
	{
		Target component = obj.GetComponent<Target>();
		if (null != component.target)
		{
			return Status.Success;
		}
		return Status.Failure;
	}
}
