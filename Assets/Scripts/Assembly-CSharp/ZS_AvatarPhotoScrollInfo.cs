using System;
using System.Collections.Generic;
using Event;
using UnityEngine;

public class ZS_AvatarPhotoScrollInfo : MonoBehaviour
{
	public TUIScrollList scrollList;

	public TUIControl control;

	public TUILabel descLab;

	public TUILabel hpLab;

	public TUILabel specialLab;

	public ZS_TopInfomation topInfo;

	public TUIClipBinder clipBinder;

	public TUIRect clipBinderRect;

	private List<ZS_AvatarPhotoInfo> list;

	private string lockImg = "suo";

	public ZS_AvatarInfo aif;

	public ZS_AvatarGroupBtnShow groupBtn;

	public GameObject NoGoldBox;

	public GameObject NoCrystalBox;

	private Func<ZS_AvatarPhotoInfo, int> buyAvatarPhotoEvent;

	private Func<ZS_AvatarPhotoInfo, int> useAvatarPhotoEvent;

	private void showNotEnougtGold(params string[] id)
	{
		ZS_NotEnoughMoney component = NoGoldBox.GetComponent<ZS_NotEnoughMoney>();
		component.titleLab.TextID = id[0];
		component.contentLab.SetFormatText(id[1], "abc", "deg");
		component.mapShadow.transform.localScale = component.mapShadow.transform.localScale * 100000f;
		NoGoldBox.transform.localScale = NoGoldBox.transform.localScale * 100000f;
	}

	private void showNotEnougtCrystal(string id)
	{
		ZS_NotEnoughMoney component = NoCrystalBox.GetComponent<ZS_NotEnoughMoney>();
		component.contentLab.TextID = id;
		component.mapShadow.transform.localScale = component.mapShadow.transform.localScale * 100000f;
		NoCrystalBox.transform.localScale = NoCrystalBox.transform.localScale * 100000f;
	}

	private void Start()
	{
		InitialHeroPhotoInfo();
		SetScrollListInfo();
	}

	private void InitialHeroPhotoInfo()
	{
		aif = ZS_TopInfomation.avatar;
		EventCenter.Instance.Publish(this, new ZS_PublishAllAvatarPhotoInfoEvent(GetAvatarPhotoList));
	}

	private void GetAvatarPhotoList(List<ZS_AvatarPhotoInfo> photoList)
	{
		list = photoList;
	}

	private void GetCurrentAvatar(ZS_AvatarInfo info)
	{
		ZS_AvatarPhotoEventProcess.currentAvatar = info;
		ZS_TopInfomation.avatar = info;
		aif = info;
	}

	private int Compare(TUIButtonSelect t1, TUIButtonSelect t2)
	{
		if (t1.index > t2.index)
		{
			return 1;
		}
		if (t1.index < t2.index)
		{
			return -1;
		}
		return 0;
	}

