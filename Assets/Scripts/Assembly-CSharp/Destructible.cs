using UnityEngine;

public abstract class Destructible : MonoBehaviour
{
	public float hp;

	public abstract void OnHurt(Gun gun, float damage);

	public abstract void OnHurt(MeleeWeapon meleeWeapon, float damage);

	public abstract void OnHurt(IItem item, float damage);
}
