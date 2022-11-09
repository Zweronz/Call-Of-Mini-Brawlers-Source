using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletCase : MonoBehaviour
{
	[SerializeField]
	protected float speed = 3f;

	[SerializeField]
	protected float angularSpeed = 40f;

	[SerializeField]
	protected float dismissAfterCollision = 0.5f;

	[SerializeField]
	protected bool deleteNotDismiss;

	private bool isFlying;

	protected float culTime;

	private void Start()
	{
	}

	public void Fly()
	{
		base.gameObject.active = true;
		base.GetComponent<Rigidbody>().AddForce(base.transform.forward * speed, ForceMode.VelocityChange);
		base.GetComponent<Rigidbody>().SetMaxAngularVelocity(float.MaxValue);
		base.GetComponent<Rigidbody>().AddRelativeTorque(-base.transform.right * angularSpeed, ForceMode.VelocityChange);
		isFlying = true;
		culTime = 0f;
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

	private void FixedUpdate()
	{
	}

	private void Update()
	{
		if (isFlying || !base.gameObject.active)
		{
			return;
		}
		culTime += Time.deltaTime;
		if (culTime > dismissAfterCollision)
		{
			if (deleteNotDismiss)
			{
				Object.DestroyImmediate(base.gameObject);
				return;
			}
			base.gameObject.active = false;
			culTime = 0f;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		isFlying = false;
	}
}
