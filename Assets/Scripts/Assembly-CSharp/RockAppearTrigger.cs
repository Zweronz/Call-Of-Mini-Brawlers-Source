using System.Collections.Generic;
using UnityEngine;

public class RockAppearTrigger : MonoBehaviour
{
	public Animation anim;

	public List<AnimationClip> clips;

	public GameObject handler;

	public string functionName;

	private void Awake()
	{
		foreach (AnimationClip clip in clips)
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
}
