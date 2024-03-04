using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
	public TextAsset en;

	public TextAsset cn;

	public static bool init = true;

	public GameObject equip;

	public GameObject achieve;

	private void Awake()
	{
		Time.timeScale = 1f;
		DataCenter.Instance.Achievements.InitializeAllAchievements();
		CalculateAchievements();
		UIDataServer.Instance.Initialize();
		if (init)
		{
			TUITextManager.Instance().Parser("lan/" + en.name, "lan/" + cn.name);
			init = false;
		}
	}

	private void Start()
	{
		ZS_AvatarInfo avatar = ZS_TopInfomation.avatar;
		if (avatar.CanBuyOrUpdate)
		{
			equip.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			equip.GetComponent<Animation>().Play();
		}
		if (avatar.IsAchieveComplete)
		{
			achieve.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			achieve.GetComponent<Animation>().Play();
		}
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
}
