using UnityEngine;

public class HeroCreator : Creator<Hero, HeroData>
{
	public override Hero Create(HeroData data)
	{
		if (!isInitialized)
		{
			Initialize();
		}
		GameObject gameObject = (GameObject)Object.Instantiate(prefabsDictionary[data.modelName]);
		return gameObject.GetComponent<Hero>();
	}
}
