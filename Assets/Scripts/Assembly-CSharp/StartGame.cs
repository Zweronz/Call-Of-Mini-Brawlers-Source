using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StartGame : MonoBehaviour
{
	public TextAsset en;

	public TextAsset cn;

	public static bool init = true;

	public GameObject equip;

	public GameObject achieve;

	[CompilerGenerated]
	private static Action<IAchievement> _003C_003Ef__am_0024cache5;

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
		if (_003C_003Ef__am_0024cache5 == null)
		{
			_003C_003Ef__am_0024cache5 = _003CCalculateAchievements_003Em__5B;
		}
		all.ForEach(_003C_003Ef__am_0024cache5);
		DataCenter.Instance.Achievements.InitializeAllAchievements();
	}

	[CompilerGenerated]
	private static void _003CCalculateAchievements_003Em__5B(IAchievement achievement)
	{
		achievement.Process();
	}
}
