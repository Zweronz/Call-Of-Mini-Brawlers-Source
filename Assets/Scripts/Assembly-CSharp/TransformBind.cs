using System.Collections.Generic;
using UnityEngine;

public class TransformBind : MonoBehaviour
{
	public List<Transform> trans;

	public Transform target;

	public bool followPosition;

	public bool followRotation;

	private void Awake()
	{
		Bind();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void Bind()
	{
		foreach (Transform tran in trans)
		{
			if (null != tran)
			{
				tran.parent = target;
				if (followPosition)
				{
					tran.localPosition = Vector3.zero;
				}
				if (followRotation)
				{
					tran.localRotation = Quaternion.identity;
				}
			}
		}
	}
}
