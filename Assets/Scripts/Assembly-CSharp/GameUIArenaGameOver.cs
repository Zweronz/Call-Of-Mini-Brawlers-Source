using Event;
using UnityEngine;

public class GameUIArenaGameOver : MonoBehaviour
{
	public Animation anim;

	public AnimationClip clip;

	public SpecialIAPCrystalLabel2 allgold;

	public TUILabel distance;

	public TUIBlock block;

	public TUIMeshSprite blockBg;

	public ITAudioEvent win;

	public ITAudioEvent lose;

	public ITAudioEvent bgm;

	public ITAudioEvent oksfx;

	public TUIButtonClick challengeBtn;

	private ArenaMissionDetail missionDetial;

	private void Awake()
	{
		block.m_bEnable = false;
		blockBg.gameObject.SetActiveRecursively(false);
		EventCenter.Instance.Register<ArenaGameOverEvent>(HandleArenaGameOverEvent);
		challengeBtn.gameObject.SetActiveRecursively(false);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<ArenaGameOverEvent>(HandleArenaGameOverEvent);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void HandleArenaGameOverEvent(object sender, ArenaGameOverEvent evt)
	{
		bgm.Stop();
		TAudioManager.instance.soundVolume = 0.3f;
		blockBg.gameObject.SetActiveRecursively(true);
		missionDetial = evt.MissionDetail;
		challengeBtn.Disable(missionDetial.MissionDetailData.defeatFriends == null || missionDetial.MissionDetailData.defeatFriends.Count <= 0);
		allgold.SetCrystal((int)(evt.MissionDetail.MissionDetailData.getGold + missionDetial.MissionDetailData.bonus));
		distance.Text = evt.MissionDetail.MissionDetailData.arenaScore + "M";
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

	private void HandleChallengeBtn(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			challengeBtn.Disable(true);
		}
	}
}
