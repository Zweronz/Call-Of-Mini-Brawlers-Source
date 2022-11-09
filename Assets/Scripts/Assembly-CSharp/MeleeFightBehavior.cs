using System.Collections.Generic;
using Fight;
using UnityEngine;

public class MeleeFightBehavior : IFightBehavior
{
	private MeleeWeapon meleeWeapon;

	private List<Zombie> victims;

	private List<Destructible> destructibleObjs;

	public MeleeFightBehavior(MeleeWeapon meleeWeapon, GameObject owner, params GameObject[] victims)
	{
		this.meleeWeapon = meleeWeapon;
		this.victims = new List<Zombie>();
		destructibleObjs = new List<Destructible>();
		foreach (GameObject gameObject in victims)
		{
			if (gameObject.tag == "Zombie")
			{
				this.victims.Add(gameObject.GetComponent<Zombie>());
				destructibleObjs.Add(null);
			}
			else if (gameObject.tag == "Destructible")
			{
				this.victims.Add(null);
				destructibleObjs.Add(gameObject.GetComponent<Destructible>());
			}
		}
	}

	public void Execute()
	{
		int num = 0;
		foreach (Zombie victim in victims)
		{
			if (null != victim)
			{
				victim.OnHurt(meleeWeapon, CalculateHurt(victim, num));
			}
			else if (null != destructibleObjs[num])
			{
				destructibleObjs[num].OnHurt(meleeWeapon, CalculateHurt(destructibleObjs[num], num));
			}
			num++;
		}
	}

	private float CalculateHurt(Zombie zombie, int index)
	{
		float num = meleeWeapon.Data.damage + ZombieStreetCommon.Random01() * meleeWeapon.Data.extraDamage;
		float num2 = ZombieStreetCommon.Random01();
		if (num2 < meleeWeapon.Data.criticalHitRate)
		{
			num *= meleeWeapon.Data.criticalHitDamage;
		}
		foreach (int damageType in meleeWeapon.Data.damageTypes)
		{
			num *= 1f - zombie.Data.resistance[damageType];
		}
		return num;
	}

	private float CalculateHurt(Destructible destructibleObj, int index)
	{
		float num = meleeWeapon.Data.damage;
		float num2 = ZombieStreetCommon.Random01();
		if (num2 < meleeWeapon.Data.criticalHitRate)
		{
			num *= meleeWeapon.Data.criticalHitDamage;
		}
		return num;
	}
}
