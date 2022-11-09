using System;
using System.Collections.Generic;
using UnityEngine;

public class ZS_EquipBtnShowInfo : MonoBehaviour
{
	public ZS_EquipmentInfo myEquipInfo;

	public TUIMeshSprite msEquipped;

	public TUIMeshSprite equipIcon;

	public TUIMeshSprite lockIcon;

	public TUILabel count;

	private Func<ZS_EquipmentInfo, bool> triggerSelectEvent;

	private void Start()
	{
		SetSelectEventHandle(HandleEvent);
	}

	public void SetSelectEventHandle(Func<ZS_EquipmentInfo, bool> handle)
	{
		triggerSelectEvent = handle;
	}

	public void ClearSelectEventHandle()
	{
		triggerSelectEvent = null;
	}

	public bool HandleEvent(ZS_EquipmentInfo einfo)
	{
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
		GameObject gameObject = base.transform.parent.parent.parent.parent.gameObject;
		ZS_GroupObjectBind component = gameObject.GetComponent<ZS_GroupObjectBind>();
		component.equipInfo = einfo;
		ZS_EquipPanelMove.unwiledLocation = GetUnwieldLocation(ZS_EquipUsingShow.usingEquipInfo, einfo.Group, ZS_EquipPanelMove.clickLocation);
		if (einfo.IsOwn)
		{
			component.equipOwn.SetActiveRecursively(true);
			component.equipUnOwn.SetActiveRecursively(false);
			component.ownTopInfo.ShowEquipTopInfo(einfo.Image, einfo.Name);
			ShowEquipUpGradeInfo(component, einfo);
			component.SetAllBtnUnVisable();
			if (einfo.CanUpdate)
			{
				component.upGradeBtn.gameObject.SetActiveRecursively(true);
				component.upGradeBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component2 = component.upGradeBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component2.equipInfo = einfo;
				component2.SetUpGradeEventHandle(einfo.UpGradeCallBack);
				ZS_EquipShowPrice componentInChildren = component2.GetComponentInChildren<ZS_EquipShowPrice>();
				componentInChildren.ShowPriceObject(einfo.UpdateMoney);
			}
			else
			{
				component.updateFail.SetActiveRecursively(true);
			}
			if (einfo.IsEquiped)
			{
				if (ZS_EquipPanelMove.unwiledLocation != 0 && ZS_EquipPanelMove.unwiledLocation != 1)
				{
					if (ZS_EquipPanelMove.unwiledLocation == ZS_EquipPanelMove.clickLocation)
					{
						component.unwieldBtn.gameObject.SetActiveRecursively(true);
						component.unwieldBtn.m_PressObj.SetActiveRecursively(false);
						ZS_EquipEventBtnBindInfo component3 = component.unwieldBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
						component3.equipInfo = einfo;
						component3.SetUnwieldEventHandle(einfo.UnwieldCallBack);
					}
				}
				else
				{
					component.unwieldBtn.gameObject.SetActiveRecursively(false);
				}
			}
			else if (einfo.IsCanEquip)
			{
				component.equipBtn.gameObject.SetActiveRecursively(true);
				component.equipBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component4 = component.equipBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component4.equipInfo = einfo;
				component4.SetEquipEventHandle(einfo.EquipCallBack);
			}
			else
			{
				component.equipFail.SetActiveRecursively(true);
				component.equipFailLab.SetFormatText("Text020", einfo.EquipCondition);
			}
		}
		else
		{
			component.equipOwn.SetActiveRecursively(false);
			component.equipUnOwn.SetActiveRecursively(true);
			component.unOwnTopInfo.ShowEquipTopInfo(einfo.Image, einfo.Name);
			ShowEquipUpGradeInfo(component, einfo);
			component.SetAllBtnUnVisable();
			if (einfo.IsCanBuy)
			{
				component.buyBtn.gameObject.SetActiveRecursively(true);
				component.buyBtn.m_PressObj.SetActiveRecursively(false);
				ZS_EquipEventBtnBindInfo component5 = component.buyBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
				component5.equipInfo = einfo;
				component5.SetBuyEventHandle(einfo.BuyCallBack);
				ZS_EquipShowPrice componentInChildren2 = component5.GetComponentInChildren<ZS_EquipShowPrice>();
				componentInChildren2.ShowPriceObject(einfo.BuyMoney);
			}
			else
			{
				component.buyFail.SetActiveRecursively(true);
				component.buyFailLab.SetFormatText("Text019", einfo.BuyCondition);
			}
		}
		return true;
	}

