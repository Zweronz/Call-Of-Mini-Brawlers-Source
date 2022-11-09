using UnityEngine;

public class AutoDestroyAfterDead : MonoBehaviour
{
	public AnimationClip clip;

	public GameObject destroyObj;

	public float afterTime = 2f;

	private bool beginWaitting;

	private float timer;

	private void Awake()
	{
		if (null != clip)
		{
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.time = clip.length;
			animationEvent.functionName = "OnDeadAnimationEnd";
			animationEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
			clip.AddEvent(animationEvent);
		}
		else
		{
			OnDeadAnimationEnd();
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
				Object.DestroyImmediate(destroyObj);
			}
			beginWaitting = false;
		}
	}

	private void OnDeadAnimationEnd()
	{
		beginWaitting = true;
	}
}
