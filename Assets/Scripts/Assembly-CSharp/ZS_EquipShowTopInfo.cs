using UnityEngine;

public class ZS_EquipShowTopInfo : MonoBehaviour
{
	public TUILabel label;

	public TUIMeshSprite mSprite;

	public void ShowEquipTopInfo(string _icon, string _name)
	{
		label.Text = _name;
		mSprite.texture = _icon;
	}

	public void ShowItemTopInfo(string _icon, string _name)
	{
		label.TextID = _name;
		mSprite.texture = _icon;
	}
}
