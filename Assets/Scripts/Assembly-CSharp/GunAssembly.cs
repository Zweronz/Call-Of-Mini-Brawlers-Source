using UnityEngine;

public class GunAssembly : Creator<Gun, GunData>
{
	public override Gun Create(GunData data)
	{
		if (!isInitialized)
		{
			Initialize();
		}
		if (prefabsDictionary.ContainsKey(data.modelName))
		{
			GameObject gameObject = (GameObject)Object.Instantiate(prefabsDictionary[data.modelName]);
			Gun component = gameObject.GetComponent<Gun>();
			component.Initialize(data);
			return component;
		}
		return null;
	}
}
