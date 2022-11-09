using System;
using UnityEngine;

public class ZS_EquipEventBtnBindInfo : MonoBehaviour
{
	public ZS_EquipmentInfo equipInfo;

	public ZS_ItemInfo itemInfo;

	private Func<ZS_EquipmentInfo, int> buyEventProcess;

	private Func<int, ZS_EquipmentInfo, int> equipEventProcess;

	private Func<int, ZS_EquipmentInfo, int> unwieldEventProcess;

	private Func<ZS_EquipmentInfo, int> upGradeEventProcess;

	private Func<ZS_ItemInfo, int> buyItemProcess;

	public Func<int, ZS_ItemInfo, int> equipItemProcess;

	private Func<int, ZS_ItemInfo, int> unwieldItemProcess;

	public void SetBuyItemHandle(Func<ZS_ItemInfo, int> handle)
	{
		buyItemProcess = handle;
	}

	public void SetEquipItemHandle(Func<int, ZS_ItemInfo, int> handle)
	{
		equipItemProcess = handle;
	}

	public void SetUnwieldItemHandle(Func<int, ZS_ItemInfo, int> handle)
	{
		unwieldItemProcess = handle;
	}

	public void SetBuyEventHandle(Func<ZS_EquipmentInfo, int> handle)
	{
		buyEventProcess = handle;
	}

	public void SetEquipEventHandle(Func<int, ZS_EquipmentInfo, int> handle)
	{
		equipEventProcess = handle;
	}

	public void SetUnwieldEventHandle(Func<int, ZS_EquipmentInfo, int> handle)
	{
		unwieldEventProcess = handle;
	}

	public void SetUpGradeEventHandle(Func<ZS_EquipmentInfo, int> handle)
	{
		upGradeEventProcess = handle;
	}

	public void ClearBuyEventHandle()
	{
		buyEventProcess = null;
	}

	public void ClearEquipEventHandle()
	{
		equipEventProcess = null;
	}

	public void ClearUnwieldEventHandle()
	{
		unwieldEventProcess = null;
	}

	public void ClearUpgraedEventHandle()
	{
		upGradeEventProcess = null;
	}

	public void ClearBuyItemHandle()
	{
		buyItemProcess = null;
	}

	public void ClearEquipItemHandle()
	{
		equipItemProcess = null;
	}

	public void ClearUnwieldItemHandle()
	{
		unwieldItemProcess = null;
	}

	public int NotifyBuyItemClick()
	{
		if (buyItemProcess != null)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_buy);
			return buyItemProcess(itemInfo);
		}
		return 3;
	}

	public int NotifyEquipItemClick()
	{
		if (equipItemProcess != null)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_weapon_equip);
			return equipItemProcess(ZS_EquipPanelMove.clickLocation, itemInfo);
		}
		return 3;
	}

	public int NotifyUnwieldItemClick()
	{
		if (unwieldItemProcess != null)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_weapon_unwield);
			return unwieldItemProcess(ZS_EquipPanelMove.unwiledLocation, itemInfo);
		}
		return 3;
	}

	public int NotifyBuyClick()
	{
		if (buyEventProcess != null)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_buy);
			return buyEventProcess(equipInfo);
		}
		return 3;
	}

	public int NotifyEquipClick()
	{
		if (equipEventProcess != null)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_weapon_equip);
			return equipEventProcess(ZS_EquipPanelMove.clickLocation, equipInfo);
		}
		return 3;
	}

	public int NotifyUnwieldClick()
	{
		if (unwieldEventProcess != null)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_weapon_unwield);
			return unwieldEventProcess(ZS_EquipPanelMove.unwiledLocation, equipInfo);
		}
		return 3;
	}

	public int NotifyUpgraedClick()
	{
		if (upGradeEventProcess != null)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_weapon_upgrad);
			return upGradeEventProcess(equipInfo);
		}
		return 3;
	}
}
