using UnityEngine;

public class HeadEmitter : MonoBehaviour
{
	public BulletCase head;

	public float minAngularX;

	public float maxAngularX;

	public float minAngularY;

	public float maxAngularY;

	public float minAngularZ;

	public float maxAngularZ;

	private void Start()
	{
		float x = Random.Range(minAngularX, maxAngularX);
		float y = Random.Range(minAngularY, maxAngularY);
		float z = Random.Range(minAngularZ, maxAngularZ);
		head.transform.localRotation = Quaternion.Euler(x, y, z);
		Emit(head.transform.position, head.transform.rotation);
	}

	private void Update()
	{
	}

	private void Emit(Vector3 position, Quaternion rotation)
	{
		head.transform.parent = null;
		head.Fly(position, rotation);
	}
}
