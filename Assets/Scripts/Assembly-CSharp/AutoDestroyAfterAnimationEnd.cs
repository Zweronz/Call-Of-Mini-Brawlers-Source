using System;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAfterAnimationEnd : MonoBehaviour
{
	[Serializable]
	public class Data
	{
		public AnimationClip clip;

		public float afterTime = 2f;
	}

	public List<Data> datas;

	public GameObject destroyObj;

	private float afterTime;

	private bool beginWaitting;

	private float timer;

	private void Awake()
	{
		foreach (Data data in datas)
		{
			AnimationClip clip = data.clip;
			if (null != clip)
			{
				AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
				animationTriggerEvent.animationState = base.GetComponent<Animation>()[clip.name];
				animationTriggerEvent.obj = base.gameObject;
				animationTriggerEvent.time = base.GetComponent<Animation>()[clip.name].length;
				animationTriggerEvent.functionName = "OnDeadAnimationEnd";
				animationTriggerEvent.data = data.afterTime;
				animationTriggerEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
				animationTriggerEvent.AddToClip();
			}
			else
			{
				OnDeadAnimationEnd(data.afterTime);
			}
		}
	}

	private void Update()
	{
		if (!beginWaitting)
		{
			return;
		}
		timer += Time.deltaTime;
		if (timer > afterTime)
		{
			if (null != destroyObj)
			{
				UnityEngine.Object.DestroyImmediate(destroyObj);
			}
			beginWaitting = false;
		}
	}

	private void OnDeadAnimationEnd(float afterTime)
	{
		this.afterTime = afterTime;
		beginWaitting = true;
	}
}
