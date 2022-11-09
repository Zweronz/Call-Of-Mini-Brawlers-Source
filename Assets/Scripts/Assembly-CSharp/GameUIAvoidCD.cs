using Event;
using UnityEngine;

public class GameUIAvoidCD : MonoBehaviour
{
	[SerializeField]
	protected TUIBlock block;

	[SerializeField]
	protected TUISlider slider;

	private float time;

	private float timer;

	private ZombieStreetTimer.TimerData timerData;

	private bool updateCD;

	public void StartCD(float time)
	{
		if (time > 0f)
		{
			this.time = time;
			timer = 0f;
			block.m_bEnable = true;
			timerData = new ZombieStreetTimer.TimerData();
			timerData.time = time;
			timerData.handler = TimerHandler;
			ZombieStreetTimer.Instance.AddTimer(timerData);
			slider.gameObject.SetActiveRecursively(true);
			updateCD = true;
		}
		else
		{
			EndCD();
		}
	}

	public void EndCD()
	{
		if (timerData != null)
		{
			ZombieStreetTimer.RemoveTimer(timerData);
			timerData = null;
		}
		timer = 0f;
		updateCD = false;
		block.m_bEnable = false;
		slider.gameObject.SetActiveRecursively(false);
		EventCenter.Instance.Publish(this, new AvoidCDOverEvent());
	}

	private void TimerHandler(ZombieStreetTimer.TimerData data)
	{
		EndCD();
	}

	private void Start()
	{
		EndCD();
	}

	private void OnDestroy()
	{
		if (timerData != null)
		{
			ZombieStreetTimer.RemoveTimer(timerData);
		}
	}

	private void Update()
	{
		if (updateCD)
		{
			timer += Time.deltaTime;
			if (timer >= time)
			{
				timer = time;
			}
			slider.sliderValue = 1f - timer / time;
		}
	}
}
