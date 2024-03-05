using UnityEngine;

public class ZS_NotEnoughMoney : MonoBehaviour
{
	public TUILabel contentLab;

	public TUILabel titleLab;

	public GameObject IapBox;

	public TUIMeshSprite mapShadow;

	public string url;

	private void Start()
	{
		base.transform.localScale = base.transform.localScale * 1E-05f;
	}

	private void CancelIapEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_no_back);
			base.transform.localScale = base.transform.localScale * 1E-05f;
			mapShadow.transform.localScale = mapShadow.transform.localScale * 1E-05f;
		}
	}

	private void ConfirmIapEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			IapBox.GetComponent<Animation>().Play("UI2");
			base.transform.localScale = base.transform.localScale * 1E-05f;
		}
	}

	private void ConfirmIapGoldEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			base.transform.localScale = base.transform.localScale * 1E-05f;
			mapShadow.transform.localScale = mapShadow.transform.localScale * 1E-05f;
		}
	}

	private void ConfirmIapGameEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			base.transform.localScale = base.transform.localScale * 1E-05f;
			mapShadow.transform.localScale = mapShadow.transform.localScale * 1E-05f;
			Application.OpenURL(url);
		}
	}
}
