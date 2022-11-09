using System.Collections.Generic;
using Event;
using UnityEngine;

public class EnemyChest : Destructible
{
	public GameObject crashPrefab;

	public List<int> enemies;

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

	private void OnDead()
	{
		EventCenter.Instance.Publish(null, new DestroyDangerousChestEvent());
		RefreshEnemy();
		CreateCrash();
		Object.DestroyImmediate(base.gameObject);
	}

	private void RefreshEnemy()
	{
		EventCenter.Instance.Publish(null, new CreateEnemy(enemies[ZombieStreetCommon.Random(0, enemies.Count)], base.transform.position, base.transform.rotation));
	}

	private void CreateCrash()
	{
		Object.Instantiate(crashPrefab, base.transform.position, base.transform.rotation);
	}
}
