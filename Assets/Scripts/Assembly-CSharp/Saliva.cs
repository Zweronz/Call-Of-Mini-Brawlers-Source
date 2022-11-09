using Fight;
using UnityEngine;

public class Saliva : MonoBehaviour
{
	[SerializeField]
	protected float angularSpeed = 40f;

	public float vzx;

	public float vy;

	public GameObject sPrefab;

	private float g;

	public int zombieId;

	public float coefficientOfDamage;

	public void Spout(Transform beginTrans, Vector3 endPoint)
	{
		base.transform.position = beginTrans.position;
		base.transform.rotation = beginTrans.rotation;
		g = GetGravity(beginTrans.position, endPoint, vzx, vy);
		Vector3 force = endPoint - beginTrans.position;
		force.y = 0f;
		force.Normalize();
		force.x *= vzx;
		force.z *= vzx;
		force.y = vy;
		base.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
		base.GetComponent<Rigidbody>().AddForce(new Vector3(0f, base.GetComponent<Rigidbody>().mass * g, 0f), ForceMode.Acceleration);
		base.GetComponent<Rigidbody>().SetMaxAngularVelocity(float.MaxValue);
		base.GetComponent<Rigidbody>().AddRelativeTorque(-base.transform.right * angularSpeed, ForceMode.VelocityChange);
	}

	public static Vector3 VelocityInGravity(Vector3 beginPoint, Vector3 endPoint, float g, float time)
	{
		return new Vector3((endPoint.x - beginPoint.x) / time, (endPoint.y - beginPoint.y) / time - 0.5f * g * time, (endPoint.z - beginPoint.z) / time);
	}

	public static Vector3 VelocityInGravityV(Vector3 beginPoint, Vector3 endPoint, float g, float v)
	{
		Vector3 vector = endPoint - beginPoint;
		float time = Mathf.Abs(new Vector2(vector.x, vector.z).magnitude / v);
		return VelocityInGravity(beginPoint, endPoint, g, time);
	}

	public static float GetGravity(Vector3 beginPoint, Vector3 endPoint, float vzx, float vy)
	{
		Vector3 vector = endPoint - beginPoint;
		float num = Mathf.Abs(new Vector2(vector.x, vector.z).magnitude / vzx);
		return 2f * (vector.y / num - vy) / num;
	}

	private void OnTriggerEnter(Collider other)
	{
		OnTrigger(other);
	}

	private void OnTrigger(Collider other)
	{
		if (other.tag == "Ground")
		{
			Ray ray = new Ray(base.transform.position + base.GetComponent<Rigidbody>().velocity.normalized * -1000000f, base.GetComponent<Rigidbody>().velocity);
			RaycastHit hitInfo;
			if (other.Raycast(ray, out hitInfo, 1E+15f))
			{
				GameObject gameObject = (GameObject)Object.Instantiate(sPrefab);
				gameObject.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
				gameObject.transform.position = hitInfo.point + gameObject.transform.forward * -0.09f;
			}
			Object.Destroy(base.gameObject);
		}
		else if (other.tag == "Hero")
		{
			FightManager.Instance.Add(new ZombieSalivaFightBehavior(zombieId, other.gameObject, coefficientOfDamage));
			Object.Destroy(base.gameObject);
		}
	}
}
