using UnityEngine;

public class FlameLightEffect : MonoBehaviour
{
	public Transform effectPoint;

	public Transform flamePoint;

	public GameObject effectPrefab;

	public void Emit()
	{
		Emit(effectPoint);
	}

	public void Emit(Transform effectPoint)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(effectPrefab, new Vector3(flamePoint.position.x, effectPoint.position.y, flamePoint.position.z), effectPoint.transform.rotation);
		gameObject.transform.parent = effectPoint;
	}
}
