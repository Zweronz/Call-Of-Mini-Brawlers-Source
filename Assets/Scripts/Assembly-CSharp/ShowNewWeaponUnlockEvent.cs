using System.Collections.Generic;

public class ShowNewWeaponUnlockEvent
{
	public List<string> WeaponIconList { get; private set; }

	public ShowNewWeaponUnlockEvent(List<string> weaponIconList)
	{
		WeaponIconList = weaponIconList;
	}
}
