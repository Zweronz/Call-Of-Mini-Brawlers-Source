using System;
using System.Collections.Generic;
using UnityEngine;

public class ZS_EquipBtnItemShowInfo : MonoBehaviour
{
	public ZS_ItemInfo eInfo;

	public TUIMeshSprite msEquipped;

	public TUIMeshSprite equipIcon;

	private GameObject bsGroup;

	private List<ZS_ItemInfo> allItemList;

	private Func<ZS_ItemInfo, bool> triggerSelectEvent;

	private void Start()
	{
		SetSelectEventHandle(HandleEvent);
	}

	public void SetSelectEventHandle(Func<ZS_ItemInfo, bool> handle)
	{
		triggerSelectEvent = handle;
	}

	public void SetSelectEventHandle()
	{
		triggerSelectEvent = null;
	}

	public bool NotifySelectEvent()
	{
		if (eInfo != null)
		{
			return triggerSelectEvent(eInfo);
		}
		return false;
	}

	private void ItemList(List<ZS_ItemInfo> list)
	{
		allItemList = list;
	}

	public bool HandleEvent(ZS_ItemInfo einfo)
	{
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
		GameObject gameObject = base.transform.parent.parent.parent.parent.gameObject;
		ZS_GroupObjectBind component = gameObject.GetComponent<ZS_GroupObjectBind>();
		component.itemTopInfo.ShowItemTopInfo(einfo.Image, einfo.Name);
		component.SetAllBtnUnVisable();
		ZS_EquipPanelMove.unwiledLocation = GetUnwieldLocation(ZS_EquipUsingItemShow.usingItemInfo, einfo.Id, ZS_EquipPanelMove.clickLocation);
		component.itemInfo = einfo;
		TUIButtonClick buyItemBtn = component.buyItemBtn;
		TUIButtonClick equipItemBtn = component.equipItemBtn;
		TUIButtonClick unwieldItemBtn = component.unwieldItemBtn;
		ZS_EquipEventBtnBindInfo component2 = buyItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
		if (einfo.canBuy)
		{
			buyItemBtn.gameObject.SetActiveRecursively(true);
			buyItemBtn.m_PressObj.SetActiveRecursively(false);
			component2.itemInfo = einfo;
			component2.SetBuyItemHandle(einfo.BuyCallBack);
			ZS_EquipShowPrice componentInChildren = buyItemBtn.GetComponentInChildren<ZS_EquipShowPrice>();
			componentInChildren.ShowPriceObject(einfo.Money);
		}
		ZS_EquipEventBtnBindInfo component3 = equipItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
		ZS_EquipEventBtnBindInfo component4 = unwieldItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
		component3.itemInfo = einfo;
		component4.itemInfo = einfo;
		component3.SetEquipItemHandle(einfo.EquipCallBack);
		component4.SetUnwieldItemHandle(einfo.UnwieldCallBack);
		component.itemDesc.TextID = einfo.Desc;
		component.itemCount.Text = "YOU OWN  " + eInfo.Count;
		if (einfo.IsUsing)
		{
			equipItemBtn.gameObject.SetActiveRecursively(false);
			if (ZS_EquipPanelMove.clickLocation == ZS_EquipPanelMove.unwiledLocation)
			{
				unwieldItemBtn.gameObject.SetActiveRecursively(true);
				unwieldItemBtn.m_PressObj.SetActiveRecursively(false);
			}
		}
		else
		{
			unwieldItemBtn.gameObject.SetActiveRecursively(false);
			if (einfo.IsOwn)
			{
				equipItemBtn.gameObject.SetActiveRecursively(true);
				equipItemBtn.m_PressObj.SetActiveRecursively(false);
			}
		}
		return true;
	}

	private int GetUnwieldLocation(List<ZS_ItemInfo> usingItemList, string id, int defaultLocation)
	{
		for (int i = 0; i < usingItemList.Count; i++)
		{
			if (usingItemList[i] != null && usingItemList[i].Id.Equals(id))
			{
				return i;
			}
		}
		return defaultLocation;
	}
}
