using BehaviorTree;
using UnityEngine;

public class ActionTarget : Action
{
	private GameObject obj;

	public ActionTarget(GameObject obj)
	{
		this.obj = obj;
	}

	public override Status Update()
	{
		obj.SendMessage("OnFocus", GameObject.FindGameObjectWithTag("Hero"), SendMessageOptions.DontRequireReceiver);
		return Status.Success;
	}
}
