using System;

[Serializable]
public class HpMedicine : Item<HpMedicineData>
{
	public override void Use(Hero hero)
	{
		hero.UseHpMedicine(this);
	}
}
