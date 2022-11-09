using System;

[Serializable]
public class BulletPackage : Item<BulletPackageData>
{
	public override void Use(Hero hero)
	{
		hero.UseBulletPackage(this);
	}
}
