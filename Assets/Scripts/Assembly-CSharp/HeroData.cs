using System;
using System.Collections.Generic;

[Serializable]
public class HeroData
{
	public enum UnLockType
	{
		normal = 0,
		level = 1,
		gold = 2,
		crystal = 3
	}

	public int id;

	public string name;

	public float hp;

	public float hpIncrease;

	public float def;

	public float defIncrease;

	public float moveSpeed;

	public float exp;

	public float expM;

	public UnLockType unlock;

	public float price;

	public string modelName;

	public float coefficientOfGold;

	public float coefficientOfExp;

	public List<string> gunsWithBirth;

	public string meleeWeaponWithBirth;

	public float skillCD;

	public string nameId;

	public string descId;

	public string icon;

	public string specialId;
}
