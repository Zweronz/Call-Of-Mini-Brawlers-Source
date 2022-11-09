using System;
using System.Collections;
using UnityEngine;

public class ZS_NavigationMove : MonoBehaviour
{
	public static bool isMaskShow;

	private bool flag = true;

	private bool canNextMove = true;

	private float moveSpeed = 1f;

	public float moveDistance = 250f;

	private Vector3 pos;

	public float moveTime = 0.3f;

	public GameObject navObj;

	public TUIMeshSprite shadow;

	public TUIButtonClick downClick;

	public TUIButtonClick backClick;

	private Func<bool> BankEventDelegate;

	private Func<bool> AchieveEventDelegate;

	private Func<bool> GameCentEventDelegate;

	private Func<bool> OptionEventDelegate;

	public TUIBlock blockMap;

	public TUIBlock blockNav;

	private void Start()
	{
		ZS_UIAudioManager.PlayMusic(SoundKind.Mus_map, true);
		TAudioManager.instance.AudioListener.transform.position = Vector3.zero;
		moveSpeed = moveDistance / moveTime;
		blockMap.gameObject.active = false;
		blockNav.gameObject.active = false;
		if (null != navObj)
		{
			pos = navObj.transform.position;
		}
		ChangeBtnState(true);
		SetNavigationBtnDelegate();
	}

	private void ChangeBtnState(bool flag)
	{
		if (flag)
		{
			downClick.gameObject.SetActiveRecursively(true);
			downClick.m_PressObj.SetActiveRecursively(false);
			backClick.gameObject.SetActiveRecursively(false);
		}
		else
		{
			backClick.gameObject.SetActiveRecursively(true);
			backClick.m_PressObj.SetActiveRecursively(false);
			downClick.gameObject.SetActiveRecursively(false);
		}
	}

	private void SetNavigationBtnDelegate()
	{
		SetBankEventHandle(ShowtBank);
		SetAchieveEventHandle(ShowAchievement);
		SetOptionEventHandle(ShowOption);
		SetGameCentEventHandle(ShowGameCent);
	}

	private bool ShowtBank()
	{
		Application.LoadLevel(ZS_TUIMisc.creditScene);
		return true;
	}

	private bool ShowAchievement()
	{
		Application.LoadLevel(ZS_TUIMisc.gloryScene);
		return true;
	}

	private bool ShowOption()
	{
		Application.LoadLevel(ZS_TUIMisc.optionScene);
		return true;
	}

	private bool ShowGameCent()
	{
		return true;
	}

	private void ShowBankPage(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			isMaskShow = false;
			BankEventDelegate();
		}
	}

	private void ShowOptionPage(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok, true);
			isMaskShow = false;
			OptionEventDelegate();
		}
	}

	private void ShowAchievePage(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok, true);
			isMaskShow = false;
			AchieveEventDelegate();
		}
	}

	private void ShowGameCentPage(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok, true);
			MyGameCenter.ShowLeaderboard();
		}
	}

	private void ShowOrHideNavigation(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3 && canNextMove && null != navObj)
		{
			canNextMove = false;
			StartCoroutine(MoveNavigation());
		}
	}

	private IEnumerator MoveNavigation()
	{
		float distance = 0f;
		if (flag)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_topbutton_in);
			blockMap.gameObject.active = true;
			blockNav.gameObject.active = true;
			if (null != shadow)
			{
				shadow.transform.localScale = new Vector3(800f, 800f, 1f);
			}
			ChangeBtnState(false);
			while (true)
			{
				distance += moveSpeed * Time.deltaTime;
				if (distance > moveDistance)
				{
					break;
				}
				ZS_TUIMisc.SetPosition(navObj, pos.y - distance, ZS_TUIMisc.Arrangement.Vertical);
				yield return true;
			}
			ZS_TUIMisc.SetPosition(navObj, pos.y - moveDistance, ZS_TUIMisc.Arrangement.Vertical);
			pos = navObj.transform.position;
			flag = false;
			isMaskShow = true;
			canNextMove = true;
			yield break;
		}
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_topbutton_out);
		blockMap.gameObject.active = false;
		blockNav.gameObject.active = false;
		if (null != shadow)
		{
			shadow.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		}
		ChangeBtnState(true);
		while (true)
		{
			distance += moveSpeed * Time.deltaTime;
			if (distance > moveDistance)
			{
				break;
			}
			ZS_TUIMisc.SetPosition(navObj, pos.y + distance, ZS_TUIMisc.Arrangement.Vertical);
			yield return true;
		}
		ZS_TUIMisc.SetPosition(navObj, pos.y + moveDistance, ZS_TUIMisc.Arrangement.Vertical);
		pos = navObj.transform.position;
		flag = true;
		isMaskShow = false;
		canNextMove = true;
	}

	public void SetBankEventHandle(Func<bool> handle)
	{
		BankEventDelegate = handle;
	}

	public void RemoveBankEventHandle()
	{
		BankEventDelegate = null;
	}

	public void SetAchieveEventHandle(Func<bool> handle)
	{
		AchieveEventDelegate = handle;
	}

	public void RemoveAchieveEventHandle()
	{
		AchieveEventDelegate = null;
	}

	public void SetGameCentEventHandle(Func<bool> handle)
	{
		GameCentEventDelegate = handle;
	}

	public void RemoveGameCentEventHandle()
	{
		GameCentEventDelegate = null;
	}

	public void SetOptionEventHandle(Func<bool> handle)
	{
		OptionEventDelegate = handle;
	}

	public void RemoveOptionEventHandle()
	{
		OptionEventDelegate = null;
	}
}
