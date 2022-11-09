using System;
using System.Collections.Generic;

[Serializable]
public class WeaponData
{
	public string id;

	public string name;

	public bool canAppear;

	public int unLockLevel;

	public int requisiteOfLevel;

	public string nextId;

	public string typeName;

	public int gold;

	public int crystal;

	public string modelName;

	public float mass;

	public float damage;

	public float extraDamage;

	public float attackRange;

	public List<int> damageTypes;

	public float interval;

	public float criticalHitRate;

	public float criticalHitDamage;

	public float fend;

	public float stiff;

	public string icon;
}
