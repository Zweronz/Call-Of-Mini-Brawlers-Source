using UnityEngine;

public class MissionBoard : MonoBehaviour
{
	public TUILabel desc;

	public TUILabel mission;

	public TUILabel distance;

	public TUIMeshSprite shadowMap;

	private bool isArena;

	private void Start()
	{
		Close();
	}

	private void Update()
	{
	}

	public void Open()
	{
		base.gameObject.SetActiveRecursively(true);
		if (null != shadowMap)
		{
			shadowMap.transform.localScale = new Vector3(800f, 800f, 1f);
		}
		SetView(DataCenter.Instance.Missions.Find(ChooseMission.missionId));
		distance.gameObject.SetActiveRecursively(false);
	}

	public void OpenArena()
	{
		base.gameObject.SetActiveRecursively(true);
		if (null != shadowMap)
		{
			shadowMap.transform.localScale = new Vector3(800f, 800f, 1f);
		}
		SetArenaView(ArenaMission.Instance);
		if (0 < Player.Instance.ArenaScore)
		{
			distance.Text = "Best Record: " + Player.Instance.ArenaScore + "M";
		}
		else
		{
			distance.gameObject.SetActiveRecursively(false);
		}
	}

	public void Close()
	{
		base.gameObject.SetActiveRecursively(false);
		if (null != shadowMap)
		{
			shadowMap.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		}
	}

	public void SetView(IMission mission)
	{
		isArena = false;
		desc.SetFormatText(mission.DescId, mission.GetDescData(Player.Instance.GameLevel).ToArray());
		this.mission.SetFormatText("Text006", Player.Instance.GameLevel);
	}

	public void SetArenaView(ArenaMission mission)
	{
		isArena = true;
		desc.SetFormatText(mission.DescId);
		this.mission.SetFormatText("Text075");
	}

	public void HandleOKBtnClick(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_start, true);
			if (isArena)
			{
				GameLoading.loadingScene = "scene0";
			}
			else
			{
				GameLoading.loadingScene = "scene" + DataCenter.Instance.MapPoints.Find(ChooseMission.mapPointId).sceneId;
			}
			Application.LoadLevel("LoadingUI");
		}
	}

	public void HandleCancelBtnClick(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			Close();
		}
	}
}
