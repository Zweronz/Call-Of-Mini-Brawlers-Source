using System.Collections.Generic;
using Event;
using UnityEngine;

public class TestMissionBtn : MonoBehaviour
{
	public string missionId;

	public int mapId;

	public MissionBoard mBoard;

	private void Start()
	{
	}

	public void HandleBtnClick(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType != 3)
		{
			return;
		}
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
		ChooseMission.missionId = ArenaMission.Instance.ID;
		GameLoading.action = delegate
		{
			GameCenterModel.LoadFriendScores(GameCenterLeaderboardTimeScope.AllTime, 1, 50, "com.trinitigame.callofminibrawlers.l2", true, false, delegate(List<GameCenterModel.FriendScore> fsList)
			{
				ArenaGameLevel.SetFriendScoreList(fsList);
				EventCenter.Instance.Publish(null, new GameLoadingWaittingEndEvent(true));
			});
			return true;
		};
		mBoard.OpenArena();
	}
}
