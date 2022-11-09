using System;

[Serializable]
public class MeleeWeaponData : WeaponData
{
	public enum Type
	{
		Blunt = 1,
		Sharp = 2
	}

	public Type type;
}
