using UnityEngine;

public class MissionUICreator : MonoBehaviour
{
	public GameObject slaughterMissionUIPrefab;

	public GameObject surviveMissionUIPrefab;

	public GameObject escapeMissionUIPrefab;

	public GameObject arenaMissionUIPrefab;

	private static MissionUICreator instance;

	public static MissionUICreator Instance
	{
		get
		{
			if (null == instance)
			{
				instance = Create();
			}
			return instance;
		}
	}

	public void CreateSlaughterMissionUI(SlaughterMission mission)
	{
		Transform uIPoint = GetUIPoint();
		GameObject gameObject = (GameObject)Object.Instantiate(slaughterMissionUIPrefab, uIPoint.position, Quaternion.identity);
		gameObject.transform.parent = uIPoint;
		SlaughterMissionUIModel component = gameObject.GetComponent<SlaughterMissionUIModel>();
		gameObject.transform.localPosition = Vector3.zero;
		component.Initialize(mission);
	}

	public void CreateSurviveMissionUI(SurviveMission mission)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(surviveMissionUIPrefab);
		gameObject.transform.parent = GetUIPoint();
		SurviveMissionUIModel component = gameObject.GetComponent<SurviveMissionUIModel>();
		gameObject.transform.localPosition = Vector3.zero;
		component.Initialize(mission);
	}

	public void CreateEscapeMissionUI(EscapeMission mission)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(escapeMissionUIPrefab);
		gameObject.transform.parent = GetUIPoint();
		EscapeMissionUIModel component = gameObject.GetComponent<EscapeMissionUIModel>();
		gameObject.transform.localPosition = Vector3.zero;
		component.Initialize(mission);
	}

	public void CreateArenaMissionUI(ArenaMission mission)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(arenaMissionUIPrefab);
		gameObject.transform.parent = GetUIPoint();
		ArenaMissionUIModel component = gameObject.GetComponent<ArenaMissionUIModel>();
		gameObject.transform.localPosition = Vector3.zero;
		component.Initialize(mission);
	}

	private static MissionUICreator Create()
	{
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("MissionUICreator"));
		return gameObject.GetComponent<MissionUICreator>();
	}

	private static Transform GetUIPoint()
	{
		return GameObject.FindGameObjectWithTag("MissionUIPoint").transform;
	}
}
