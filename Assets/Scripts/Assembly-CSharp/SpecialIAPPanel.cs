using UnityEngine;

public class SpecialIAPPanel : MonoBehaviour
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
