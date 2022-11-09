using UnityEngine;

[RequireComponent(typeof(BoneRotate))]
public class LookAtNearestEnemy : MonoBehaviour
{
	public int[] lookAtLayers;

	public float length;

	public Transform face;

	public Transform foot;

	public float interval;

	public float minAngle = 1f;

	public float maxFaceAndFootAngle = 45f;

	private int isLocked;

	private float timer;

	private BoneRotate rotate;

	private bool ISLocked
	{
		get
		{
			return isLocked > 0;
		}
	}

	public int LookAtLayers
	{
		get
		{
			int num = 0;
			if (lookAtLayers != null)
			{
				int[] array = lookAtLayers;
				foreach (int num2 in array)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}
	}

	public void Lock()
	{
		isLocked++;
	}

	public void Unlock()
	{
		if (isLocked > 0)
		{
			isLocked--;
		}
	}

	private void Start()
	{
		rotate = GetComponent<BoneRotate>();
	}

	private GameObject FindNearestEnemy()
	{
		Ray ray = new Ray(foot.position, foot.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, length, LookAtLayers))
		{
			return ZombieStreetCommon.GetGameObjectInRaycastHit(hitInfo)[0];
		}
		return null;
	}

	private bool Do()
	{
		GameObject gameObject = FindNearestEnemy();
		if (null != gameObject)
		{
			FaceTo(gameObject);
		}
		else
		{
			Reback(false);
		}
		return false;
	}

	private void FaceTo(GameObject enemy)
	{
		Vector3 vector = enemy.transform.position - foot.position;
		float num = Vector3.Angle(foot.forward, vector);
		if (num > maxFaceAndFootAngle)
		{
			vector = Quaternion.AngleAxis(maxFaceAndFootAngle, Vector3.Cross(foot.forward, vector)) * foot.forward;
		}
		float num2 = Vector3.Angle(face.forward, vector);
		Vector3 normal = Vector3.Cross(face.forward, vector);
		if (num2 > minAngle)
		{
			Rotate(normal, num2);
		}
	}

	public void Reback(bool immediately)
	{
		float angle = Vector3.Angle(face.forward, foot.forward);
		Vector3 normal = Vector3.Cross(face.forward, foot.forward);
		Rotate(normal, angle, immediately);
	}

	private void Rotate(Vector3 normal, float angle)
	{
		Rotate(normal, angle, false);
	}

	private void Rotate(Vector3 normal, float angle, bool immediately)
	{
		normal.x = 0f;
		normal.z = 0f;
		if (immediately)
		{
			rotate.RotateImmediately(normal, angle);
		}
		else
		{
			rotate.Rotate(normal, angle);
		}
	}

	private void Update()
	{
		if (!ISLocked)
		{
			timer += Time.deltaTime;
			if (timer >= interval)
			{
				Do();
			}
		}
	}

	public void OnDrawGizmos()
	{
		Vector3 position = face.position;
		Vector3 forward = foot.forward;
		Vector3 up = face.up;
		float num = 3f;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(position, position + Quaternion.AngleAxis(maxFaceAndFootAngle, up) * forward * num);
		Gizmos.DrawLine(position, position + Quaternion.AngleAxis(maxFaceAndFootAngle, -up) * forward * num);
	}
}
