using UnityEngine;

public class SalivaTrigger : MonoBehaviour
{
	public Animation anim;

	public AnimationClip clip;

	public int frame;

	public GameObject handler;

	public string functionName;

	private void Awake()
	{
		AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[clip.name];
		animationTriggerEvent.obj = handler;
		animationTriggerEvent.time = 1f / clip.frameRate * (float)frame;
		animationTriggerEvent.functionName = functionName;
		animationTriggerEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
		animationTriggerEvent.AddToClip();
	}
}
