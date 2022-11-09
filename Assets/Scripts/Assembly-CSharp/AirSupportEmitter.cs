using UnityEngine;

public class AirSupportEmitter : MonoBehaviour
{
	public GameObject firePrefab;

	public float speed;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Fire(AirSupport airSupport)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(firePrefab, base.transform.position, base.transform.rotation);
		Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.velocity = base.transform.forward * speed;
		AirSupportBullet component = gameObject.GetComponent<AirSupportBullet>();
		component.airSupport = airSupport;
	}
}
