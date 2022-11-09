using UnityEngine;

public class GameUIGold : MonoBehaviour
{
	[SerializeField]
	protected TUILabel label;

	public void SetGold(int gold)
	{
		label.Text = gold.ToString("N0");
	}
}
