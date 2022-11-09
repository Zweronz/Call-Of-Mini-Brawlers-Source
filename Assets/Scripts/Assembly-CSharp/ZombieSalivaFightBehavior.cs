using Fight;
using UnityEngine;

public class ZombieSalivaFightBehavior : IFightBehavior
{
	private int zombieId;

	private Hero target;

	private float coefficientOfDamage = 1f;

	public ZombieSalivaFightBehavior(int zombieId, GameObject target, float coefficientOfDamage)
	{
		this.zombieId = zombieId;
		this.target = target.GetComponent<Hero>();
		this.coefficientOfDamage = coefficientOfDamage;
	}

	public void Execute()
	{
		if (null != target)
		{
			target.OnSalivaHurt(zombieId, CalculateHurt(zombieId, target));
		}
	}

	private float CalculateHurt(int zombieId, Hero target)
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
