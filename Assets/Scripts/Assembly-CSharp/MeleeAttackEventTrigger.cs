using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackEventTrigger : MonoBehaviour
{
	[Serializable]
	public class Data
	{
		public AnimationClip clip;

		public int frame;

		public GameObject lightPrefab;
	}

	public Animation anim;

	[SerializeField]
	protected List<Data> datas;

	public GameObject obj;

	public string function;

	public Transform point;

	private void Awake()
	{
		if (datas == null)
		{
			return;
		}
		foreach (Data data in datas)
		{
			AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
			animationTriggerEvent.animationState = anim[data.clip.name];
			animationTriggerEvent.time = (float)data.frame / data.clip.frameRate;
			animationTriggerEvent.obj = base.gameObject;
			animationTriggerEvent.data = this;
			animationTriggerEvent.functionName = "HandleMeleeAttackEvent";
			animationTriggerEvent.AddToClip();
			animationTriggerEvent = new AnimationTriggerEvent();
			animationTriggerEvent.animationState = anim[data.clip.name];
			animationTriggerEvent.time = 0f;
			animationTriggerEvent.obj = base.gameObject;
			animationTriggerEvent.data = data.lightPrefab;
			animationTriggerEvent.functionName = "HandleMeleeAttackBegin";
			animationTriggerEvent.AddToClip();
		}
	}

	private void HandleMeleeAttackEvent(MeleeAttackEventTrigger trigger)
	{
		if (this == trigger)
		{
			obj.SendMessage(function);
		}
	}

	private void HandleMeleeAttackBegin(GameObject lightPrefab)
	{
		if (null != lightPrefab)
		{
			lightPrefab.GetComponent<Animation>().Play();
		}
	}
}
