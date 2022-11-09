using UnityEngine;

public class SalivaEmitter : MonoBehaviour
{
	public Transform emitterPoint;

	public GameObject salivaPrefab;

	public void Emit(Vector3 endPoint)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(salivaPrefab);
		Saliva component = gameObject.GetComponent<Saliva>();
		component.Spout(emitterPoint, endPoint);
	}
}
