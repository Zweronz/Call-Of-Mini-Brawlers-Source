using UnityEngine;

public abstract class MissionUIModel<TMission, TMissionData> : MonoBehaviour where TMission : Mission<TMissionData> where TMissionData : MissionData
{
	public abstract void Initialize(TMission mission);
}
