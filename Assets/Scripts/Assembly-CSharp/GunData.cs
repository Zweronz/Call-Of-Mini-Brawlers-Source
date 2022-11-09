using System;

[Serializable]
public class GunData : WeaponData
{
	public int bulletType;

	public int maxOfBullets;

	public bool penetrable;

	public int penetrableNumber;

	public float decreaseDamageWhenPenetrate;
}
