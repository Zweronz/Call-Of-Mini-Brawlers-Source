using UnityEngine;

public class GameUIBullet : MonoBehaviour
{
	[SerializeField]
	protected TUILabel count;

	public void SetBullet(int count, int max)
	{
		this.count.Text = count + "/" + max;
	}
}
