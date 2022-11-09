public class ZombieDeadEvent
{
	public enum WeaponType
	{
		Gun = 0,
		MeleeWeapon = 1,
		Item = 2
	}

	public int ZombieID { get; private set; }

	public string WeaponID { get; private set; }

	public WeaponType Type { get; private set; }

	public ArenaMissionData.EnemyRate Rate { get; private set; }

	public ZombieDeadEvent(int zombieId, string weaponID, WeaponType type, ArenaMissionData.EnemyRate rate = null)
	{
		ZombieID = zombieId;
		WeaponID = weaponID;
		Type = type;
		Rate = rate;
	}
}
