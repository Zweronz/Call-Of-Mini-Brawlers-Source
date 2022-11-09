using System.Collections.Generic;
using Fight;
using UnityEngine;

public class ZombieDestructionAttackModel : ZombieAttackModel
{
	public GameObject explosionPrefab;

	public float range;

	public Zombie self;

	public Target target;

	public override void BeginAttack()
	{
	}

	public override void EndAttack()
	{
	}

	private void OnAttackOver()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Hero");
		List<GameObject> list = new List<GameObject>();
		if (array != null)
		{
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if ((gameObject.transform.position - self.transform.position).magnitude <= range)
				{
					list.Add(gameObject);
				}
			}
		}
		if (list.Count > 0)
		{
			FightManager.Instance.Add(new ZombieExplosionFightBehavior(self.Data.id, self.CoefficientOfDamage, self.transform.position, list.ToArray()));
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(explosionPrefab);
		gameObject2.transform.position = self.transform.position;
		gameObject2.transform.rotation = self.transform.rotation;
		Object.Destroy(self.gameObject);
	}
}
