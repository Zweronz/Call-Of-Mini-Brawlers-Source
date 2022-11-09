using System;
using UnityEngine;

public class EscapeMissionAssist : MonoBehaviour
{
	public Transform startPoint;

	public Transform targetPoint;

	public string targetTag;

	public float distance;

	public Action action;

	public bool isStarted;

	public float process;

	public float GetProcess()
	{
		return process;
	}

	private void Update()
	{
		if (!isStarted)
		{
			return;
		}
		if (null == targetPoint)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(targetTag);
			if (null != gameObject)
			{
				targetPoint = gameObject.transform;
			}
		}
		if (!(null != targetPoint))
		{
			return;
		}
		Vector3 vector = targetPoint.position - startPoint.position;
		process = vector.magnitude / Mathf.Abs(distance);
		if (vector.magnitude >= Mathf.Abs(distance))
		{
			isStarted = false;
			if (action != null)
			{
				action();
			}
		}
	}
}
