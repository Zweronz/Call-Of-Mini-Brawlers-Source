using System.Collections.Generic;
using Event;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
	public List<GameObject> effectPrefab;

	public float minSpeed;

	public float min;

	public float max;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > minSpeed)
		{
			CreateEffect(collision.contacts[0].point, -base.transform.forward);
			EventCenter.Instance.Publish(this, new TapOnScreenEvent());
		}
	}

	private void CreateEffect(Vector3 postion, Vector3 normal)
	{
		if (effectPrefab != null)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(effectPrefab[Random.Range(0, effectPrefab.Count)]);
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = postion;
			gameObject.transform.localScale *= Random.Range(min, max);
			gameObject.transform.rotation = Quaternion.LookRotation(normal);
		}
	}
}
