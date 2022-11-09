using System.Collections.Generic;
using Event;
using UnityEngine;

public class ZS_IAPEvent : MonoBehaviour
{
	public ZS_IapInfo iapInfo;

	public List<ZS_IapInfo> iapList;

	public List<ZS_IapGoldInfo> iapGoldList;

	public GameObject loading;

	public ZS_NotEnoughMoney noIap;

	public ZS_IAPBindInfo[] bindIapArray;

	public ZS_IAPBindGoldInfo[] bindGoldArray;

	public TUILabel goldLabel;

	public TUILabel tcyLabel;

	private GameObject load;

	private TUIMeshSprite sprite;

	private ZS_AvatarInfo avatarInfo;

	private ZombieStreetTimer.TimerData timerData;

	public void getCurrentAvatar(ZS_AvatarInfo info)
	{
		avatarInfo = info;
	}

	public void GetAllIapList(List<ZS_IapInfo> list)
	{
		iapList = list;
	}

	public void GetAllIapGoldList(List<ZS_IapGoldInfo> list)
	{
		iapGoldList = list;
	}

	private void Start()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishIAPEvent(GetAllIapList));
		EventCenter.Instance.Register<ZS_PublishIAPResultEvent>(HandleZS_PublishIAPResultEvent);
		BindIap();
	}

	private void BindIap()
	{
		for (int i = 0; i < iapList.Count && i != bindIapArray.Length; i++)
		{
			bindIapArray[i].iapInfo = iapList[i];
			if (null != bindIapArray[i].soldOut)
			{
				if (iapList[i].leftPackage > 0)
				{
					bindIapArray[i].soldOut.gameObject.active = false;
					continue;
				}
				bindIapArray[i].soldOut.gameObject.active = true;
				bindIapArray[i].gameObject.GetComponent<TUIButtonClick>().enabled = false;
			}
		}
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<ZS_PublishIAPResultEvent>(HandleZS_PublishIAPResultEvent);
	}

	private void BuyEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType != 3)
		{
			return;
		}
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
		if (null == load)
		{
			load = Object.Instantiate(loading) as GameObject;
			load.transform.parent = base.transform;
			load.transform.localPosition = new Vector3(load.transform.localPosition.x, load.transform.localPosition.y, -55f);
			load.SetActiveRecursively(true);
			timerData = new ZombieStreetTimer.TimerData();
			timerData.time = 0.15f;
			timerData.invokeTimes = -1;
			timerData.data = 0;
			timerData.ingoreTimeScale = true;
			timerData.handler = LoadingPng;
			ZombieStreetTimer.Instance.AddTimer(timerData);
			ZS_IAPBindInfo component = control.transform.GetComponent<ZS_IAPBindInfo>();
			ZS_IapInfo zS_IapInfo = component.iapInfo;
			if (zS_IapInfo.buyCallBack(zS_IapInfo) != 0)
			{
			}
		}
	}

	private void BuyGoldEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType != 3)
		{
			return;
		}
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
		if (null == load)
		{
			load = Object.Instantiate(loading) as GameObject;
			load.transform.parent = base.transform;
			load.transform.localPosition = new Vector3(load.transform.localPosition.x, load.transform.localPosition.y, -35f);
			load.SetActiveRecursively(true);
			timerData = new ZombieStreetTimer.TimerData();
			timerData.time = 0.15f;
			timerData.invokeTimes = -1;
			timerData.data = 0;
			timerData.ingoreTimeScale = true;
			timerData.handler = LoadingPng;
			ZombieStreetTimer.Instance.AddTimer(timerData);
			ZS_IAPBindGoldInfo component = control.GetComponent<ZS_IAPBindGoldInfo>();
			ZS_IapGoldInfo iapGoldInfo = component.iapGoldInfo;
			int num = iapGoldInfo.buyCallBack(iapGoldInfo);
			TUIMeshSprite componentInChildren = load.GetComponentInChildren<TUIMeshSprite>();
			switch (num)
			{
			case 0:
				EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(getCurrentAvatar));
				goldLabel.Text = ZS_TUIMisc.FormatToString(avatarInfo.Money.Gold);
				tcyLabel.Text = ZS_TUIMisc.FormatToString(avatarInfo.Money.Tcystal);
				break;
			case 2:
				showNotEnougtCrystal("Text033");
				break;
			}
			ZombieStreetTimer.RemoveTimer(timerData);
			Object.Destroy(load);
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
		int iapEventResult = evt.IapEventResult;
		if (iapEventResult != -1 && iapEventResult == 0)
		{
			EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(getCurrentAvatar));
			goldLabel.Text = ZS_TUIMisc.FormatToString(avatarInfo.Money.Gold);
			tcyLabel.Text = ZS_TUIMisc.FormatToString(avatarInfo.Money.Tcystal);
			EventCenter.Instance.Publish(this, new ZS_PublishIAPEvent(GetAllIapList));
			BindIap();
		}
		ZombieStreetTimer.RemoveTimer(timerData);
		Object.Destroy(load);
	}

	private void showNotEnougtCrystal(string id)
	{
		noIap.contentLab.TextID = id;
		noIap.transform.localScale = noIap.transform.localScale * 100000f;
	}
}
