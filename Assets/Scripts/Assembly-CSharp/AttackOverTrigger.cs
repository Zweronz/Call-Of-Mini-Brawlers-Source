using UnityEngine;

public class AttackOverTrigger : MonoBehaviour
{
	public Animation anim;

	public AnimationClip clip;

	public GameObject handler;

	public string functionName;

	private void Awake()
	{
		AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[clip.name];
		animationTriggerEvent.obj = handler;
		animationTriggerEvent.time = anim[clip.name].length;
		animationTriggerEvent.functionName = functionName;
		animationTriggerEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
		animationTriggerEvent.AddToClip();
	}
}
