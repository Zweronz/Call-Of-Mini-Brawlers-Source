using System;

[Serializable]
public class DamageMedicine : Item<DamageMedicineData>
{
	public override void Use(Hero hero)
	{
		hero.UseDamageMedicine(this);
	}
}
