using Event;
using UnityEngine;

public class ZS_OptionResetEvent : MonoBehaviour
{
	public ZS_AvatarInfo currentAvartar;

	public ZS_OptionEventProcess tar;

	private void CancelResetEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			tar.CancelMapShadow();
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
			base.gameObject.SetActiveRecursively(false);
		}
	}

	private void ConfirmResetEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			tar.CancelMapShadow();
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
			base.gameObject.SetActiveRecursively(false);
			RestData();
		}
	}

	private void GetCurrentAvatar(ZS_AvatarInfo avatarInfo)
	{
		currentAvartar = avatarInfo;
	}

	private void RestData()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishResetDataEvent());
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvatar));
		EventCenter.Instance.Publish(this, new ZS_PublishOptionInfoEvent(ZS_OptionSetSoundEvent.optionInfo));
		ZS_TopInfomation componentInChildren = base.transform.parent.GetComponentInChildren<ZS_TopInfomation>();
		if (currentAvartar != null && null != componentInChildren)
		{
			componentInChildren.SetTopInfo(currentAvartar.CurrentAvatarPhoto.level, currentAvartar.Money, currentAvartar.Experience);
			ZS_OptionSetSoundEvent componentInChildren2 = base.transform.parent.GetComponentInChildren<ZS_OptionSetSoundEvent>();
			componentInChildren2.musciSet.SetSelectBtnState(ZS_OptionSetSoundEvent.optionInfo.isMusicOn);
			componentInChildren2.soundSet.SetSelectBtnState(ZS_OptionSetSoundEvent.optionInfo.isSoundOn);
		}
	}
}
