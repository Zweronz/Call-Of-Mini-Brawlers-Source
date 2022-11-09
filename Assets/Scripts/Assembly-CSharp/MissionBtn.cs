using UnityEngine;

public class MissionBtn : MonoBehaviour
{
	public MissionBoard board;

	public TUIMeshSprite missionIcon;

	public Animation anim;

	public int mapPointId;

	public IMission mission;

	public void SetMission(IMission mission)
	{
		this.mission = mission;
		missionIcon.texture = mission.Icon;
		anim.Play();
	}

	public void HandleBtnClick(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
			if (mission != null)
			{
				ChooseMission.missionId = mission.ID;
				ChooseMission.mapPointId = mapPointId;
				board.Open();
			}
		}
	}
}
