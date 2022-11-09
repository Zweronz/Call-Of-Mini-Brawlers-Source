using BehaviorTree;
using UnityEngine;

public class ActionMove : Action
{
	private GameObject obj;

	public ActionMove(GameObject obj)
	{
		this.obj = obj;
	}

	public override Status Update()
	{
		obj.SendMessage("OnMove", SendMessageOptions.DontRequireReceiver);
		return Status.Success;
	}
}