	public static void ShowEquipUpGradeInfo(ZS_GroupObjectBind groupShow, ZS_EquipmentInfo einfo)
	{
		if (einfo.IsOwn)
		{
			groupShow.sliderOLAmmo.sliderValue = (float)einfo.Ammo / (float)einfo.maxAmmo;
			groupShow.sliderOLAttack.sliderValue = (float)einfo.Attack / (float)einfo.maxAttack;
			groupShow.sliderOLChance.sliderValue = einfo.CriticalChance / einfo.maxCriticalChance;
			groupShow.sliderOLHit.sliderValue = einfo.CriticalHit / einfo.maxCriticalHit;
			groupShow.sliderOLKnock.sliderValue = einfo.KnockBack / einfo.maxKnockBack;
			groupShow.sliderOTAmmo.sliderValue = (float)einfo.NextAmmo / (float)einfo.maxAmmo;
			groupShow.sliderOTAttack.sliderValue = (float)einfo.NextAttack / (float)einfo.maxAttack;
			groupShow.sliderOTChance.sliderValue = einfo.NextCriticalChance / einfo.maxCriticalChance;
			groupShow.sliderOTHit.sliderValue = einfo.NextCriticalHit / einfo.maxCriticalHit;
			groupShow.sliderOTKnock.sliderValue = einfo.NextKnockBack / einfo.maxKnockBack;
			groupShow.sliderOAmmoLab.Text = einfo.Ammo.ToString();
			groupShow.sliderOAttackLab.Text = einfo.Attack.ToString();
			groupShow.sliderOChanceLab.Text = einfo.CriticalChance.ToString();
			groupShow.sliderOKnockLab.Text = einfo.KnockBack.ToString();
			groupShow.sliderOHitLab.Text = einfo.CriticalHit.ToString();
		}
		else
		{
			groupShow.sliderULAmmo.sliderValue = (float)einfo.minAmmo / (float)einfo.maxAmmo;
			groupShow.sliderULAttack.sliderValue = (float)einfo.minAttack / (float)einfo.maxAttack;
			groupShow.sliderULChance.sliderValue = einfo.minCriticalChance / einfo.maxCriticalChance;
			groupShow.sliderULHit.sliderValue = einfo.minCriticalHit / einfo.maxCriticalHit;
			groupShow.sliderULKnock.sliderValue = einfo.minKnockBack / einfo.maxKnockBack;
			groupShow.sliderUTAmmo.gameObject.SetActiveRecursively(false);
			groupShow.sliderUTAttack.gameObject.SetActiveRecursively(false);
			groupShow.sliderUTChance.gameObject.SetActiveRecursively(false);
			groupShow.sliderUTHit.gameObject.SetActiveRecursively(false);
			groupShow.sliderUTKnock.gameObject.SetActiveRecursively(false);
			groupShow.sliderUAmmoLab.Text = einfo.Ammo.ToString();
			groupShow.sliderUAttackLab.Text = einfo.Attack.ToString();
			groupShow.sliderUChanceLab.Text = einfo.CriticalChance.ToString();
			groupShow.sliderUKnockLab.Text = einfo.KnockBack.ToString();
			groupShow.sliderUHitLab.Text = einfo.CriticalHit.ToString();
		}
	}

	private int GetUnwieldLocation(List<ZS_EquipmentInfo> usingList, string group, int defaultLocation)
	{
		for (int i = 0; i < usingList.Count; i++)
		{
			if (usingList[i] != null && usingList[i].Group.Equals(group))
			{
				return i;
			}
		}
		return defaultLocation;
	}

	public bool NotifySelectEvent()
	{
		if (myEquipInfo != null)
		{
			return triggerSelectEvent(myEquipInfo);
		}
		return false;
	}
}
