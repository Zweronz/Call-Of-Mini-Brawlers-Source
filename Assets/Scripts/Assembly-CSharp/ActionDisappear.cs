using BehaviorTree;
using UnityEngine;

public class ActionDisappear : Action
{
	private GameObject obj;

	public ActionDisappear(GameObject obj)
	{
		this.obj = obj;
	}

	public override Status Update()
	{
		obj.SendMessage("Disappear", SendMessageOptions.DontRequireReceiver);
		return Status.Success;
	}
}
