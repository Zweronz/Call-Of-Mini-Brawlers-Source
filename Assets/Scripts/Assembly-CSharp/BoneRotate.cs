using UnityEngine;

public class BoneRotate : MonoBehaviour
{
	public Transform bone;

	public float angleSpeed;

	private Vector3 normal;

	private float angle;

	private bool isRotating;

	private float timer;

	private float maxTime;

	public void Rotate(Vector3 normal, float angle)
	{
		this.normal = normal;
		this.angle = angle;
		maxTime = angle / angleSpeed;
		BeginRoate();
	}

	public void RotateImmediately(Vector3 normal, float angle)
	{
		this.normal = normal;
		this.angle = angle;
		RoateBoneImmediately();
		EndRotate();
	}

	private void BeginRoate()
	{
		isRotating = true;
	}

	private void EndRotate()
	{
		timer = 0f;
		maxTime = 0f;
		isRotating = false;
	}

	private void RoateBone(float time)
	{
		if (null != bone)
		{
			bone.Rotate(normal, time * angleSpeed, Space.World);
		}
	}

	private void RoateBoneImmediately()
	{
		if (null != bone)
		{
			bone.Rotate(normal, angle, Space.World);
		}
	}

	private void LateUpdate()
	{
		if (isRotating)
		{
			timer += Time.deltaTime;
			if (timer >= maxTime)
			{
				RoateBone(maxTime - timer + Time.deltaTime);
				EndRotate();
			}
			else
			{
				RoateBone(Time.deltaTime);
			}
		}
	}
}
