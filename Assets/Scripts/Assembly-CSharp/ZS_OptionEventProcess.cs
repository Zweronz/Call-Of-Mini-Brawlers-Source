using System;
using UnityEngine;

public class ZS_OptionEventProcess : MonoBehaviour
{
	public GameObject box;

	private Func<bool> triggerSupportEvent;

	public TUIMeshSprite shadowMap;

	private GameObject temp;

	private void Awake()
	{
		box.SetActiveRecursively(false);
	}

	public void SetSupportEventHandle(Func<bool> handle)
	{
		triggerSupportEvent = handle;
	}

	public void ClearSupportEventHandle()
	{
		triggerSupportEvent = null;
	}

	private bool ReviewEvent()
	{
		return true;
	}

	private bool SupportEvent()
	{
		Application.OpenURL("https://www.youtube.com/watch?v=gyNtZeN_gTQ&list=PLameREsjcp75af75spbRZoHwC0ICqSeJ3");
		return true;
	}

	private bool ResetEvent()
	{
		return true;
	}

	private void Start()
	{
		SetSupportEventHandle(SupportEvent);
	}

	private void ReviewEventHandle(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			Application.OpenURL("https://www.youtube.com/watch?v=gyNtZeN_gTQ&list=PLameREsjcp75af75spbRZoHwC0ICqSeJ3");
		}
	}

	private void SupportEventHandle(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			triggerSupportEvent();
		}
	}

	private void ResetEventHandle(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			box.SetActiveRecursively(true);
			shadowMap.transform.localScale = shadowMap.transform.localScale * 100000f;
		}
	}

	public void CancelMapShadow()
	{
		shadowMap.transform.localScale = shadowMap.transform.localScale * 1E-05f;
	}
}
