using Event;
using UnityEngine;

public class ZS_RongyuShow : MonoBehaviour
{
	private const string jinbiIcon = "jinbi";

	private const string shuijinIcon = "shuijing";

	public TUILabel rewardCount;

	public TUILabel messageLab;

	public TUILabel sliderLab;

	public TUIMeshSprite iconSprite;

	public TUIMeshSprite fgSprite;

	public TUISlider slider;

	public TUIButton compButton;

	public GameObject compUnButton;

	public TUIMeshSprite completeIcon;

	private ZS_AvatarInfo avatarInfo;

	public void SetInfo(ZS_RongyuInfo rongyuInfo)
	{
		if (rongyuInfo.money.Gold > 0.0)
		{
			ZS_TUIMisc.SetImage(iconSprite, "jinbi");
			ZS_TUIMisc.SetLabel(rewardCount, " X " + ZS_TUIMisc.FormatToString(rongyuInfo.money.Gold));
		}
		else if (rongyuInfo.money.Tcystal > 0.0)
		{
			ZS_TUIMisc.SetImage(iconSprite, "shuijing");
			ZS_TUIMisc.SetLabel(rewardCount, " X " + ZS_TUIMisc.FormatToString(rongyuInfo.money.Tcystal));
		}
		else
		{
			ZS_TUIMisc.SetImage(iconSprite, rongyuInfo.icon);
			ZS_TUIMisc.SetLabel(rewardCount, " X " + ZS_TUIMisc.FormatToString(rongyuInfo.rewardCount));
		}
		messageLab.TextID = rongyuInfo.message;
		if (rongyuInfo.isCompleted)
		{
			compButton.gameObject.SetActiveRecursively(false);
			compUnButton.SetActiveRecursively(false);
			completeIcon.gameObject.SetActiveRecursively(true);
			slider.gameObject.SetActiveRecursively(false);
			slider.gameObject.active = true;
		}
		else if (rongyuInfo.compPercent < 1f)
		{
			compUnButton.SetActiveRecursively(true);
			compButton.gameObject.SetActiveRecursively(false);
			completeIcon.gameObject.SetActiveRecursively(false);
			slider.gameObject.SetActiveRecursively(true);
			slider.sliderValue = rongyuInfo.compPercent;
			ZS_TUIMisc.SetLabel(sliderLab, Mathf.Floor(rongyuInfo.compPercent * 100f) + "%");
		}
		else
		{
			slider.gameObject.SetActiveRecursively(true);
			slider.sliderValue = rongyuInfo.compPercent;
			ZS_TUIMisc.SetLabel(sliderLab, Mathf.Floor(rongyuInfo.compPercent * 100f) + "%");
			compButton.gameObject.SetActiveRecursively(true);
			compButton.m_PressObj.SetActiveRecursively(false);
			compUnButton.SetActiveRecursively(false);
			completeIcon.gameObject.SetActiveRecursively(false);
		}
	}

	private void Click_RewardButton(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_bonus);
			ZS_RongyuReward component = control.GetComponent<ZS_RongyuReward>();
			ZS_RongyuInfo zS_RongyuInfo = component.NotifyClick();
			if (zS_RongyuInfo != null)
			{
				RewardSuccess(component.bindInfo, zS_RongyuInfo);
			}
		}
	}

	private void RewardSuccess(ZS_RongyuInfo info, ZS_RongyuInfo nextInfo)
	{
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvater));
		ZS_RongYuEventProcess component = base.transform.parent.parent.parent.parent.gameObject.GetComponent<ZS_RongYuEventProcess>();
		ZS_RongyuReward componentInChildren = base.transform.GetComponentInChildren<ZS_RongyuReward>();
		componentInChildren.bindInfo = nextInfo;
		componentInChildren.SetEventHandle(nextInfo.callBack);
		ZS_RongyuShow component2 = componentInChildren.transform.parent.GetComponent<ZS_RongyuShow>();
		component2.SetInfo(nextInfo);
		if (info.money.Gold > 0.0)
		{
			component.goldLab.Text = ZS_TUIMisc.FormatToString(avatarInfo.Money.Gold);
		}
		else if (info.money.Tcystal > 0.0)
		{
			component.tcyLab.Text = ZS_TUIMisc.FormatToString(avatarInfo.Money.Tcystal);
		}
	}

	private void GetCurrentAvater(ZS_AvatarInfo info)
	{
		avatarInfo = info;
	}
}
