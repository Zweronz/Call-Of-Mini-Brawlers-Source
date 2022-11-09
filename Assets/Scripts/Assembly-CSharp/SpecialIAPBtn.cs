using UnityEngine;

public class SpecialIAPBtn : MonoBehaviour
{
	public void Show()
	{
		base.gameObject.SetActiveRecursively(true);
	}

	public void Hide()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
