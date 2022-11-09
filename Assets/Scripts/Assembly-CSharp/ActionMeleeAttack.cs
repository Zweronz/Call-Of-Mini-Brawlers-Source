using BehaviorTree;
using UnityEngine;

public class ActionMeleeAttack : Action
{
	private GameObject obj;

	public ActionMeleeAttack(GameObject obj)
	{
		this.obj = obj;
	}

	public override Status Update()
	{
		obj.SendMessage("OnMeleeAttack", SendMessageOptions.DontRequireReceiver);
		return Status.Success;
	}
}
