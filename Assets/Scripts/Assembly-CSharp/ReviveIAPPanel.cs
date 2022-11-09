using Event;
using UnityEngine;

public class ReviveIAPPanel : MonoBehaviour
{
	public GameObject loading;

	private GameObject load;

	private ZombieStreetTimer.TimerData timerData;

	private TUIMeshSprite sprite;

	private ReviveIAPData data;

	private RevivePanel revivePanel;

	public Vector3 showDis;

	public Vector3 disapearDis;

	private void Awake()
	{
		EventCenter.Instance.Register<ZS_PublishIAPResultEvent>(HandleZS_PublishIAPResultEvent);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<ZS_PublishIAPResultEvent>(HandleZS_PublishIAPResultEvent);
	}

	public void Init(ReviveIAPData data, RevivePanel revivePanel)
	{
		this.data = data;
		this.revivePanel = revivePanel;
	}

	public void Show()
	{
		base.transform.position = showDis;
	}

	public void Hide()
	{
		base.transform.position = disapearDis;
	}

	private void HandleYesBtn(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3 && this.data != null)
		{
			load = Object.Instantiate(loading) as GameObject;
			load.transform.parent = base.transform;
			load.transform.localPosition = new Vector3(load.transform.localPosition.x, load.transform.localPosition.y, -50f);
			load.SetActiveRecursively(true);
			timerData = new ZombieStreetTimer.TimerData();
			timerData.time = 0.15f;
			timerData.invokeTimes = -1;
			timerData.data = 0;
			timerData.ingoreTimeScale = true;
			timerData.handler = LoadingPng;
			ZombieStreetTimer.Instance.AddTimer(timerData);
			this.data.iapEvent.buyCallBack(this.data.iapEvent);
		}
	}

	private void HandleNoBtn(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			revivePanel.Resume();
			Hide();
		}
	}

	private void LoadingPng(ZombieStreetTimer.TimerData timerData)
	{
		string[] loadingImgs = ZS_TUIMisc.loadingImgs;
		int num = loadingImgs.Length;
		int num2 = (int)timerData.data;
		sprite = load.GetComponentInChildren<TUIMeshSprite>();
		sprite.texture = loadingImgs[num2];
		num2 = ((num2 < num - 1) ? (num2 + 1) : 0);
		timerData.data = num2;
	}

	private void HandleZS_PublishIAPResultEvent(object sender, ZS_PublishIAPResultEvent evt)
	{
		switch (evt.IapEventResult)
		{
		case 0:
			if (!data.reviveAction(revivePanel.Crystal))
			{
				revivePanel.Resume();
			}
			break;
		case -1:
			revivePanel.Resume();
			break;
		default:
			revivePanel.Resume();
			break;
		}
		Hide();
		ZombieStreetTimer.RemoveTimer(timerData);
		Object.Destroy(load);
	}
}
