using BehaviorTree;
using UnityEngine;

public class ActionFaceto : Action
{
	public GameObject obj;

	public ActionFaceto(GameObject obj)
	{
		this.obj = obj;
	}

	public override Status Update()
	{
		Target component = obj.GetComponent<Target>();
		obj.SendMessage("OnFaceTo", component.target.transform, SendMessageOptions.DontRequireReceiver);
		return Status.Success;
	}
}
