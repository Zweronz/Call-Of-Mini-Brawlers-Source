using System.Collections.Generic;
using UnityEngine;

public class ChooseMission : MonoBehaviour
{
	public GameObject missionBtnPrefab;

	public Transform missionTrans;

	public MissionBoard board;

	public TUISelfAdaptiveAnchorGroup anchorGroup;

	public static string missionId;

	public static int mapPointId;

	public int count = 3;

	private List<int> mapPoints = new List<int>();

	private List<IMission> choosedMissions = new List<IMission>();

	private List<IMission> missions = new List<IMission>();

	private List<IMission> samePriorityMissions = new List<IMission>();

	private void Awake()
	{
		Time.timeScale = 1f;
	}

	private void Start()
	{
		foreach (KeyValuePair<int, string> mission in Player.Instance.Missions)
		{
			if (!DataCenter.Instance.MapPoints.Contain(mission.Key))
			{
				Player.Instance.NeedRefreshMission = true;
				break;
			}
		}
		if (Player.Instance.NeedRefreshMission)
		{
			Player.Instance.ClearMission();
			int num = Random.Range(1, count);
			CreateMissions(num);
			RandomMapPoints(num);
			for (int i = 0; i < mapPoints.Count && i < choosedMissions.Count; i++)
			{
				Player.Instance.AddMission(mapPoints[i], choosedMissions[i].ID);
			}
			Player.Instance.NeedRefreshMission = false;
		}
		Player.Instance.Save();
		LayoutMap();
	}

	private void RandomMapPoints(int count)
	{
		mapPoints.Clear();
		List<MapPointData> list = DataCenter.Instance.MapPoints.FindAll((MapPointData data) => true);
		int num = count;
		while (num > 0 && list.Count > 0)
		{
			int index = Random.Range(0, list.Count);
			mapPoints.Add(list[index].id);
			list.RemoveAt(index);
			num--;
		}
	}

	private void LayoutMap()
	{
		anchorGroup.trans.Clear();
		foreach (KeyValuePair<int, string> mission in Player.Instance.Missions)
		{
			MapPointData mapPointData = DataCenter.Instance.MapPoints.Find(mission.Key);
			Vector2 vector = LeftTop2Center(mapPointData.x, mapPointData.y);
			GameObject gameObject = (GameObject)Object.Instantiate(missionBtnPrefab, new Vector3(vector.x, vector.y, -2f), Quaternion.identity);
			MissionBtn component = gameObject.GetComponent<MissionBtn>();
			component.board = board;
			component.mapPointId = mission.Key;
			component.SetMission(DataCenter.Instance.Missions.Find(mission.Value));
			component.transform.parent = missionTrans;
			anchorGroup.trans.Add(gameObject.transform);
		}
		anchorGroup.Anchor();
	}

	private Vector2 LeftTop2Center(float x, float y)
	{
		Vector2 result = default(Vector2);
		result.x = x - 240f;
		result.y = 160f - y;
		return result;
	}

	private void CreateMissions(int count)
	{
		int num = count;
		choosedMissions.Clear();
		missions.Clear();
		samePriorityMissions.Clear();
		missions.AddRange(DataCenter.Instance.Missions.FindAll(PredicateMission));
		missions.Sort(SortMission);
		while (num > 0)
		{
			if (samePriorityMissions.Count > 0)
			{
				foreach (IMission samePriorityMission in samePriorityMissions)
				{
					missions.Remove(samePriorityMission);
				}
			}
			samePriorityMissions.Clear();
			if (missions.Count > 0)
			{
				samePriorityMissions.AddRange(missions.FindAll(PredicatePriorityMission));
			}
			if (samePriorityMissions.Count == 0)
			{
				break;
			}
			samePriorityMissions = ZombieStreetCommon.RandomSortList(samePriorityMissions);
			for (int i = 0; i < samePriorityMissions.Count; i++)
			{
				if (num > 0)
				{
					choosedMissions.Add(samePriorityMissions[i]);
					num--;
				}
			}
		}
	}

	private bool PredicateMission(IMission mission)
	{
		return mission.IsAvailable(Player.Instance.GameLevel);
	}

	private bool PredicatePriorityMission(IMission mission)
	{
		return mission.Priority == missions[0].Priority;
	}

	private int SortMission(IMission mission1, IMission mission2)
	{
		if (mission1.Priority > mission2.Priority)
		{
			return -1;
		}
		if (mission1.Priority < mission2.Priority)
		{
			return 1;
		}
		return 0;
	}

	private void TriggerEquip(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok, true);
			Application.LoadLevel(ZS_TUIMisc.equipScene);
		}
	}

	private void TriggerHero(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok, true);
			Application.LoadLevel(ZS_TUIMisc.heroScene);
		}
	}

	private void TriggerMiss(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok, true);
			Application.LoadLevel(ZS_TUIMisc.gloryScene);
		}
	}
}
