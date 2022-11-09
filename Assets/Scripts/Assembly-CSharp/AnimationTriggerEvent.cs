using System;
using UnityEngine;

public class AnimationTriggerEvent
{
	private AnimationEvent evt;

	public GameObject obj;

	private static string linkString = "!_@_!";

	public AnimationState animationState { get; set; }

	public string functionName { get; set; }

	public SendMessageOptions messageOptions { get; set; }

	public object data { get; set; }

	public float time { get; set; }

	public string eventString { get; private set; }

	private AnimationEvent ToAnimationEvent()
	{
		if (evt == null)
		{
			evt = new AnimationEvent();
		}
		evt.functionName = "OnAnimationTrigger";
		evt.messageOptions = messageOptions;
		eventString = ToStringParameter(animationState.clip.name, functionName, time.ToString());
		evt.stringParameter = eventString;
		evt.time = time;
		return evt;
	}

	public void AddToClip(bool repeatable = false)
	{
		ToAnimationEvent();
		if (!AnimationTriggerEventCenter.Instance.ExistEventStringInClip(animationState.clip, eventString))
		{
			animationState.clip.AddEvent(evt);
		}
		AnimationTriggerEventCenter.Instance.AddAnimationTriggerEvent(this, repeatable);
	}

	public void Send()
	{
		if (null != obj && !string.IsNullOrEmpty(functionName))
		{
			if (data != null)
			{
				obj.SendMessage(functionName, data, messageOptions);
			}
			else
			{
				obj.SendMessage(functionName, messageOptions);
			}
		}
	}

	public static string ToStringParameter(string clipName, string functionName, string time)
	{
		return clipName + linkString + functionName + linkString + time;
	}

	public static void ToClipNameAndFunctionName(string stringParameter, out string clipName, out string functionName, out string time)
	{
		string[] array = stringParameter.Split(new string[1] { linkString }, StringSplitOptions.RemoveEmptyEntries);
		clipName = array[0];
		functionName = array[1];
		time = array[2];
	}

	public static bool Compare(AnimationTriggerEvent evt1, AnimationTriggerEvent evt2, bool includeData)
	{
		if (includeData)
		{
			return evt1.animationState == evt2.animationState && evt1.functionName == evt2.functionName && evt1.time == evt2.time && evt1.obj == evt2.obj && evt1.data == evt2.data;
		}
		return evt1.animationState == evt2.animationState && evt1.functionName == evt2.functionName && evt1.time == evt2.time && evt1.obj == evt2.obj;
	}

	public static bool Compare(AnimationTriggerEvent evt1, AnimationTriggerEvent evt2)
	{
		return ToStringParameter(evt1.animationState.clip.name, evt1.functionName, evt1.time.ToString()).Equals(ToStringParameter(evt2.animationState.clip.name, evt2.functionName, evt2.time.ToString()));
	}
}
