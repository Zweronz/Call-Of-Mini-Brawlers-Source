using System;
using UnityEngine;

public class ZS_AvatarPhotoEventProcess : MonoBehaviour
{
	private const string goldIcon = "jinbi";

	private const string tcyIcon = "shuijin";

	public static ZS_AvatarInfo currentAvatar;

	private Func<bool> selectBtnEvent;

	private ZS_AvatarGroupBtnShow avatarGroup;

	private ZS_AvatarPhotoScrollInfo apsi;

	private void Start()
	{
		InitialData();
	}

	private void InitialData()
	{
		currentAvatar = ZS_TopInfomation.avatar;
		SetSelectBtnEventHandle(SelectEventHandle);
	}

	private void GetCurrentAvatar(ZS_AvatarInfo avatar)
	{
		currentAvatar = avatar;
	}

	private void SetSelectBtnEventHandle(Func<bool> handle)
	{
		selectBtnEvent = handle;
	}

	private void ClearSelectBtnEventHandle()
	{
		selectBtnEvent = null;
	}

	private void AvatarPhotoSeleced(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			selectBtnEvent();
		}
	}

	private bool SelectEventHandle()
	{
		avatarGroup = base.transform.parent.parent.parent.GetComponent<ZS_AvatarGroupBtnShow>();
		apsi = avatarGroup.transform.parent.parent.GetComponent<ZS_AvatarPhotoScrollInfo>();
		TUIButtonSelect[] componentsInChildren = avatarGroup.transform.GetComponentsInChildren<TUIButtonSelect>();
		avatarGroup.HideAvatarPanelBtn();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ZS_AvatarBtnShowInfo component = componentsInChildren[i].gameObject.GetComponent<ZS_AvatarBtnShowInfo>();
			if (componentsInChildren[i].IsSelected())
			{
				ZS_AvatarPhotoInfo avatarPhotoInfo = component.avatarPhotoInfo;
				if (!string.IsNullOrEmpty(avatarPhotoInfo.specialId))
				{
					apsi.specialLab.gameObject.SetActiveRecursively(true);
					apsi.specialLab.TextID = avatarPhotoInfo.specialId;
				}
				else
				{
					apsi.specialLab.gameObject.SetActiveRecursively(false);
				}
				apsi.descLab.TextID = avatarPhotoInfo.name;
				if (avatarPhotoInfo.level > 0)
				{
					ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
					if (avatarPhotoInfo.id.Equals(currentAvatar.CurrentAvatarPhoto.id))
					{
						avatarGroup.unableBtn.SetActiveRecursively(true);
					}
					else
					{
						avatarGroup.useBtn.gameObject.SetActiveRecursively(true);
						avatarGroup.useBtn.m_PressObj.SetActiveRecursively(false);
						ZS_AvatarPhotoUseBtnBind component2 = avatarGroup.useBtn.GetComponent<ZS_AvatarPhotoUseBtnBind>();
						component2.bindAvatarPhoto = avatarPhotoInfo;
						apsi.SetUseAvatarPhotoEvent(avatarPhotoInfo.UseAvatarCallBack);
					}
				}
				else if (avatarPhotoInfo.isLock)
				{
					ZS_UIAudioManager.PlayAudio(SoundKind.UI_hero_lock);
					avatarGroup.unableBtn.SetActiveRecursively(true);
					avatarGroup.unlockShow.SetActiveRecursively(true);
					avatarGroup.unlockCondition.Text = avatarPhotoInfo.unlockCondition;
				}
				else
				{
					ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
					avatarGroup.buyBtn.gameObject.SetActiveRecursively(true);
					avatarGroup.buyBtn.m_PressObj.SetActiveRecursively(false);
					ZS_AvatarPhotoBuyBtnBind component3 = avatarGroup.buyBtn.GetComponent<ZS_AvatarPhotoBuyBtnBind>();
					component3.bindAvatarPhoto = avatarPhotoInfo;
					apsi.SetBuyAvatarPhotoEvent(avatarPhotoInfo.BuyAvatarCallBack);
					if (avatarPhotoInfo.money.Gold > 0.0)
					{
						component3.priceIcon.texture = "jinbi";
						component3.priceLab.Text = ZS_TUIMisc.FormatToString(avatarPhotoInfo.money.Gold);
					}
					else if (avatarPhotoInfo.money.Tcystal > 0.0)
					{
						component3.priceIcon.texture = "shuijin";
						component3.priceLab.Text = ZS_TUIMisc.FormatToString(avatarPhotoInfo.money.Tcystal);
					}
				}
				avatarGroup.hpShow.SetActiveRecursively(false);
				component.selectedIcon.gameObject.SetActiveRecursively(true);
				avatarGroup.FreshAvatarModel(avatarPhotoInfo);
			}
			else
			{
				component.selectedIcon.gameObject.SetActiveRecursively(false);
			}
		}
		return true;
	}
}
