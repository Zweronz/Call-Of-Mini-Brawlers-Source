using UnityEngine;

public class Laser : MonoBehaviour
{
	public MeshRenderer laserRenderer;

	public Transform laser;

	public int[] colliderLayers;

	public float maxLength;

	private Transform laserPoint;

	public int ColliderLayers
	{
		get
		{
			int num = 0;
			if (colliderLayers != null)
			{
				int[] array = colliderLayers;
				foreach (int num2 in array)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}
	}

	public void Emit(Transform laserPoint)
	{
		this.laserPoint = laserPoint;
	}

	private void Start()
	{
	}

	private void Update()
	{
		_Emit();
	}

	private void _Emit()
	{
		float magnitude = maxLength;
		Ray ray = new Ray(laserPoint.position, laserPoint.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, maxLength, ColliderLayers))
		{
			magnitude = (hitInfo.point - laserPoint.position).magnitude;
		}
		Scale(magnitude);
	}

	private void Scale(float drawLength)
	{
		Vector3 localScale = laser.localScale;
		localScale.y = drawLength;
		laser.localScale = localScale;
		laserRenderer.material.SetTextureScale("_MainTex", new Vector2(1f, drawLength));
	}
}
