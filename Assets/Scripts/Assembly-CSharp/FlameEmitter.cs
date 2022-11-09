using UnityEngine;

public class FlameEmitter : MonoBehaviour
{
	public Transform emitPoint;

	public GameObject flamePrefab;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Emit()
	{
		GameObject gameObject = (GameObject)Object.Instantiate(flamePrefab, emitPoint.transform.position, emitPoint.transform.rotation);
		gameObject.AddComponent<AutoDestroyWhenNoChild>();
		gameObject.transform.parent = emitPoint.transform;
	}
}
