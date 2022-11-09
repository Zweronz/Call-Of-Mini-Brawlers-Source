using UnityEngine;

public class ZombieCreator : Creator<Zombie, ZombieData>
{
	public override Zombie Create(ZombieData data)
	{
		if (!isInitialized)
		{
			Initialize();
		}
		if (prefabsDictionary.ContainsKey(data.data.modelName))
		{
			GameObject gameObject = (GameObject)Object.Instantiate(prefabsDictionary[data.data.modelName]);
			Zombie component = gameObject.GetComponent<Zombie>();
			component.Initialize(data.data, data.baseData);
			return component;
		}
		return null;
	}

	public Zombie Create(ZombieData data, ArenaMissionData.EnemyRate enemyRate)
	{
		if (!isInitialized)
		{
			Initialize();
		}
		if (prefabsDictionary.ContainsKey(data.data.modelName))
		{
			GameObject gameObject = (GameObject)Object.Instantiate(prefabsDictionary[data.data.modelName]);
			Zombie component = gameObject.GetComponent<Zombie>();
			component.Initialize(data.data, data.baseData, enemyRate);
			return component;
		}
		return null;
	}
}
