using Fight;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
	public CryoGun owner;

	private int collisionCount;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collisionCount == 0)
		{
			Explosion(collision.transform.gameObject);
			collisionCount++;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (collisionCount == 0)
		{
			Explosion(other.transform.gameObject);
			collisionCount++;
		}
	}

	private void Explosion(GameObject other)
	{
		FightManager.Instance.Add(new ShootFightBehavior(owner, owner.Owner, other));
		base.gameObject.SetActiveRecursively(false);
		Object.Destroy(base.gameObject);
	}
}
