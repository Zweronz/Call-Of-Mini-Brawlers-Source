using System.Collections.Generic;
using Fight;
using UnityEngine;

public class ShootFightBehavior : IFightBehavior
{
	private Gun gun;

	private List<Zombie> victims;

	private List<Destructible> destructibleObjs;

	private bool isLaserGun;

	private bool isCryoGun;

	public ShootFightBehavior(Gun gun, GameObject owner, params GameObject[] victims)
	{
		this.gun = gun;
		this.victims = new List<Zombie>();
		destructibleObjs = new List<Destructible>();
		foreach (GameObject gameObject in victims)
		{
			if (gameObject.tag == "Zombie")
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (null == component && null != gameObject.transform.root)
				{
					component = gameObject.transform.root.GetComponent<Zombie>();
				}
				this.victims.Add(component);
				destructibleObjs.Add(null);
			}
			else if (gameObject.tag == "Destructible")
			{
				this.victims.Add(null);
				destructibleObjs.Add(gameObject.GetComponent<Destructible>());
			}
		}
		LaserGun laserGun = gun as LaserGun;
		isLaserGun = null != laserGun;
		CryoGun cryoGun = gun as CryoGun;
		isCryoGun = null != cryoGun;
	}

	public void Execute()
	{
		int num = 0;
		foreach (Zombie victim in victims)
		{
			if (null != victim)
			{
				if (isLaserGun)
				{
					victim.OnLaserHurt(gun, CalculateHurt(victim, num) * 0.1f);
				}
				else if (isCryoGun)
				{
					victim.OnFrozenHurt(gun, CalculateHurt(victim, num));
				}
				else
				{
					victim.OnHurt(gun, CalculateHurt(victim, num));
				}
			}
			else if (null != destructibleObjs[num])
			{
				destructibleObjs[num].OnHurt(gun, CalculateHurt(destructibleObjs[num], num));
			}
			num++;
		}
	}

	private float CalculateHurt(Zombie zombie, int index)
	{
		float num = gun.Data.damage + ZombieStreetCommon.Random01() * gun.Data.extraDamage;
		float num2 = ZombieStreetCommon.Random01();
		if (num2 < gun.Data.criticalHitRate)
		{
			num *= gun.Data.criticalHitDamage;
		}
		if (gun.Data.penetrable)
		{
			float num3 = 1f - gun.Data.decreaseDamageWhenPenetrate * (float)index;
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			num *= num3;
		}
		foreach (int damageType in gun.Data.damageTypes)
		{
			num *= 1f - zombie.Data.resistance[damageType];
		}
		return num;
	}

	private float CalculateHurt(Destructible destructibleObj, int index)
	{
		float num = gun.Data.damage;
		float num2 = ZombieStreetCommon.Random01();
		if (num2 < gun.Data.criticalHitRate)
		{
			num *= gun.Data.criticalHitDamage;
		}
		if (gun.Data.penetrable)
		{
			float num3 = 1f - gun.Data.decreaseDamageWhenPenetrate * (float)index;
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			num *= num3;
		}
		return num;
	}
}
