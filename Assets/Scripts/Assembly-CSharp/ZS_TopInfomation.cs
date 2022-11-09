using System;
using Event;
using UnityEngine;

public class ZS_TopInfomation : MonoBehaviour
{
	private const int frameCount = 50;

	private Func<bool> backEvent;

	private Func<bool> anniuEvent;

	public TUIMeshSprite mSprite;

	public TUIMeshSprite gMSprite;

	public TUIMeshSprite shadow;

	public TUIMeshSprite navShadow;

	public TUISlider forSlider;

	public TUISlider guangSlider;

	public TUILabel levLabel;

	public TUILabel goldLabel;

	public TUILabel tcyLabel;

	public TUILabel levPercentLab;

	public TUIRect rect;

	public TUIFade fade;

	public GameObject IapBox;

	public TUIBlock blockMap;

	public TUIMeshSprite rewardFlagmSprit;

	public static ZS_AvatarInfo avatar;

	private void Awake()
	{
		EventCenter.Instance.Register<CrystalChangedEvent>(HandleCrystalChangedEvent);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<CrystalChangedEvent>(HandleCrystalChangedEvent);
	}

	private void HandleCrystalChangedEvent(object sender, CrystalChangedEvent evt)
	{
		ZS_TUIMisc.SetLabel(tcyLabel, ZS_TUIMisc.FormatToString(evt.Crystal));
	}

	private void Start()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvatar));
		blockMap.gameObject.active = false;
		ShowAvatarAchieveState();
		SetTopInfo(avatar.CurrentAvatarPhoto.level, avatar.Money, avatar.Experience);
		SetBackEventHandle(HandleBack);
		SetAnniuEventHandle(HandleAnniu);
	}

	private void ShowAvatarAchieveState()
	{
		if (avatar.IsAchieveComplete)
		{
			rewardFlagmSprit.gameObject.SetActiveRecursively(true);
		}
		else
		{
			rewardFlagmSprit.gameObject.SetActiveRecursively(false);
		}
	}

	private void GetCurrentAvatar(ZS_AvatarInfo avatarInfo)
	{
		avatar = avatarInfo;
	}

	private void SetBackEventHandle(Func<bool> handle)
	{
		backEvent = handle;
	}

	private void ClearBackEventHandle()
	{
		backEvent = null;
	}

	private void SetAnniuEventHandle(Func<bool> handle)
	{
		anniuEvent = handle;
	}

	private void ClearAnniuEventHandle()
	{
		anniuEvent = null;
	}

	public void SetTopInfo(int heroLevel, ZS_Money money, float experience)
	{
		forSlider.sliderValue = experience;
		guangSlider.sliderValue = experience;
		ZS_TUIMisc.SetLabel(levLabel, Convert.ToString(heroLevel));
		ZS_TUIMisc.SetLabel(levPercentLab, Mathf.Floor(experience * 100f) + "%");
		ZS_TUIMisc.SetLabel(goldLabel, ZS_TUIMisc.FormatToString(money.Gold));
		ZS_TUIMisc.SetLabel(tcyLabel, ZS_TUIMisc.FormatToString(money.Tcystal));
	}

	private void TriggerBackEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_no_back, true);
			backEvent();
		}
	}

	private void TriggerAnniuEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			anniuEvent();
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_popup);
		}
	}

	private bool HandleBack()
	{
		if (null != fade)
		{
			fade.FadeOut(ZS_TUIMisc.indexScene);
		}
		else
		{
			Application.LoadLevel(ZS_TUIMisc.indexScene);
		}
		return true;
	}

	private bool HandleAnniu()
	{
		IapBox.GetComponent<Animation>().Play("UI2");
		blockMap.gameObject.active = true;
		if (null != shadow)
		{
			shadow.transform.localScale = new Vector3(800f, 800f, 1f);
		}
		if (null != navShadow)
		{
			navShadow.transform.localScale = new Vector3(0.001f, 0.001f, 1f);
		}
		return true;
	}
}
