using Event;
using UnityEngine;

public class GameUIGameOver : MonoBehaviour
{
	public Animation anim;

	public AnimationClip clip;

	public TUIMeshSprite gameResult;

	public TUILabel kills;

	public TUILabel gold;

	public TUILabel bonus;

	public SpecialIAPCrystalLabel allgold;

	public TUIBlock block;

	public TUIMeshSprite blockBg;

	public ITAudioEvent win;

	public ITAudioEvent lose;

	public ITAudioEvent bgm;

	public ITAudioEvent oksfx;

	private void Awake()
	{
		block.m_bEnable = false;
		blockBg.gameObject.SetActiveRecursively(false);
		EventCenter.Instance.Register<GameOverEvent>(HandleGameOverEvent);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<GameOverEvent>(HandleGameOverEvent);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void HandleGameOverEvent(object sender, GameOverEvent evt)
	{
		bgm.Stop();
		TAudioManager.instance.soundVolume = 0.3f;
		if (evt.Completed)
		{
			gameResult.texture = "COMPLETE";
			win.Trigger();
		}
		else
		{
			gameResult.texture = "failed";
			lose.Trigger();
		}
		blockBg.gameObject.SetActiveRecursively(true);
		allgold.SetCrystal(evt.AllGold);
		gold.Text = evt.Gold.ToString();
		kills.Text = evt.Kills.ToString();
		bonus.Text = evt.Bonus.ToString();
		block.m_bEnable = true;
		OpenClikPlugin.Show(false);
		ChartBoostAndroid.showInterstitial(null);
		TUIActiveAnimation.Play(anim, clip.name, TUIDirection.Forward);
	}

	private void HandleOKBtn(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			TAudioManager.instance.soundVolume = 1f;
			oksfx.Trigger();
			oksfx.transform.parent = null;
			Object.DontDestroyOnLoad(oksfx.gameObject);
			EventCenter.Instance.Publish(this, new GameCloseEvent(false));
		}
	}
}
