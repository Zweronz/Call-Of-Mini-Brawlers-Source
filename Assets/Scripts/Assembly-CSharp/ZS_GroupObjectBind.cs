using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;

public class ZS_GroupObjectBind : MonoBehaviour
{
	public TUIButtonClick buyBtn;

	public TUIButtonClick upGradeBtn;

	public TUIButtonClick equipBtn;

	public TUIButtonClick unwieldBtn;

	public TUIButtonClick buyItemBtn;

	public TUIButtonClick unwieldItemBtn;

	public TUIButtonClick equipItemBtn;

	public TUILabel equipNameLab;

	public GameObject NoGoldBox;

	public GameObject NoCrystalBox;

	public TUILabel itemCount;

	public TUILabel itemDesc;

	public GameObject equipOwn;

	public GameObject equipUnOwn;

	public TUILabel buyFailLab;

	public TUILabel equipFailLab;

	public GameObject buyFail;

	public GameObject equipFail;

	public GameObject updateFail;

	public ZS_EquipShowTopInfo ownTopInfo;

	public ZS_EquipShowTopInfo unOwnTopInfo;

	public ZS_EquipShowTopInfo itemTopInfo;

	public ZS_EquipmentInfo equipInfo;

	public ZS_ItemInfo itemInfo;

	public ZS_EquipPanelMove epMoveEvent;

	private ZS_IapToGold iapToGoldInfo;

	private ZS_AvatarInfo avatarInfo;

	private List<ZS_ItemInfo> allItemList;

	private ZS_EquipmentInfo[] equipArray;

	public TUILabel sliderOAmmoLab;

	public TUILabel sliderOAttackLab;

	public TUILabel sliderOChanceLab;

	public TUILabel sliderOHitLab;

	public TUILabel sliderOKnockLab;

	public TUILabel sliderUAmmoLab;

	public TUILabel sliderUAttackLab;

	public TUILabel sliderUChanceLab;

	public TUILabel sliderUHitLab;

	public TUILabel sliderUKnockLab;

	public TUISlider sliderOLAmmo;

	public TUISlider sliderOLAttack;

	public TUISlider sliderOLChance;

	public TUISlider sliderOLHit;

	public TUISlider sliderOLKnock;

	public TUISlider sliderOTAmmo;

	public TUISlider sliderOTAttack;

	public TUISlider sliderOTChance;

	public TUISlider sliderOTHit;

	public TUISlider sliderOTKnock;

	public TUISlider sliderULAmmo;

	public TUISlider sliderULAttack;

	public TUISlider sliderULChance;

	public TUISlider sliderULHit;

	public TUISlider sliderULKnock;

	public TUISlider sliderUTAmmo;

	public TUISlider sliderUTAttack;

	public TUISlider sliderUTChance;

	public TUISlider sliderUTHit;

	public TUISlider sliderUTKnock;

