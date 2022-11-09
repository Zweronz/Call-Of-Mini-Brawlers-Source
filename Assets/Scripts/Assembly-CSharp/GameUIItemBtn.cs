using UnityEngine;

public class GameUIItemBtn : MonoBehaviour
{
	[SerializeField]
	protected TUIMeshSprite icon;

	[SerializeField]
	protected TUILabel count;

	public void SetIcon(string icon)
	{
		this.icon.texture = icon;
	}

	public void SetCount(int count)
	{
		this.count.Text = count.ToString();
	}

	public void Set(string icon, int count)
	{
		SetIcon(icon);
		SetCount(count);
		this.icon.GrayStyle = count == 0;
	}
}
