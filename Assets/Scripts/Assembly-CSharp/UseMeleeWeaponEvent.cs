public class UseMeleeWeaponEvent
{
	public string MeleeWeaponID { get; private set; }

	public UseMeleeWeaponEvent(string meleeWeaponID)
	{
		MeleeWeaponID = meleeWeaponID;
	}
}
