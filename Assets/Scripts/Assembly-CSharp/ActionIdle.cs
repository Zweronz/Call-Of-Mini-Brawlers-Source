using BehaviorTree;
using UnityEngine;

public class ActionIdle : Action
{
	private GameObject obj;

	public ActionIdle(GameObject obj)
	{
		this.obj = obj;
	}

	public override Status Update()
	{
		obj.SendMessage("OnIdle", SendMessageOptions.DontRequireReceiver);
		return Status.Success;
	}
}
