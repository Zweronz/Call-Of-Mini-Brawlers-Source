using UnityEngine;

public class SpecialIAPCrystalLabel : MonoBehaviour
{
	public TUIMeshSprite icon;

	public TUILabel label;

	private float crystal;

	private Vector3 firstLabelPos;

	private void Awake()
	{
		firstLabelPos = label.transform.position;
	}

	public void SetCrystal(float crystal)
	{
		this.crystal = crystal;
		SetIconLabelPos();
		label.Text = crystal.ToString();
	}

	private void SetIconLabelPos()
	{
		string str = crystal.ToString();
		Bounds bounds = label.CalculateBounds(str);
		float num = icon.texInfo.rect.width / 2f * icon.transform.localScale.x;
		if (TUI.IsRetina())
		{
			num /= 2f;
		}
		Vector3 position = icon.transform.position;
		label.transform.position = firstLabelPos;
		label.transform.position = label.transform.TransformPoint(bounds.extents.x + num, 0f, 0f);
		position.x = label.transform.TransformPoint(bounds.min.x, 0f, 0f).x - num;
		position.z = label.transform.position.z;
		icon.transform.position = position;
	}
}