	private void showNotEnougtGold(ZS_IapToGold iapToGInfo, params string[] id)
	{
		ZS_NotEnoughMoney component = NoGoldBox.GetComponent<ZS_NotEnoughMoney>();
		component.titleLab.TextID = id[0];
		component.contentLab.SetFormatText(id[1], iapToGInfo.goldCount, iapToGoldInfo.iapCount);
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

	private void TriggerBuyEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType != 3)
		{
			return;
		}
		ZS_EquipEventBtnBindInfo component = buyBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
		switch (component.NotifyBuyClick())
		{
		case 0:
			BuySuccessDone();
			if (component.equipInfo.IsCanEquip)
			{
				TriggerEquipEvent(control, eventType, wparam, lparam, data);
			}
			break;
		case 1:
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
			showNotEnougtCrystal("Text032");
			break;
		case 2:
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
			showNotEnougtCrystal("Text033");
			break;
		case 3:
			break;
		}
	}

	private void GetIapToGoldInfo(ZS_IapToGold info)
	{
		iapToGoldInfo = info;
	}

	private void TriggerEquipEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_EquipEventBtnBindInfo component = equipBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			switch (component.NotifyEquipClick())
			{
			case 0:
				EquipSuccessDone();
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			}
		}
	}

	private void TriggerUpGradeEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_EquipEventBtnBindInfo component = upGradeBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			switch (component.NotifyUpgraedClick())
			{
			case 0:
				StartCoroutine(UpGradeSuccessDone());
				break;
			case 1:
				ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
				showNotEnougtCrystal("Text032");
				break;
			case 2:
				ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
				showNotEnougtCrystal("Text033");
				break;
			case 3:
				break;
			}
		}
	}

	private void TriggerUnwieldEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_EquipEventBtnBindInfo component = unwieldBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			switch (component.NotifyUnwieldClick())
			{
			case 0:
				UnwieldSuccessDone();
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			}
		}
	}

	private void TriggerBuyItemEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_EquipEventBtnBindInfo component = buyItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			ZS_EquipEventBtnBindInfo component2 = equipItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			switch (component.NotifyBuyItemClick())
			{
			case 0:
				BuyItemSuccessDone();
				break;
			case 1:
				showNotEnougtCrystal("Text032");
				ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
				break;
			case 2:
				showNotEnougtCrystal("Text033");
				ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
				break;
			case 3:
				break;
			}
		}
	}

	private void TriggerUnwieldItemEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_EquipEventBtnBindInfo component = unwieldItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			switch (component.NotifyUnwieldItemClick())
			{
			case 0:
				UnwieldItemSuccessDone();
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			}
		}
	}

	private void TriggerEquipItemEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_EquipEventBtnBindInfo component = equipItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			switch (component.NotifyEquipItemClick())
			{
			case 0:
				EquipItemSuccessDone();
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			}
		}
	}

	private void GetCurrentAvatar(ZS_AvatarInfo avatar)
	{
		avatarInfo = avatar;
	}

	private void GetUsingEquip(List<ZS_EquipmentInfo> list)
	{
		ZS_EquipUsingShow.usingEquipInfo = list;
	}

	private void GetAllEquipMent(params ZS_EquipmentInfo[] array)
	{
		equipArray = array;
	}

	private void FreshAvatarTopInfo(ZS_AvatarInfo avatar)
	{
		epMoveEvent.tcyLabel.Text = ZS_TUIMisc.FormatToString(avatar.Money.Tcystal);
		epMoveEvent.goldLabel.Text = ZS_TUIMisc.FormatToString(avatar.Money.Gold);
	}

	private IEnumerator ScrollSliderProcess(TUISlider slider, float start, float end, float time)
	{
		float speed = (end - start) / time;
		float dTime = Time.deltaTime;
		while (true)
		{
			start += speed * dTime;
			if (start >= end)
			{
				break;
			}
			slider.sliderValue = start;
			yield return true;
		}
		slider.sliderValue = end;
	}

	private IEnumerator ShowScrollLowAttribute(ZS_EquipmentInfo currentInfo, float time)
	{
		if (currentInfo.Ammo != currentInfo.NextAmmo)
		{
			StartCoroutine(ScrollSliderProcess(start: (float)currentInfo.Ammo / (float)currentInfo.maxAmmo, end: (float)currentInfo.NextAmmo / (float)currentInfo.maxAmmo, slider: sliderOLAmmo, time: time));
		}
		if (currentInfo.Attack != currentInfo.NextAttack)
		{
			StartCoroutine(ScrollSliderProcess(start: (float)currentInfo.Attack / (float)currentInfo.maxAttack, end: (float)currentInfo.NextAttack / (float)currentInfo.maxAttack, slider: sliderOLAttack, time: time));
		}
		if (currentInfo.CriticalChance != currentInfo.NextCriticalChance)
		{
			StartCoroutine(ScrollSliderProcess(start: currentInfo.CriticalChance / currentInfo.maxCriticalChance, end: currentInfo.NextCriticalChance / currentInfo.maxCriticalChance, slider: sliderOLChance, time: time));
		}
		if (currentInfo.CriticalHit != currentInfo.NextCriticalHit)
		{
			StartCoroutine(ScrollSliderProcess(start: currentInfo.CriticalHit / currentInfo.maxCriticalHit, end: currentInfo.NextCriticalHit / currentInfo.maxCriticalHit, slider: sliderOLHit, time: time));
		}
		if (currentInfo.KnockBack != currentInfo.NextKnockBack)
		{
			StartCoroutine(ScrollSliderProcess(start: currentInfo.KnockBack / currentInfo.maxKnockBack, end: currentInfo.NextKnockBack / currentInfo.maxKnockBack, slider: sliderOLKnock, time: time));
		}
		float waitSeconds = time + 0.5f;
		yield return new WaitForSeconds(waitSeconds);
	}

	private IEnumerator ShowScrollTopAttribute(ZS_EquipmentInfo nextInfo, float time)
	{
		if (nextInfo.Ammo != nextInfo.NextAmmo)
		{
			StartCoroutine(ScrollSliderProcess(start: (float)nextInfo.Ammo / (float)nextInfo.maxAmmo, end: (float)nextInfo.NextAmmo / (float)nextInfo.maxAmmo, slider: sliderOTAmmo, time: time));
		}
		if (nextInfo.Attack != nextInfo.NextAttack)
		{
			StartCoroutine(ScrollSliderProcess(start: (float)nextInfo.Attack / (float)nextInfo.maxAttack, end: (float)nextInfo.NextAttack / (float)nextInfo.maxAttack, slider: sliderOTAttack, time: time));
		}
		if (nextInfo.CriticalChance != nextInfo.NextCriticalChance)
		{
			StartCoroutine(ScrollSliderProcess(start: nextInfo.CriticalChance / nextInfo.maxCriticalChance, end: nextInfo.NextCriticalChance / nextInfo.maxCriticalChance, slider: sliderOTChance, time: time));
		}
		if (nextInfo.CriticalHit != nextInfo.NextCriticalHit)
		{
			StartCoroutine(ScrollSliderProcess(start: nextInfo.CriticalHit / nextInfo.maxCriticalHit, end: nextInfo.NextCriticalHit / nextInfo.maxCriticalHit, slider: sliderOTHit, time: time));
		}
		if (nextInfo.KnockBack != nextInfo.NextKnockBack)
		{
			StartCoroutine(ScrollSliderProcess(start: nextInfo.KnockBack / nextInfo.maxKnockBack, end: nextInfo.NextKnockBack / nextInfo.maxKnockBack, slider: sliderOTKnock, time: time));
		}
		float waitSeconds = time + 0.5f;
		yield return new WaitForSeconds(waitSeconds);
	}

	private void BuySuccessDone()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvatar));
		EventCenter.Instance.Publish(this, new ZS_PublishAllEquipEvent(GetAllEquipMent));
		BindSelectBtnInfoAfterBuy();
		FreshAvatarTopInfo(avatarInfo);
		equipOwn.SetActiveRecursively(true);
		equipUnOwn.SetActiveRecursively(false);
		SetAllBtnUnVisable();
		ownTopInfo.ShowEquipTopInfo(equipInfo.Image, equipInfo.Name);
		ZS_EquipBtnShowInfo.ShowEquipUpGradeInfo(this, equipInfo);
		if (equipInfo.IsOwn)
		{
			if (equipInfo.CanUpdate)
			{
				upGradeBtn.gameObject.SetActiveRecursively(true);
				upGradeBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component = upGradeBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component.equipInfo = equipInfo;
				component.SetUpGradeEventHandle(equipInfo.UpGradeCallBack);
				ZS_EquipShowPrice componentInChildren = component.GetComponentInChildren<ZS_EquipShowPrice>();
				componentInChildren.ShowPriceObject(equipInfo.UpdateMoney);
			}
			else
			{
				updateFail.SetActiveRecursively(true);
			}
			if (equipInfo.IsCanEquip)
			{
				equipBtn.gameObject.SetActiveRecursively(true);
				equipBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component2 = equipBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component2.equipInfo = equipInfo;
				component2.SetEquipEventHandle(equipInfo.EquipCallBack);
			}
			else
			{
				equipFail.SetActiveRecursively(true);
				equipFailLab.SetFormatText("Text020", equipInfo.EquipCondition);
			}
		}
	}

	private void EquipSuccessDone()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishAllEquipEvent(GetAllEquipMent));
		EventCenter.Instance.Publish(this, new ZS_PublishUsingEquipEvent(GetUsingEquip, ZS_EquipUsingShow.count));
		BindSelectBtnInfoAfterEquiped();
		SetAllBtnUnVisable();
		if (equipInfo.IsOwn)
		{
			if (equipInfo.CanUpdate)
			{
				upGradeBtn.gameObject.SetActiveRecursively(true);
				upGradeBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component = upGradeBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component.equipInfo = equipInfo;
				component.SetUpGradeEventHandle(equipInfo.UpGradeCallBack);
				ZS_EquipShowPrice componentInChildren = component.GetComponentInChildren<ZS_EquipShowPrice>();
				componentInChildren.ShowPriceObject(equipInfo.UpdateMoney);
			}
			else
			{
				updateFail.SetActiveRecursively(true);
			}
			if (equipInfo.IsEquiped && ZS_EquipPanelMove.clickLocation != 0 && ZS_EquipPanelMove.clickLocation != 1)
			{
				unwieldBtn.gameObject.SetActiveRecursively(true);
				unwieldBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component2 = unwieldBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component2.equipInfo = equipInfo;
				component2.SetUnwieldEventHandle(equipInfo.UnwieldCallBack);
			}
		}
	}

	private void UnwieldSuccessDone()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishAllEquipEvent(GetAllEquipMent));
		BindSelectBtnInfoAfterUnwield();
		SetAllBtnUnVisable();
		if (equipInfo.IsOwn)
		{
			if (equipInfo.CanUpdate)
			{
				upGradeBtn.gameObject.SetActiveRecursively(true);
				upGradeBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component = upGradeBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component.equipInfo = equipInfo;
				component.SetUpGradeEventHandle(equipInfo.UpGradeCallBack);
				ZS_EquipShowPrice componentInChildren = component.GetComponentInChildren<ZS_EquipShowPrice>();
				componentInChildren.ShowPriceObject(equipInfo.UpdateMoney);
			}
			else
			{
				updateFail.SetActiveRecursively(true);
			}
			if (equipInfo.IsCanEquip)
			{
				equipBtn.gameObject.SetActiveRecursively(true);
				equipBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component2 = equipBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component2.equipInfo = equipInfo;
				component2.SetEquipEventHandle(equipInfo.EquipCallBack);
			}
			else
			{
				equipFail.SetActiveRecursively(true);
				equipFailLab.Text = equipInfo.EquipCondition;
			}
		}
	}

	private void UpdateProcessAttribute(ZS_EquipmentInfo eInfo)
	{
		sliderOAmmoLab.Text = eInfo.Ammo.ToString();
		sliderOAttackLab.Text = eInfo.Attack.ToString();
		sliderOChanceLab.Text = eInfo.CriticalChance.ToString();
		sliderOKnockLab.Text = eInfo.KnockBack.ToString();
		sliderOHitLab.Text = eInfo.CriticalHit.ToString();
	}

	private IEnumerator UpGradeSuccessDone()
	{
		GameObject obj = new GameObject();
		TUIBlock block = obj.AddComponent<TUIBlock>();
		block.size = new Vector2(480f, 320f);
		block.m_bEnable = true;
		obj.name = "Block";
		obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -80f);
		obj.transform.parent = base.transform.parent.parent.parent;
		yield return StartCoroutine(ShowScrollLowAttribute(equipInfo, 0.25f));
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvatar));
		EventCenter.Instance.Publish(this, new ZS_PublishAllEquipEvent(GetAllEquipMent));
		ZS_EquipBtnShowInfo bindShowInfo = null;
		BindSelectBtnInfoAfterUpGrade(ref bindShowInfo);
		UpdateProcessAttribute(equipInfo);
		ZS_EquipShowPrice _price = bindShowInfo.GetComponentInChildren<ZS_EquipShowPrice>();
		FreshAvatarTopInfo(avatarInfo);
		yield return StartCoroutine(ShowScrollTopAttribute(equipInfo, 0.25f));
		Object.Destroy(obj);
		ownTopInfo.ShowEquipTopInfo(equipInfo.Image, equipInfo.Name);
		SetAllBtnUnVisable();
		if (!equipInfo.IsOwn)
		{
			yield break;
		}
		if (equipInfo.IsEquiped)
		{
			if (ZS_EquipPanelMove.clickLocation != 0 && ZS_EquipPanelMove.clickLocation != 1 && ZS_EquipPanelMove.clickLocation == ZS_EquipPanelMove.unwiledLocation)
			{
				unwieldBtn.gameObject.SetActiveRecursively(true);
				unwieldBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo bind3 = unwieldBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				bind3.equipInfo = equipInfo;
				bind3.SetUnwieldEventHandle(equipInfo.UnwieldCallBack);
			}
		}
		else if (equipInfo.IsCanEquip)
		{
			equipBtn.gameObject.SetActiveRecursively(true);
			equipBtn.m_PressObj.SetActiveRecursively(false);
			ZS_EquipEventBtnBindInfo bind2 = equipBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			bind2.equipInfo = equipInfo;
			bind2.SetEquipEventHandle(equipInfo.EquipCallBack);
		}
		else
		{
			equipFail.SetActiveRecursively(true);
			equipFailLab.SetFormatText("Text020", equipInfo.EquipCondition);
		}
		if (equipInfo.CanUpdate)
		{
			upGradeBtn.gameObject.SetActiveRecursively(true);
			upGradeBtn.m_PressObj.SetActiveRecursively(false);
			ZS_EquipEventBtnBindInfo bind = upGradeBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			bind.equipInfo = equipInfo;
			bind.SetUpGradeEventHandle(equipInfo.UpGradeCallBack);
			ZS_EquipShowPrice price = bind.GetComponentInChildren<ZS_EquipShowPrice>();
			price.ShowPriceObject(equipInfo.UpdateMoney);
		}
		else
		{
			updateFail.gameObject.SetActiveRecursively(true);
		}
	}

	private void BindSelectBtnInfoAfterBuy()
	{
		ZS_EquipBtnShowInfo[] componentsInChildren = base.gameObject.GetComponentsInChildren<ZS_EquipBtnShowInfo>();
		List<ZS_EquipBtnShowInfo> list = new List<ZS_EquipBtnShowInfo>();
		List<ZS_EquipmentInfo> list2 = new List<ZS_EquipmentInfo>();
		list2.AddRange(equipArray);
		list.AddRange(componentsInChildren);
		equipInfo = list2.Find((ZS_EquipmentInfo info) => info.Id.Equals(equipInfo.Id));
		ZS_EquipBtnShowInfo zS_EquipBtnShowInfo = list.Find((ZS_EquipBtnShowInfo bInfo) => equipInfo.Id.Equals(bInfo.myEquipInfo.Id));
		zS_EquipBtnShowInfo.myEquipInfo = equipInfo;
		ZS_EquipShowPrice componentInChildren = zS_EquipBtnShowInfo.GetComponentInChildren<ZS_EquipShowPrice>();
		componentInChildren.gameObject.SetActiveRecursively(false);
	}

	private void BindSelectBtnInfoAfterEquiped()
	{
		ZS_EquipBtnShowInfo[] componentsInChildren = base.gameObject.GetComponentsInChildren<ZS_EquipBtnShowInfo>();
		Dictionary<string, ZS_EquipmentInfo> dictionary = new Dictionary<string, ZS_EquipmentInfo>();
		List<ZS_EquipmentInfo> list = new List<ZS_EquipmentInfo>();
		list.AddRange(equipArray);
		foreach (ZS_EquipmentInfo item in list)
		{
			if (equipInfo.Id.Equals(item.Id))
			{
				equipInfo = item;
			}
			dictionary.Add(item.Id, item);
		}
		ZS_EquipBtnShowInfo[] array = componentsInChildren;
		foreach (ZS_EquipBtnShowInfo zS_EquipBtnShowInfo in array)
		{
			if (dictionary.ContainsKey(zS_EquipBtnShowInfo.myEquipInfo.Id))
			{
				zS_EquipBtnShowInfo.myEquipInfo = dictionary[zS_EquipBtnShowInfo.myEquipInfo.Id];
			}
			if (zS_EquipBtnShowInfo.myEquipInfo.IsEquiped)
			{
				zS_EquipBtnShowInfo.msEquipped.gameObject.SetActiveRecursively(true);
			}
			else
			{
				zS_EquipBtnShowInfo.msEquipped.gameObject.SetActiveRecursively(false);
			}
		}
	}

	private void BindSelectBtnInfoAfterUnwield()
	{
		ZS_EquipBtnShowInfo[] componentsInChildren = base.gameObject.GetComponentsInChildren<ZS_EquipBtnShowInfo>();
		List<ZS_EquipBtnShowInfo> list = new List<ZS_EquipBtnShowInfo>();
		list.AddRange(componentsInChildren);
		List<ZS_EquipmentInfo> list2 = new List<ZS_EquipmentInfo>();
		list2.AddRange(equipArray);
		equipInfo = list2.Find((ZS_EquipmentInfo info) => equipInfo.Id.Equals(info.Id));
		ZS_EquipBtnShowInfo zS_EquipBtnShowInfo = list.Find((ZS_EquipBtnShowInfo bInfo) => bInfo.myEquipInfo.Id.Equals(equipInfo.Id));
		zS_EquipBtnShowInfo.myEquipInfo = equipInfo;
		if (zS_EquipBtnShowInfo.myEquipInfo.IsEquiped)
		{
			zS_EquipBtnShowInfo.msEquipped.gameObject.SetActiveRecursively(true);
		}
		else
		{
			zS_EquipBtnShowInfo.msEquipped.gameObject.SetActiveRecursively(false);
		}
	}

	private void BindSelectBtnInfoAfterUpGrade(ref ZS_EquipBtnShowInfo btnInfo)
	{
		ZS_EquipBtnShowInfo[] componentsInChildren = base.gameObject.GetComponentsInChildren<ZS_EquipBtnShowInfo>();
		List<ZS_EquipBtnShowInfo> list = new List<ZS_EquipBtnShowInfo>();
		List<ZS_EquipmentInfo> list2 = new List<ZS_EquipmentInfo>();
		list.AddRange(componentsInChildren);
		list2.AddRange(equipArray);
		equipInfo = list2.Find((ZS_EquipmentInfo info) => info.Group.Equals(equipInfo.Group));
		btnInfo = list.Find((ZS_EquipBtnShowInfo bInfo) => equipInfo.Group.Equals(bInfo.myEquipInfo.Group));
		btnInfo.myEquipInfo = equipInfo;
	}

	private void GetAllItem(List<ZS_ItemInfo> itemList)
	{
		allItemList = itemList;
	}

	private void GetUsingItem(List<ZS_ItemInfo> itemList)
	{
		ZS_EquipUsingItemShow.usingItemInfo = itemList;
	}

	private void BindSelectBtnAfterBuyItem()
	{
		ZS_EquipBtnItemShowInfo[] componentsInChildren = base.transform.GetComponentsInChildren<ZS_EquipBtnItemShowInfo>();
		List<ZS_EquipBtnItemShowInfo> list = new List<ZS_EquipBtnItemShowInfo>();
		list.AddRange(componentsInChildren);
		itemInfo = allItemList.Find((ZS_ItemInfo info) => info.Id.Equals(itemInfo.Id));
		ZS_EquipBtnItemShowInfo zS_EquipBtnItemShowInfo = list.Find((ZS_EquipBtnItemShowInfo info) => info.eInfo.Id.Equals(itemInfo.Id));
		if (null != zS_EquipBtnItemShowInfo)
		{
			zS_EquipBtnItemShowInfo.eInfo = itemInfo;
			itemCount.Text = "YOU OWN " + zS_EquipBtnItemShowInfo.eInfo.Count;
		}
	}

	private void BindSelectBtnAfterEquipItem()
	{
		ZS_EquipBtnItemShowInfo[] componentsInChildren = base.transform.GetComponentsInChildren<ZS_EquipBtnItemShowInfo>();
		List<ZS_EquipBtnItemShowInfo> list = new List<ZS_EquipBtnItemShowInfo>();
		list.AddRange(componentsInChildren);
		itemInfo = allItemList.Find((ZS_ItemInfo info) => info.Id.Equals(itemInfo.Id));
		if (!itemInfo.canBuy)
		{
			buyItemBtn.gameObject.SetActiveRecursively(false);
		}
		ZS_EquipBtnItemShowInfo bindItem;
		foreach (ZS_EquipBtnItemShowInfo item in list)
		{
			bindItem = item;
			ZS_ItemInfo eInfo = allItemList.Find((ZS_ItemInfo info) => info.Id.Equals(bindItem.eInfo.Id));
			bindItem.eInfo = eInfo;
			if (bindItem.eInfo.IsUsing)
			{
				bindItem.msEquipped.gameObject.SetActiveRecursively(true);
			}
			else
			{
				bindItem.msEquipped.gameObject.SetActiveRecursively(false);
			}
		}
	}

	private void BindSelectBtnAfterUnwieldItem()
	{
		ZS_EquipBtnItemShowInfo[] componentsInChildren = base.transform.GetComponentsInChildren<ZS_EquipBtnItemShowInfo>();
		List<ZS_EquipBtnItemShowInfo> list = new List<ZS_EquipBtnItemShowInfo>();
		list.AddRange(componentsInChildren);
		itemInfo = allItemList.Find((ZS_ItemInfo info) => info.Id.Equals(itemInfo.Id));
		if (!itemInfo.canBuy)
		{
			buyItemBtn.gameObject.SetActiveRecursively(false);
		}
		ZS_EquipBtnItemShowInfo bindItem;
		foreach (ZS_EquipBtnItemShowInfo item in list)
		{
			bindItem = item;
			ZS_ItemInfo eInfo = allItemList.Find((ZS_ItemInfo info) => info.Id.Equals(bindItem.eInfo.Id));
			bindItem.eInfo = eInfo;
			if (bindItem.eInfo.IsUsing)
			{
				bindItem.msEquipped.gameObject.SetActiveRecursively(true);
			}
			else
			{
				bindItem.msEquipped.gameObject.SetActiveRecursively(false);
			}
		}
	}

	private void BuyItemSuccessDone()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvatar));
		EventCenter.Instance.Publish(this, new ZS_PublishAllItemEvent(GetAllItem));
		BindSelectBtnAfterBuyItem();
		FreshAvatarTopInfo(avatarInfo);
		SetAllBtnUnVisable();
		buyItemBtn.gameObject.SetActiveRecursively(true);
		buyItemBtn.m_PressObj.SetActiveRecursively(false);
		ZS_EquipEventBtnBindInfo component = buyItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
		component.itemInfo = itemInfo;
		component.SetBuyItemHandle(itemInfo.BuyCallBack);
		if (!itemInfo.IsOwn)
		{
			return;
		}
		if (itemInfo.IsUsing)
		{
			if (ZS_EquipPanelMove.unwiledLocation == ZS_EquipPanelMove.clickLocation)
			{
				unwieldItemBtn.gameObject.SetActiveRecursively(true);
				unwieldItemBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component2 = unwieldItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component2.itemInfo = itemInfo;
				component2.SetUnwieldItemHandle(itemInfo.UnwieldCallBack);
			}
		}
		else
		{
			equipItemBtn.gameObject.SetActiveRecursively(true);
			equipItemBtn.m_PressObj.SetActiveRecursively(false);
			ZS_EquipEventBtnBindInfo component3 = equipItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
			component3.itemInfo = itemInfo;
			component3.SetEquipItemHandle(itemInfo.EquipCallBack);
		}
	}

	private void EquipItemSuccessDone()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishUsingItemEvent(GetUsingItem, ZS_EquipUsingItemShow.count));
		EventCenter.Instance.Publish(this, new ZS_PublishAllItemEvent(GetAllItem));
		SetAllBtnUnVisable();
		unwieldItemBtn.gameObject.SetActiveRecursively(true);
		unwieldItemBtn.m_PressObj.SetActiveRecursively(false);
		equipItemBtn.gameObject.SetActiveRecursively(false);
		buyItemBtn.gameObject.SetActiveRecursively(true);
		buyItemBtn.m_PressObj.SetActiveRecursively(false);
		BindSelectBtnAfterEquipItem();
	}

	private void UnwieldItemSuccessDone()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishUsingItemEvent(GetUsingItem, ZS_EquipUsingItemShow.count));
		EventCenter.Instance.Publish(this, new ZS_PublishAllItemEvent(GetAllItem));
		SetAllBtnUnVisable();
		unwieldItemBtn.gameObject.SetActiveRecursively(false);
		equipItemBtn.gameObject.SetActiveRecursively(true);
		equipItemBtn.m_PressObj.SetActiveRecursively(false);
		buyItemBtn.gameObject.SetActiveRecursively(true);
		buyItemBtn.m_PressObj.SetActiveRecursively(false);
		BindSelectBtnAfterUnwieldItem();
	}

	public void SetAllBtnUnVisable()
	{
		upGradeBtn.gameObject.SetActiveRecursively(false);
		unwieldBtn.gameObject.SetActiveRecursively(false);
		buyBtn.gameObject.SetActiveRecursively(false);
		equipBtn.gameObject.SetActiveRecursively(false);
		buyItemBtn.gameObject.SetActiveRecursively(false);
		equipItemBtn.gameObject.SetActiveRecursively(false);
		unwieldItemBtn.gameObject.SetActiveRecursively(false);
		updateFail.SetActiveRecursively(false);
		buyFail.SetActiveRecursively(false);
		equipFail.SetActiveRecursively(false);
	}
}
