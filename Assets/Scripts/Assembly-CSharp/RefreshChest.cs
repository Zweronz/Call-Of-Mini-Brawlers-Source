using System.Collections.Generic;
using UnityEngine;

public class RefreshChest : MonoBehaviour
{
	public float intervalDis;

	public float refreshDis;

	private Transform beginPoint;

	private Transform target;

	private float maxLength;

	private float currentLength;

	public List<GameObject> chestPrefabs = new List<GameObject>();

	private bool instantiate;

	public void Instantiate(Transform beginPoint, Transform target, float maxLength)
	{
		this.beginPoint = beginPoint;
		this.target = target;
		this.maxLength = maxLength;
		instantiate = true;
	}

	private void Awake()
	{
	}

	private void Update()
	{
		if (instantiate && (maxLength == 0f || currentLength + intervalDis + refreshDis < maxLength))
		{
			Vector3 to = target.position - beginPoint.position;
			if (to.magnitude - currentLength >= intervalDis && Vector3.Angle(beginPoint.forward, to) < 90f)
			{
				Refresh(target.position + beginPoint.forward * refreshDis, Quaternion.AngleAxis(180f, beginPoint.up));
				currentLength += intervalDis;
			}
		}
	}

	private void Refresh(Transform point)
	{
		Refresh(point.position, point.rotation);
	}

	private void Refresh(Vector3 position, Quaternion rotation)
	{
		GameObject gameObject = chestPrefabs[ZombieStreetCommon.Random(0, chestPrefabs.Count)];
		if (null != gameObject)
		{
			Object.Instantiate(gameObject, position, rotation);
		}
	}
}
