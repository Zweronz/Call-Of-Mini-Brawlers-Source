using System.Collections.Generic;
using Event;
using UnityEngine;

public class NewWeaponPanel : MonoBehaviour
{
	public TUIMeshSprite weaponIcon;

	public Vector3 showDis;

	public Vector3 disappearDis;

	private Stack<string> weaponIconStack = new Stack<string>();

	private void Awake()
	{
		EventCenter.Instance.Register<ShowNewWeaponUnlockEvent>(HandleShowNewWeaponUnlockEvent);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<ShowNewWeaponUnlockEvent>(HandleShowNewWeaponUnlockEvent);
	}

	private void Start()
	{
		Hide();
	}

	private void Update()
	{
	}

	private void Show()
	{
		base.transform.position = showDis;
	}

	private void Hide()
	{
		base.transform.position = disappearDis;
	}

	private void HandleShowNewWeaponUnlockEvent(object sender, ShowNewWeaponUnlockEvent evt)
	{
		if (evt.WeaponIconList == null || evt.WeaponIconList.Count <= 0)
		{
			return;
		}
		weaponIconStack.Clear();
		foreach (string weaponIcon in evt.WeaponIconList)
		{
			weaponIconStack.Push(weaponIcon);
		}
		if (ShowWeapon())
		{
			Show();
		}
	}

	private void HandleTapBtn(TUIControl ctl, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3 && !ShowWeapon())
		{
			Hide();
		}
	}

	private bool ShowWeapon()
	{
		bool flag = weaponIconStack.Count > 0;
		if (flag)
		{
			weaponIcon.texture = weaponIconStack.Pop();
		}
		return flag;
	}
}
