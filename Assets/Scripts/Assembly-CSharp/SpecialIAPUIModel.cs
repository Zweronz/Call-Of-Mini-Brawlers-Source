using System;
using Event;
using UnityEngine;

public class SpecialIAPUIModel : MonoBehaviour
{
	public int time;

	public TUILabel timer1;

	public TUILabel timer2;

	public SpecialIAPPanel panel;

	public SpecialIAPBtn btn;

	public TUILabel goldLabel;

	public TUILabel tcyLabel;

	public TUILabel prevLabel;

	public TUILabel nowLabel;

	public TUILabel offlabel;

	public SpecialIAPCrystalLabel cLabel;

	public GameObject loading;

	private GameObject load;

	private TUIMeshSprite sprite;

	private ZombieStreetTimer.TimerData timerData;

	private ZS_AvatarInfo avatarInfo;

	private ZS_IapInfo iapInfo;

	public static bool isFirstShow;

	private bool isStarted;

	private bool isPaused;

	private bool isWaked;

	public static int currentNotifyDay = -1;

	private void Awake()
	{
		EventCenter.Instance.Register<ZS_PublishSpecialIAPResultEvent>(HandleZS_PublishSpecialIAPResultEvent);
	}

	private void Start()
	{
		InitUI();
	}

	private void InitUI()
	{
		if (Player.Instance.SpecialIAPTime > 0)
		{
			DateTime dateTime = new DateTime(Player.Instance.SpecialIAPTime);
			if (DateTime.Now.CompareTo(dateTime) < 0)
			{
				Player.Instance.SpecialIAPTime = DateTime.Now.Ticks;
				currentNotifyDay = Player.Instance.CurrentSpecialIAP;
			}
			else if ((DateTime.Now - dateTime).TotalSeconds >= (double)time)
			{
				currentNotifyDay = -1;
			}
			else
			{
				currentNotifyDay = Player.Instance.CurrentSpecialIAP;
			}
		}
		if (currentNotifyDay == -1)
		{
			if (!Player.Instance.ActiveSpecialIAPShowed && Player.Instance.IsActivePlayer)
			{
				currentNotifyDay = 0;
				isFirstShow = true;
			}
			else if (LocalNotification.notifyDay > 0)
			{
				currentNotifyDay = LocalNotification.notifyDay;
				isFirstShow = true;
			}
		}
		if (currentNotifyDay >= 0)
		{
			EventCenter.Instance.Publish(this, new ZS_PublishSpecialIAPEvent(GetSpecialIap, currentNotifyDay));
			SetView(iapInfo);
		}
		if (currentNotifyDay >= 0)
		{
			if (isFirstShow)
			{
				btn.Hide();
				panel.Show();
				Player.Instance.CurrentSpecialIAP = currentNotifyDay;
				Player.Instance.SpecialIAPTime = DateTime.Now.Ticks;
				isFirstShow = false;
			}
			else
			{
				panel.Hide();
				btn.Show();
			}
			isStarted = true;
		}
		else
		{
			btn.Hide();
			panel.Hide();
		}
	}

	private void OnHandlePurchaseBtn(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3 && currentNotifyDay >= 0)
		{
			load = UnityEngine.Object.Instantiate(loading) as GameObject;
			load.transform.parent = base.transform;
			load.transform.localPosition = new Vector3(load.transform.localPosition.x, load.transform.localPosition.y, -10f);
			load.SetActiveRecursively(true);
			timerData = new ZombieStreetTimer.TimerData();
			timerData.time = 0.15f;
			timerData.invokeTimes = -1;
			timerData.data = 0;
			timerData.ingoreTimeScale = true;
			timerData.handler = LoadingPng;
			ZombieStreetTimer.Instance.AddTimer(timerData);
			if (iapInfo != null)
			{
				iapInfo.buyCallBack(iapInfo);
			}
			isPaused = true;
		}
	}

	private void OnHandleCancelBtn(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			panel.Hide();
			btn.Show();
		}
	}

	private void OnHandleSpeicalIAPBtn(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			btn.Hide();
			panel.Show();
		}
	}

	private void Update()
	{
		if (isStarted && !isPaused && currentNotifyDay >= 0)
		{
			TimeSpan timeSpan = DateTime.Now - new DateTime(Player.Instance.SpecialIAPTime);
			if (timeSpan.TotalSeconds > (double)time)
			{
				if (currentNotifyDay == 0)
				{
					Player.Instance.ActiveSpecialIAPShowed = true;
				}
				btn.Hide();
				panel.Hide();
				Player.Instance.SpecialIAPTime = 0L;
				Player.Instance.Save(true);
				isStarted = false;
				currentNotifyDay = -1;
			}
			else if (timeSpan.TotalSeconds >= 0.0)
			{
				SetTime((int)((double)time - timeSpan.TotalSeconds));
			}
			else
			{
				btn.Hide();
				panel.Hide();
				isStarted = false;
			}
		}
		if (isWaked)
		{
			if (!isStarted)
			{
				InitUI();
			}
			isWaked = false;
		}
	}

	private void SetTime(int t)
	{
		string text = ZombieStreetCommon.Time2Str(t * 1000);
		timer1.Text = text;
		timer2.Text = text;
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

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<ZS_PublishSpecialIAPResultEvent>(HandleZS_PublishSpecialIAPResultEvent);
	}

	private void HandleZS_PublishSpecialIAPResultEvent(object sender, ZS_PublishSpecialIAPResultEvent evt)
	{
		int iapEventResult = evt.IapEventResult;
		if (iapEventResult != -1 && iapEventResult == 0)
		{
			EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(getCurrentAvatar));
			goldLabel.Text = ZS_TUIMisc.FormatToString(avatarInfo.Money.Gold);
			tcyLabel.Text = ZS_TUIMisc.FormatToString(avatarInfo.Money.Tcystal);
			if (currentNotifyDay == 0)
			{
				Player.Instance.ActiveSpecialIAPShowed = true;
			}
			Player.Instance.SpecialIAPTime = 0L;
			Player.Instance.Save(true);
			panel.Hide();
			btn.Hide();
		}
		ZombieStreetTimer.RemoveTimer(timerData);
		UnityEngine.Object.Destroy(load);
		isPaused = false;
	}

	private void getCurrentAvatar(ZS_AvatarInfo info)
	{
		avatarInfo = info;
	}

	public void GetSpecialIap(ZS_IapInfo info)
	{
		iapInfo = info;
	}

	private void SetView(ZS_IapInfo info)
	{
		if (info != null)
		{
			SpecailIAPData specailIAPData = info.data as SpecailIAPData;
			if (specailIAPData != null)
			{
				cLabel.SetCrystal(specailIAPData.crystal);
				prevLabel.Text = "$" + specailIAPData.prevPrice;
				nowLabel.Text = "$" + specailIAPData.nowPrice;
				offlabel.Text = (int)((1f - specailIAPData.nowPrice / specailIAPData.prevPrice) * 100f) + "%OFF";
			}
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			isWaked = true;
		}
	}
}
