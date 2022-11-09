using System;
using UnityEngine;

public class ZombiePhysicsModel : MonoBehaviour
{
	[SerializeField]
	protected GameObject physicsObj;

	[SerializeField]
	protected GameObject followObj;

	[SerializeField]
	protected float speed;

	public float frictionA;

	private float forceSpeed;

	private bool isFending;

	private Action doWhenFendOver;

	private float currentSpeed;

	public float Speed
	{
		get
		{
			return speed;
		}
		set
		{
			currentSpeed -= speed;
			speed = value;
			currentSpeed += speed;
		}
	}

	private void Awake()
	{
		currentSpeed = speed;
	}

	public void OnMove()
	{
		Vector3 forward = physicsObj.transform.forward;
		forward.x = 0f;
		forward.Normalize();
		physicsObj.GetComponent<Rigidbody>().velocity = forward * Mathf.Abs(currentSpeed);
	}

	public void OnStand()
	{
		physicsObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	public void OnFaceTo(Transform trans)
	{
		physicsObj.transform.LookAt(trans);
	}

	public void DecelerateMoveSpeed(float rate)
	{
		currentSpeed -= speed * rate;
	}

	public void RestoreMoveSpeed(float rate)
	{
		currentSpeed += speed * rate;
	}

	public void OnFend(float forceSpeed, Action doWhenFendOver)
	{
		this.doWhenFendOver = doWhenFendOver;
		this.forceSpeed = forceSpeed;
		isFending = true;
	}

	private void FixedUpdate()
	{
		FollowPhysicsObj();
		if (!isFending)
		{
			return;
		}
		forceSpeed -= frictionA * Time.fixedDeltaTime;
		if (forceSpeed < 0f)
		{
			OnFendOver();
			return;
		}
		physicsObj.GetComponent<Rigidbody>().velocity = (0f - forceSpeed) * physicsObj.transform.forward;
		Vector3 forward = physicsObj.transform.forward;
		Vector3 velocity = physicsObj.GetComponent<Rigidbody>().velocity;
		if (forward.x * velocity.x + forward.y * velocity.y + forward.z * velocity.z > 0f)
		{
			OnFendOver();
		}
	}

	private void OnFendOver()
	{
		isFending = false;
		if (doWhenFendOver != null)
		{
			doWhenFendOver();
		}
	}

	private void FollowPhysicsObj()
	{
		Transform parent = physicsObj.transform.parent;
		physicsObj.transform.parent = null;
		followObj.transform.position = physicsObj.transform.position;
		physicsObj.transform.parent = parent;
	}
}
