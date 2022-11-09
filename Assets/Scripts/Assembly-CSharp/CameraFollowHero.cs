using UnityEngine;

public class CameraFollowHero : MonoBehaviour
{
	public GameObject hero;

	public float deltaM;

	public float deltaSpeed;

	private Vector3 prePoint;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (null != hero && null != base.GetComponent<Camera>())
		{
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Vector3 position = base.GetComponent<Camera>().transform.position;
			position.z = Mathf.Lerp(base.GetComponent<Camera>().transform.position.z - prePoint.z, hero.transform.forward.z * hero.transform.localScale.z * deltaM, Time.fixedDeltaTime * deltaSpeed) + hero.transform.position.z;
			prePoint = hero.transform.position;
			base.GetComponent<Camera>().transform.position = position;
		}
	}
}
