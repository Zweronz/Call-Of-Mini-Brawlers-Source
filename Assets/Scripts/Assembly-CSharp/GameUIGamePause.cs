using Event;
using UnityEngine;

public class GameUIGamePause : MonoBehaviour
{
	public Animation anim;

	public AnimationClip comein;

	public AnimationClip goout;

	public TUIBlock block;

	public TUIMeshSprite blockBg;

	public ITAudioEvent ok;

	public ITAudioEvent no;

	public ITAudioEvent popup;

	private void Awake()
	{
		block.m_bEnable = false;
		blockBg.gameObject.SetActiveRecursively(false);
	}

	public void Show()
	{
		OpenClikPlugin.Show(false);
		ChartBoostAndroid.showInterstitial(null);
		Time.timeScale = 0f;
		block.m_bEnable = true;
		blockBg.gameObject.SetActiveRecursively(true);
		TUIActiveAnimation tUIActiveAnimation = TUIActiveAnimation.Play(anim, comein.name, TUIDirection.Forward);
		tUIActiveAnimation.callWhenFinished = string.Empty;
	}

	public void Hide()
	{
		OpenClikPlugin.Hide();
		block.m_bEnable = true;
		TUIActiveAnimation tUIActiveAnimation = TUIActiveAnimation.Play(anim, goout.name, TUIDirection.Forward);
		tUIActiveAnimation.callWhenFinished = "GooutEnd";
	}

	private void HandleContinueBtn(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			PlaySfx(no);
			popup.Trigger();
			Hide();
		}
	}

	private void HandleMenuBtn(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			GameObject gameObject = PlaySfx(ok);
			gameObject.transform.parent = null;
			Object.DontDestroyOnLoad(gameObject);
			EventCenter.Instance.Publish(this, new GameCloseEvent(true));
		}
	}

	private void HandleRetreatBtn(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			GameObject gameObject = PlaySfx(ok);
			gameObject.transform.parent = null;
			Object.DontDestroyOnLoad(gameObject);
			EventCenter.Instance.Publish(this, new GameRetreatEvent());
		}
	}

	private void HandlePauseBtn(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			PlaySfx(no);
			popup.Trigger();
			Show();
		}
	}

	private void GooutEnd()
	{
		Time.timeScale = 1f;
		block.m_bEnable = false;
		blockBg.gameObject.SetActiveRecursively(false);
	}

	private GameObject PlaySfx(ITAudioEvent evt)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(evt.gameObject);
		gameObject.GetComponent<ITAudioEvent>().Trigger();
		return gameObject;
	}
}
