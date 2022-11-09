using System.Collections.Generic;
using UnityEngine;

public class RefreshFriendChest : MonoBehaviour
{
	public float refreshDis;

	private Transform beginPoint;

	private Transform target;

	public List<GameObject> chestPrefabs = new List<GameObject>();

	private List<GameCenterModel.FriendScore> fsList = new List<GameCenterModel.FriendScore>();

	private bool instantiate;

	public void Instantiate(Transform beginPoint, Transform target, List<GameCenterModel.FriendScore> fsList)
	{
		this.beginPoint = beginPoint;
		this.target = target;
		if (fsList != null)
		{
			this.fsList.Clear();
			this.fsList.AddRange(fsList);
		}
		instantiate = true;
	}

	private void Update()
	{
		if (instantiate && fsList.Count > 0)
		{
			Vector3 vector = beginPoint.position + beginPoint.forward * fsList[0].score.value;
			if ((target.position - vector).magnitude <= refreshDis)
			{
				Refresh(vector, Quaternion.AngleAxis(180f, beginPoint.up), fsList[0]);
				fsList.RemoveAt(0);
			}
		}
	}

	private void Refresh(Vector3 position, Quaternion rotation, GameCenterModel.FriendScore fs)
	{
		GameObject gameObject = chestPrefabs[ZombieStreetCommon.Random(0, chestPrefabs.Count)];
		if (null != gameObject)
		{
			GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject, position, rotation);
			FriendTreasureChest component = gameObject2.GetComponent<FriendTreasureChest>();
			if (null != component)
			{
				component.SetFriend(fs.friend);
			}
		}
	}
}
