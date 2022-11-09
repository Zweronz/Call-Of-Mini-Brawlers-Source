using UnityEngine;

public class IceBulletEmitter : MonoBehaviour
{
	public CryoGun gun;

	public GameObject iceBulletPrefab;

	public Transform pos;

	public float speed;

	public void Emit()
	{
		GameObject gameObject = CreateBullet();
		gameObject.GetComponent<IceBullet>().owner = gun;
		gameObject.transform.parent = null;
		Vector3 forward = gameObject.transform.forward;
		forward.y = 0f;
		gameObject.transform.forward = forward;
		gameObject.GetComponent<Rigidbody>().AddForce(forward * speed, ForceMode.VelocityChange);
	}

	private GameObject CreateBullet()
	{
		GameObject gameObject = (GameObject)Object.Instantiate(iceBulletPrefab, pos.position, pos.rotation);
		gameObject.transform.parent = pos;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		return gameObject;
	}
}
