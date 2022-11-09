using System.Collections.Generic;
using Fight;
using UnityEngine;

public class ZombieExplosionFightBehavior : IFightBehavior
{
	private int zombieId;

	private Vector3 explosionCenter;

	private List<Hero> targets = new List<Hero>();

	private float coefficientOfDamage = 1f;

	public ZombieExplosionFightBehavior(int zombieId, float coefficientOfDamage, Vector3 explosionCenter, params GameObject[] targets)
	{
		this.zombieId = zombieId;
		this.explosionCenter = explosionCenter;
		this.coefficientOfDamage = coefficientOfDamage;
		foreach (GameObject gameObject in targets)
		{
			Hero component = gameObject.GetComponent<Hero>();
			if (null != component)
			{
				this.targets.Add(component);
			}
		}
	}

	public void Execute()
	{
		if (targets == null || targets.Count <= 0)
		{
			return;
		}
		foreach (Hero target in targets)
		{
			if (null != target)
			{
				target.OnHurt(zombieId, CalculateHurt(zombieId, explosionCenter, target));
			}
		}
	}

	private float CalculateHurt(int zombieId, Vector3 explosionCenter, Hero target)
	{
		float num = target.Data.def + target.Data.defIncrease * (float)Player.Instance.HeroLevel;
		float num2 = DataCenter.Instance.BaseEnemiesHpDmg.Find(Player.Instance.GameLevel).damage * coefficientOfDamage;
		float num3 = num2 - num;
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		return num3 * DataCenter.Instance.BaseEnemies.Find(zombieId).coefficientOfDamage;
	}
}
