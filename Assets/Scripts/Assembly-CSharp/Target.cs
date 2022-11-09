using UnityEngine;

public class Target : MonoBehaviour
{
	public GameObject target;

	private void OnFocus(GameObject target)
	{
		this.target = target;
	}
}
