using System;
using System.Collections.Generic;
using UnityEngine;

public class BoneFinder : MonoBehaviour
{
	[Serializable]
	public class BoneQueryData
	{
		public string name;

		public Transform bone;
	}

	public List<BoneQueryData> datas;

	private Dictionary<string, BoneQueryData> bones = new Dictionary<string, BoneQueryData>();

	private bool isInited;

	public BoneQueryData Query(string name)
	{
		return bones[name];
	}

	private void Awake()
	{
		Init();
	}

	public void Init()
	{
		if (isInited)
		{
			return;
		}
		if (datas != null && datas.Count > 0)
		{
			foreach (BoneQueryData data in datas)
			{
				bones.Add(data.name, data);
			}
		}
		isInited = true;
	}
}
