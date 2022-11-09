using UnityEngine;

public class ZS_IapOut : MonoBehaviour
{
	public TUIMeshSprite shadow;

	public TUIMeshSprite navShadow;

	public TUIBlock blockMap;

	private void TriggerIapOutEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType != 3)
		{
			return;
		}
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_no_back);
		base.GetComponent<Animation>().Play("UI3");
		blockMap.gameObject.active = false;
		if (ZS_NavigationMove.isMaskShow)
		{
			if (null != shadow)
			{
				shadow.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			}
			if (null != navShadow)
			{
				navShadow.transform.localScale = new Vector3(800f, 800f, 0.01f);
			}
		}
		else if (null != shadow)
		{
			shadow.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		}
	}
}