	private void SetScrollListInfo()
	{
		if (!(null != scrollList))
		{
			return;
		}
		scrollList.Clear(true);
		int num = 0;
		int num2 = 0;
		int count = list.Count;
		int num3 = count % 3;
		num2 = ((num3 != 0) ? (count / 3 + 1) : (count / 3));
		for (int i = 0; i < num2; i++)
		{
			TUIControl tUIControl = UnityEngine.Object.Instantiate(control) as TUIControl;
			TUIButtonSelect[] componentsInChildren = tUIControl.GetComponentsInChildren<TUIButtonSelect>();
			Array.Sort(componentsInChildren, Compare);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				if (num3 != 0 && i == num2 - 1)
				{
					if (num3 == 2 && j == componentsInChildren.Length - 1)
					{
						componentsInChildren[componentsInChildren.Length - 1].gameObject.SetActiveRecursively(false);
						break;
					}
					if (num3 == 1 && j == componentsInChildren.Length - 2)
					{
						componentsInChildren[componentsInChildren.Length - 1].gameObject.SetActiveRecursively(false);
						componentsInChildren[componentsInChildren.Length - 2].gameObject.SetActiveRecursively(false);
						break;
					}
				}
				ZS_AvatarBtnShowInfo component = componentsInChildren[j].GetComponent<ZS_AvatarBtnShowInfo>();
				ZS_EquipShowPrice componentInChildren = component.GetComponentInChildren<ZS_EquipShowPrice>();
				ZS_AvatarPhotoInfo zS_AvatarPhotoInfo = (component.avatarPhotoInfo = list[i * 3 + j]);
				component.photoIcon.texture = zS_AvatarPhotoInfo.image;
				component.lockIcon.texture = lockImg;
				if (zS_AvatarPhotoInfo.level > 0)
				{
					if (aif.CurrentAvatarPhoto.id.Equals(zS_AvatarPhotoInfo.id))
					{
						componentsInChildren[j].SetSelected(true);
						num = i;
						hpLab.transform.parent.gameObject.SetActiveRecursively(false);
						if (!string.IsNullOrEmpty(zS_AvatarPhotoInfo.specialId))
						{
							specialLab.gameObject.SetActiveRecursively(true);
							specialLab.TextID = zS_AvatarPhotoInfo.specialId;
						}
						else
						{
							specialLab.gameObject.SetActiveRecursively(false);
						}
						descLab.TextID = Convert.ToString(zS_AvatarPhotoInfo.name);
						groupBtn.unableBtn.SetActiveRecursively(true);
					}
					else
					{
						component.selectedIcon.gameObject.SetActiveRecursively(false);
						component.equipedIcon.gameObject.SetActiveRecursively(false);
					}
					component.lockIcon.gameObject.SetActiveRecursively(false);
				}
				else
				{
					componentInChildren.ShowPriceObject(zS_AvatarPhotoInfo.money);
					if (!zS_AvatarPhotoInfo.isLock)
					{
						component.lockIcon.gameObject.SetActiveRecursively(false);
					}
					component.selectedIcon.gameObject.SetActiveRecursively(false);
					component.equipedIcon.gameObject.SetActiveRecursively(false);
				}
			}
			scrollList.Add(tUIControl);
		}
		float num4 = 0f;
		int num5 = ((num3 != 0) ? (count / 3 + 1) : (count / 3));
		num4 = ((3 * (num + 1) < count) ? Mathf.Clamp01(Mathf.Floor(num) / (float)num5) : Mathf.Clamp01(Mathf.Floor(num + 1) / (float)num5));
		scrollList.ScrollListTo(num4);
		clipBinder.SetClipRect(clipBinderRect);
	}

	private void BuyAvatarPhoto(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType != 3)
		{
			return;
		}
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_buy);
		ZS_AvatarPhotoBuyBtnBind component = groupBtn.buyBtn.GetComponent<ZS_AvatarPhotoBuyBtnBind>();
		ZS_AvatarPhotoInfo bindAvatarPhoto = component.bindAvatarPhoto;
		if (bindAvatarPhoto != null)
		{
			switch (NotifyBuyAvatarEvent(bindAvatarPhoto))
			{
			case 0:
				BuyAvatarSuccessDone(bindAvatarPhoto);
				UseAvatarPhoto(control, eventType, wparam, lparam, data);
				break;
			case 1:
				ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
				showNotEnougtCrystal("Text032");
				break;
			case 2:
				ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
				showNotEnougtCrystal("Text033");
				break;
			}
		}
	}

	private void UseAvatarPhoto(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_AvatarPhotoUseBtnBind component = groupBtn.useBtn.GetComponent<ZS_AvatarPhotoUseBtnBind>();
			ZS_AvatarPhotoInfo bindAvatarPhoto = component.bindAvatarPhoto;
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_hero_use);
			if (bindAvatarPhoto != null && NotifyUseAvatarEvent(bindAvatarPhoto) == 0)
			{
				UseAvatarSuccessDone(bindAvatarPhoto);
			}
		}
	}

	private void BindAvatarInfo(ZS_AvatarPhotoInfo oldInfo)
	{
		if (list == null)
		{
			return;
		}
		TUIButtonSelect[] componentsInChildren = groupBtn.transform.GetComponentsInChildren<TUIButtonSelect>();
		foreach (ZS_AvatarPhotoInfo item in list)
		{
			if (!item.id.Equals(oldInfo.id))
			{
				continue;
			}
			TUIButtonSelect[] array = componentsInChildren;
			foreach (TUIButtonSelect tUIButtonSelect in array)
			{
				if (tUIButtonSelect.IsSelected())
				{
					ZS_AvatarBtnShowInfo component = tUIButtonSelect.transform.GetComponent<ZS_AvatarBtnShowInfo>();
					ZS_AvatarPhotoUseBtnBind component2 = groupBtn.useBtn.GetComponent<ZS_AvatarPhotoUseBtnBind>();
					ZS_AvatarPhotoScrollInfo component3 = groupBtn.transform.parent.parent.GetComponent<ZS_AvatarPhotoScrollInfo>();
					component2.bindAvatarPhoto = item;
					component.avatarPhotoInfo = item;
					ZS_EquipShowPrice componentInChildren = component.GetComponentInChildren<ZS_EquipShowPrice>();
					if (item.level > 0)
					{
						componentInChildren.HidePriceObject();
					}
					component3.SetUseAvatarPhotoEvent(item.UseAvatarCallBack);
				}
			}
		}
	}

	private void FreshTopInfomation()
	{
		topInfo.levLabel.Text = Convert.ToString(aif.CurrentAvatarPhoto.level);
		topInfo.goldLabel.Text = ZS_TUIMisc.FormatToString(aif.Money.Gold);
		topInfo.tcyLabel.Text = ZS_TUIMisc.FormatToString(aif.Money.Tcystal);
	}

	private void BuyAvatarSuccessDone(ZS_AvatarPhotoInfo info)
	{
		groupBtn.HideAvatarPanelBtn();
		groupBtn.useBtn.gameObject.SetActiveRecursively(true);
		groupBtn.useBtn.m_PressObj.SetActiveRecursively(false);
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvatar));
		EventCenter.Instance.Publish(this, new ZS_PublishAllAvatarPhotoInfoEvent(GetAvatarPhotoList));
		BindAvatarInfo(info);
		FreshTopInfomation();
	}

	private void UseAvatarSuccessDone(ZS_AvatarPhotoInfo info)
	{
		groupBtn.HideAvatarPanelBtn();
		groupBtn.unableBtn.gameObject.SetActiveRecursively(true);
		ZS_AvatarBtnShowInfo[] componentsInChildren = groupBtn.GetComponentsInChildren<ZS_AvatarBtnShowInfo>();
		ZS_AvatarBtnShowInfo[] array = componentsInChildren;
		foreach (ZS_AvatarBtnShowInfo zS_AvatarBtnShowInfo in array)
		{
			if (zS_AvatarBtnShowInfo.avatarPhotoInfo.id.Equals(info.id))
			{
				zS_AvatarBtnShowInfo.equipedIcon.gameObject.SetActiveRecursively(true);
			}
			else
			{
				zS_AvatarBtnShowInfo.equipedIcon.gameObject.SetActiveRecursively(false);
			}
		}
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvatar));
	}

	public void SetBuyAvatarPhotoEvent(Func<ZS_AvatarPhotoInfo, int> handle)
	{
		buyAvatarPhotoEvent = handle;
	}

	public void ClearBuyAvatarPhotoEvent()
	{
		buyAvatarPhotoEvent = null;
	}

	public void SetUseAvatarPhotoEvent(Func<ZS_AvatarPhotoInfo, int> handle)
	{
		useAvatarPhotoEvent = handle;
	}

	public void ClearUseAvatarPhotoEvent()
	{
		useAvatarPhotoEvent = null;
	}

	private int NotifyBuyAvatarEvent(ZS_AvatarPhotoInfo info)
	{
		if (buyAvatarPhotoEvent != null)
		{
			return buyAvatarPhotoEvent(info);
		}
		return 3;
	}

	private int NotifyUseAvatarEvent(ZS_AvatarPhotoInfo info)
	{
		if (useAvatarPhotoEvent != null)
		{
			return useAvatarPhotoEvent(info);
		}
		return 3;
	}
}
