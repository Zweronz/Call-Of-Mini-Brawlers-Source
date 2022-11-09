using UnityEngine;

public class AutoDestroyWhenNoChild : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.transform.childCount == 0)
		{
			Object.DestroyImmediate(base.gameObject);
		}
	}
}
