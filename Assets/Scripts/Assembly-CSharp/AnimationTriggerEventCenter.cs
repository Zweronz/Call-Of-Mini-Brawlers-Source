using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerEventCenter
{
	private static AnimationTriggerEventCenter instance;

	private Dictionary<AnimationClip, Dictionary<string, List<AnimationTriggerEvent>>> events = new Dictionary<AnimationClip, Dictionary<string, List<AnimationTriggerEvent>>>();

	private List<AnimationTriggerEvent> temp = new List<AnimationTriggerEvent>();

	public static AnimationTriggerEventCenter Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new AnimationTriggerEventCenter();
			}
			return instance;
		}
	}

	public void AddAnimationTriggerEvent(AnimationTriggerEvent evt, bool repeatable = false)
	{
		if (!events.ContainsKey(evt.animationState.clip))
		{
			events.Add(evt.animationState.clip, null);
		}
		if (events[evt.animationState.clip] == null)
		{
			events[evt.animationState.clip] = new Dictionary<string, List<AnimationTriggerEvent>>();
		}
		if (!events[evt.animationState.clip].ContainsKey(evt.eventString))
		{
			events[evt.animationState.clip].Add(evt.eventString, null);
		}
		if (events[evt.animationState.clip][evt.eventString] == null)
		{
			events[evt.animationState.clip][evt.eventString] = new List<AnimationTriggerEvent>();
		}
		bool flag = true;
		if (repeatable)
		{
			flag = true;
		}
		else
		{
			List<AnimationTriggerEvent> @event = GetEvent(evt.animationState.clip, evt.eventString);
			if (@event == null || @event.Count == 0)
			{
				flag = true;
			}
			else
			{
				flag = true;
				foreach (AnimationTriggerEvent item in @event)
				{
					if (AnimationTriggerEvent.Compare(item, evt, true))
					{
						flag = false;
						break;
					}
				}
			}
		}
		if (flag)
		{
			events[evt.animationState.clip][evt.eventString].Add(evt);
		}
	}

	public List<AnimationTriggerEvent> GetEvent(AnimationClip clip, string eventString)
	{
		if (events.ContainsKey(clip) && events[clip].ContainsKey(eventString))
		{
			temp.Clear();
			foreach (AnimationTriggerEvent item in events[clip][eventString])
			{
				if (null == item.animationState)
				{
					temp.Add(item);
				}
			}
			foreach (AnimationTriggerEvent item2 in temp)
			{
				events[clip][eventString].Remove(item2);
			}
			return events[clip][eventString];
		}
		return null;
	}

	public bool ExistEventStringInClip(AnimationClip clip, string eventString)
	{
		return events.ContainsKey(clip) && events[clip].ContainsKey(eventString);
	}
}
