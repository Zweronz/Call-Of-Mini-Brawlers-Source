using System.Collections.Generic;
using UnityEngine;

public abstract class Creator<T, TData> : MonoBehaviour, ICreator<T, TData>
{
	public List<GameObject> prefabs;

	protected Dictionary<string, GameObject> prefabsDictionary = new Dictionary<string, GameObject>();

	protected bool isInitialized;

	private void Awake()
	{
	}

	public abstract T Create(TData data);

	public virtual List<T> Create(params TData[] datas)
	{
		List<T> list = new List<T>();
		foreach (TData data in datas)
		{
			list.Add(Create(data));
		}
		return list;
	}

	protected void Initialize()
	{
		if (prefabs != null)
		{
			foreach (GameObject prefab in prefabs)
			{
				prefabsDictionary.Add(prefab.name, prefab);
			}
		}
		isInitialized = true;
	}
}
