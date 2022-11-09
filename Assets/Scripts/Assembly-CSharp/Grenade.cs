using System.Collections.Generic;
using Fight;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	[SerializeField]
	protected bool activeWhenAwake = true;

	[SerializeField]
	protected float speed = 3f;

	[SerializeField]
	protected float radius = 3f;

	public RPG owner;

	public Transform firePoint;

	public GameObject firePrefab;

	public GameObject explosionPrefab;

	private int collisionCount;

	public void Fly()
	{
		base.transform.parent = null;
		base.gameObject.SetActiveRecursively(true);
		base.gameObject.AddComponent<Rigidbody>();
		base.GetComponent<Rigidbody>().useGravity = false;
		base.GetComponent<Rigidbody>().AddForce(base.transform.forward * speed, ForceMode.VelocityChange);
		GameObject gameObject = (GameObject)Object.Instantiate(firePrefab, firePoint.position, firePoint.rotation);
		gameObject.transform.parent = firePoint;
		base.GetComponent<Collider>().enabled = true;
	}

	public void Fly(Transform startPoint)
	{
		base.transform.position = startPoint.position;
		base.transform.rotation = startPoint.rotation;
		Fly();
	}

	public void Fly(Vector3 position, Quaternion rotation)
	{
		base.transform.position = position;
		base.transform.rotation = rotation;
		Fly();
	}

	private void Start()
	{
	}

	private void Awake()
	{
		base.GetComponent<Collider>().enabled = false;
		base.gameObject.SetActiveRecursively(activeWhenAwake);
	}

	private void Update()
	{
	}

	private void Explosion(GameObject other)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(explosionPrefab, base.transform.position, Quaternion.identity);
		gameObject.AddComponent<AutoDestroyWhenNoChild>();
		List<RaycastHit> list = new List<RaycastHit>();
		List<GameObject> list2 = new List<GameObject>();
		List<GameObject> list3 = new List<GameObject>();
		Ray ray = new Ray(base.transform.position, base.transform.forward);
		RaycastHit[] array = Physics.RaycastAll(ray, owner.Data.attackRange, owner.AttackLayerMask);
		if (array != null)
		{
			list.AddRange(Physics.RaycastAll(ray, owner.Data.attackRange, owner.AttackLayerMask));
			list2.AddRange(ZombieStreetCommon.GetGameObjectInRaycastHit(list.ToArray()));
		}
		ray = new Ray(base.transform.position, -base.transform.forward);
		array = Physics.RaycastAll(ray, owner.Data.attackRange, owner.AttackLayerMask);
		if (array != null)
		{
			GameObject[] gameObjectInRaycastHit = ZombieStreetCommon.GetGameObjectInRaycastHit(array);
			if (gameObjectInRaycastHit != null)
			{
				GameObject[] array2 = gameObjectInRaycastHit;
				foreach (GameObject item in array2)
				{
					if (!list2.Contains(item))
					{
						list2.Add(item);
					}
				}
				if (null != other && !list2.Contains(other))
				{
					list2.Add(other);
				}
			}
		}
		if (list2.Count >= 0)
		{
			for (int j = 0; j < list2.Count; j++)
			{
				list3.Add(list2[j]);
			}
		}
		if (list3.Count > 0)
		{
			FightManager.Instance.Add(new ShootFightBehavior(owner, owner.Owner, list3.ToArray()));
		}
		base.gameObject.SetActiveRecursively(false);
		Object.Destroy(base.gameObject);
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
}
