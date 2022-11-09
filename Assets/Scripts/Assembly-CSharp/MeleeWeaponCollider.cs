using Fight;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MeleeWeaponCollider : MonoBehaviour
{
	public MeleeWeapon weapon;

	public bool Enabled
	{
		get
		{
			return base.GetComponent<Collider>().enabled;
		}
		set
		{
			base.GetComponent<Collider>().enabled = value;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		FightManager.Instance.Add(new MeleeFightBehavior(weapon, weapon.Owner, other.gameObject));
	}
}
