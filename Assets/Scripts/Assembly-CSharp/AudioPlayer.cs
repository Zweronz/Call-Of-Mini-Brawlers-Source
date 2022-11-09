using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	public bool autoPlayWhenStart;

	public bool autoPlayWhenEnabled;

	public bool autoStopWhenDisabled;

	public ITAudioEvent audioEvent;

	private bool isStarted;

	private void Start()
	{
		if (autoPlayWhenStart)
		{
			Play();
		}
		isStarted = true;
	}

	private void OnEnable()
	{
		if (autoPlayWhenEnabled && isStarted)
		{
			Play();
		}
	}

	private void OnDisable()
	{
		if (autoStopWhenDisabled)
		{
			Stop();
		}
	}

	private void Update()
	{
	}

	private void Play()
	{
		if (null != audioEvent)
		{
			audioEvent.transform.position = base.transform.position;
			audioEvent.Trigger();
		}
	}

	private void Stop()
	{
		if (null != audioEvent)
		{
			audioEvent.Stop();
		}
	}
}
