using System.Collections.Generic;
using Event;
using UnityEngine;

public class TreasureChest : Destructible
{
	public GameObject crashPrefab;

	public List<GameObject> treasurePrefabs;

	public override void OnHurt(Gun gun, float damage)
	{
		hp -= damage;
		if (hp <= 0f)
		{
			OnDead();
		}
	}

	public override void OnHurt(MeleeWeapon meleeWeapon, float damage)
	{
		hp -= damage;
		if (hp <= 0f)
		{
			OnDead();
		}
	}

	public override void OnHurt(IItem item, float damage)
	{
		hp -= damage;
		if (hp <= 0f)
		{
			OnDead();
		}
	}

	protected virtual void OnDead()
	{
		EventCenter.Instance.Publish(null, new DestroyChestEvent());
		RefreshTreasure();
		CreateCrash();
		Object.DestroyImmediate(base.gameObject);
	}

	protected void RefreshTreasure()
	{
		GameObject original = treasurePrefabs[ZombieStreetCommon.Random(0, treasurePrefabs.Count)];
		Object.Instantiate(original, base.transform.position, base.transform.rotation);
	}

	protected void CreateCrash()
	{
		Object.Instantiate(crashPrefab, base.transform.position, base.transform.rotation);
	}
}
