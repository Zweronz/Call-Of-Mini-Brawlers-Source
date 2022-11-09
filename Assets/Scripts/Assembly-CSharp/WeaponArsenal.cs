using System.Collections.Generic;
using UnityEngine;

public class WeaponArsenal : MonoBehaviour
{
	[SerializeField]
	protected List<Gun> guns = new List<Gun>();

	protected MeleeWeapon meleeWeapon;

	private int nextIndex;

	public Gun NextGun
	{
		get
		{
			int num = nextIndex++;
			if (nextIndex >= guns.Count)
			{
				nextIndex = 0;
			}
			if (num >= 0 && num < guns.Count)
			{
				return guns[num];
			}
			return null;
		}
	}

	public MeleeWeapon MeleeWeapon
	{
		get
		{
			return meleeWeapon;
		}
	}

	public void Recycle(Gun gun)
	{
		gun.transform.parent = base.transform;
		gun.transform.localPosition = Vector3.zero;
		gun.Owner = null;
	}

	public void Recycle(MeleeWeapon meleeWeapon)
	{
		meleeWeapon.transform.parent = base.transform;
		meleeWeapon.transform.localPosition = Vector3.zero;
		meleeWeapon.Owner = null;
	}

	public void TakeOver(params Gun[] guns)
	{
		this.guns.AddRange(guns);
		foreach (Gun gun in guns)
		{
			Recycle(gun);
		}
	}

	public void TakeOver(MeleeWeapon meleeWeapon)
	{
		this.meleeWeapon = meleeWeapon;
		Recycle(meleeWeapon);
	}

	public void AddBullet(float rate)
	{
		foreach (Gun gun in guns)
		{
			gun.AddBullet(rate);
		}
	}

	public void ChangeDamage(float rate)
	{
		foreach (Gun gun in guns)
		{
			if (null != gun)
			{
				gun.ChangeDamage(rate);
			}
		}
		if (null != meleeWeapon)
		{
			meleeWeapon.ChangeDamage(rate);
		}
	}

	public void SubDamage(float rate)
	{
		foreach (Gun gun in guns)
		{
			if (null != gun)
			{
				gun.SubDamage(rate);
			}
		}
		if (null != meleeWeapon)
		{
			meleeWeapon.SubDamage(rate);
		}
	}

	public Gun GetNextGunHasBullets()
	{
		Gun gun = null;
		int num = nextIndex;
		do
		{
			gun = NextGun;
			if (num == nextIndex)
			{
				gun = null;
				break;
			}
		}
		while (gun.Bullets <= 0);
		return gun;
	}

	private void Awake()
	{
		if (guns == null)
		{
			guns = new List<Gun>();
		}
	}
}
