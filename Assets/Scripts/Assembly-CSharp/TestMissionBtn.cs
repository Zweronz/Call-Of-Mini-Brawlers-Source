using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Event;
using UnityEngine;

public class TestMissionBtn : MonoBehaviour
{
	public string missionId;

	public int mapId;

	public MissionBoard mBoard;

	[CompilerGenerated]
	private static Func<bool> _003C_003Ef__am_0024cache3;

	[CompilerGenerated]
	private static Action<List<GameCenterModel.FriendScore>> _003C_003Ef__am_0024cache4;

	private void Start()
	{
	}

	public void HandleBtnClick(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			ChooseMission.missionId = ArenaMission.Instance.ID;
			if (_003C_003Ef__am_0024cache3 == null)
			{
				_003C_003Ef__am_0024cache3 = _003CHandleBtnClick_003Em__43;
			}
			GameLoading.action = _003C_003Ef__am_0024cache3;
			mBoard.OpenArena();
		}
	}

	[CompilerGenerated]
	private static bool _003CHandleBtnClick_003Em__43()
	{
		if (_003C_003Ef__am_0024cache4 == null)
		{
			_003C_003Ef__am_0024cache4 = _003CHandleBtnClick_003Em__44;
		}
		GameCenterModel.LoadFriendScores(GameCenterLeaderboardTimeScope.AllTime, 1, 50, "com.trinitigame.callofminibrawlers.l2", true, false, _003C_003Ef__am_0024cache4);
		return true;
	}

	[CompilerGenerated]
	private static void _003CHandleBtnClick_003Em__44(List<GameCenterModel.FriendScore> fsList)
	{
		ArenaGameLevel.SetFriendScoreList(fsList);
		EventCenter.Instance.Publish(null, new GameLoadingWaittingEndEvent(true));
	}
}
