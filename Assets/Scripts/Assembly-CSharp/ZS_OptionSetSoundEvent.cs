using Event;
using UnityEngine;

public class ZS_OptionSetSoundEvent : MonoBehaviour
{
	public ZS_OptionSetSelectBtn musciSet;

	public ZS_OptionSetSelectBtn soundSet;

	public static ZS_OptionInfo optionInfo = new ZS_OptionInfo();

	private void Start()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishOptionInfoEvent(optionInfo));
		musciSet.SetSelectBtnState(optionInfo.isMusicOn);
		soundSet.SetSelectBtnState(optionInfo.isSoundOn);
	}
}
