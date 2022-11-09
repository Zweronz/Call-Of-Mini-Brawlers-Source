using UnityEngine;

public class StartEnter : MonoBehaviour
{
	public GameObject notDel;

	public GameObject startBox;

	private void TriggerEquip(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			Application.LoadLevel(ZS_TUIMisc.equipScene);
		}
	}

	private void TriggerHero(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			Application.LoadLevel(ZS_TUIMisc.heroScene);
		}
	}

	private void TriggerOPtion(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			Application.LoadLevel(ZS_TUIMisc.optionScene);
		}
	}

	private void TriggerRongYu(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			Application.LoadLevel(ZS_TUIMisc.gloryScene);
		}
	}

	private void TriggerStart(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			GameObject gameObject = Object.Instantiate(startBox) as GameObject;
			gameObject.transform.transform.parent = base.transform.parent;
		}
	}

	private void TriggerGuanKa(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			Application.LoadLevel("GuanKaUI");
		}
	}
}
