using System;

[Serializable]
public class AirSupport : Item<AirSupportData>
{
	public override void Use(Hero hero)
	{
		hero.UseAirSupport(this);
	}

	public override bool UseItemSfx()
	{
		return false;
	}
}
