using UnityEngine;

public class ZS_HideObject : MonoBehaviour
{
	private void Start()
	{
		for (int i = 0; i < base.transform.GetChildCount(); i++)
		{
			base.transform.GetChild(i).gameObject.SetActiveRecursively(false);
		}
	}
}
