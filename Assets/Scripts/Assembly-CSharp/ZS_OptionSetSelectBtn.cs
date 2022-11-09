using System;
using Event;
using UnityEngine;

public class ZS_OptionSetSelectBtn : MonoBehaviour
{
	public TUIButtonSelect btnOn;

	public TUIButtonSelect btnOff;

	private Func<bool> triggerMusicOnEvent;

	private Func<bool> triggerMusicOffEvent;

	private Func<bool> triggerSoundOnEvent;

	private Func<bool> triggerSoundOffEvent;

	public void SetMusicOnEventHandle(Func<bool> handle)
	{
		triggerMusicOnEvent = handle;
	}

	public void ClearMusicOnEventHandle()
	{
		triggerMusicOnEvent = null;
	}

	public void SetMusicOffEventHandle(Func<bool> handle)
	{
		triggerMusicOffEvent = handle;
	}

	public void ClearMusicOffEventHandle()
	{
		triggerMusicOffEvent = null;
	}

	public void SetSoundOnEventHandle(Func<bool> handle)
	{
		triggerSoundOnEvent = handle;
	}

	public void ClearSoundOnEventHandle()
	{
		triggerSoundOnEvent = null;
	}

	public void SetSoundOffEventHandle(Func<bool> handle)
	{
		triggerSoundOffEvent = handle;
	}

	public void ClearSoundOffEventHandle()
	{
		triggerSoundOffEvent = null;
	}

	private void Start()
	{
		SetMusicOnEventHandle(SetMusicOn);
		SetMusicOffEventHandle(SetMusicOff);
		SetSoundOnEventHandle(SetSoundOn);
		SetSoundOffEventHandle(SetSoundOff);
	}

	public void SetSelectBtnState(bool flag)
	{
		if (flag)
		{
			btnOn.SetSelected(flag);
			btnOff.SetSelected(!flag);
		}
		else
		{
			btnOn.SetSelected(flag);
			btnOff.SetSelected(!flag);
		}
	}

	private bool SetMusicOn()
	{
		ChangeMusicState(true);
		return true;
	}

	private bool SetMusicOff()
	{
		ChangeMusicState(false);
		return true;
	}

	private bool SetSoundOn()
	{
		ChangeSoundState(true);
		return true;
	}

	private bool SetSoundOff()
	{
		ChangeSoundState(false);
		return true;
	}

	private void SelectBtnMusicOnEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			triggerMusicOnEvent();
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
		}
	}

	private void SelectBtnMusicOffEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			triggerMusicOffEvent();
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
		}
	}

	private void SelectBtnSoundOnEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			triggerSoundOnEvent();
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
		}
	}

	private void SelectBtnSoundOffEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			triggerSoundOffEvent();
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_moveon);
		}
	}

	private void ChangeMusicState(bool flag)
	{
		ZS_OptionSetSoundEvent.optionInfo.isMusicOn = flag;
		ZS_OptionSetSoundEvent.optionInfo.OptionCallBack(ZS_OptionSetSoundEvent.optionInfo);
		EventCenter.Instance.Publish(this, new ZS_PublishOptionInfoEvent(ZS_OptionSetSoundEvent.optionInfo));
	}

	private void ChangeSoundState(bool flag)
	{
		ZS_OptionSetSoundEvent.optionInfo.isSoundOn = flag;
		ZS_OptionSetSoundEvent.optionInfo.OptionCallBack(ZS_OptionSetSoundEvent.optionInfo);
		EventCenter.Instance.Publish(this, new ZS_PublishOptionInfoEvent(ZS_OptionSetSoundEvent.optionInfo));
	}
}
