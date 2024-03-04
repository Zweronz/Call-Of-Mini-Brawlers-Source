using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
	public TextAsset en;

	public TextAsset cn;

	public bool hasNoctice;

	public string noticeTitle;

	public string notice;

	public TUIButtonClick startGameBtn;

	public TUISlider loadingSlider;

	public Animation anim;

	public float time = 3f;

	private float timer;

	private bool loading;

	private bool needShowStartBtn;

	public float waitingTime = 0.5f;

	private void Start()
	{
		ZS_UIAudioManager.PlayMusic(SoundKind.Mus_map, true);
		DataCenter.Instance.Achievements.InitializeAllAchievements();
		CalculateAchievements();
		UIDataServer.Instance.Initialize();
		GameCenterModel.Initialize();
		OpenClikPlugin.Initialize("2D15DF4F-9A74-4BAC-AA24-19C1D33746B8");
		TUITextManager.Instance().Parser("lan/" + en.name, "lan/" + cn.name);
		if (Player.Instance.IsNewVersion && hasNoctice)
		{
			new MessageBox(noticeTitle, notice).Show();
		}
		MyGameCenter.Login();
		MyFlurry.Initialize();
		ChartBoostAndroid.init("50ed324016ba47e129000007", "2b2f51f1dc18de41cb9706684bbee16f433d8fd5");
		ChartBoostAndroid.onStart();
		ChartBoostAndroid.cacheInterstitial(null);
		loading = true;
	}

	private void CalculateAchievements()
	{
		List<IAchievement> all = DataCenter.Instance.Achievements.GetAll();
		all.ForEach(delegate(IAchievement achievement)
		{
			achievement.Process();
		});
		DataCenter.Instance.Achievements.InitializeAllAchievements();
	}

	public void HandlePlayBtnClick(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok, true);
			ChartBoostAndroid.showInterstitial(null);
			if (OpenClikPlugin.IsAdReady())
			{
				OpenClikPlugin.Show(true);
			}
			Application.LoadLevel("MapUI");
		}
	}

	private void Update()
	{
		if (loading)
		{
			timer += Time.deltaTime;
			if (timer > time)
			{
				loading = false;
				needShowStartBtn = true;
				loadingSlider.gameObject.SetActiveRecursively(false);
			}
			else
			{
				loadingSlider.sliderValue = timer / time;
			}
		}
		else if (needShowStartBtn)
		{
			timer += Time.deltaTime;
			if (timer > time + waitingTime)
			{
				startGameBtn.transform.localPosition = Vector3.zero;
				anim.Play();
				needShowStartBtn = false;
			}
		}
	}
}
