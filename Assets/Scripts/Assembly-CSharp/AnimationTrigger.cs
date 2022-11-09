using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
	private void OnAnimationTrigger(string stringParameter)
	{
		string clipName;
		string functionName;
		string time;
		AnimationTriggerEvent.ToClipNameAndFunctionName(stringParameter, out clipName, out functionName, out time);
		List<AnimationTriggerEvent> @event = AnimationTriggerEventCenter.Instance.GetEvent(base.GetComponent<Animation>()[clipName].clip, stringParameter);
		foreach (AnimationTriggerEvent item in @event)
		{
			if (item.animationState == base.GetComponent<Animation>()[clipName])
			{
				item.Send();
			}
		}
	}
}
