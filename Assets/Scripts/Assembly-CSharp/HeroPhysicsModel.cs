using UnityEngine;

public class HeroPhysicsModel : MonoBehaviour
{
	[SerializeField]
	protected GameObject physicsObj;

	public float speed;

	public float avoidSpeed;

	public void OnMove()
	{
		physicsObj.GetComponent<Rigidbody>().velocity = physicsObj.transform.forward * Mathf.Abs(speed);
	}

	public void OnStand()
	{
		physicsObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	public void OnAvoid()
	{
		physicsObj.GetComponent<Rigidbody>().velocity = physicsObj.transform.forward * Mathf.Abs(avoidSpeed);
	}
}
