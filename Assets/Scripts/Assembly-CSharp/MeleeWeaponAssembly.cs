using UnityEngine;

public class MeleeWeaponAssembly : Creator<MeleeWeapon, MeleeWeaponData>
{
	public override MeleeWeapon Create(MeleeWeaponData data)
	{
		if (!isInitialized)
		{
			Initialize();
		}
		if (prefabsDictionary.ContainsKey(data.modelName))
		{
			GameObject gameObject = (GameObject)Object.Instantiate(prefabsDictionary[data.modelName]);
			MeleeWeapon component = gameObject.GetComponent<MeleeWeapon>();
			component.Initialize(data);
			return component;
		}
		return null;
	}
}
