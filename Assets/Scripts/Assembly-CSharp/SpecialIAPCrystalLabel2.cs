using UnityEngine;

public class SpecialIAPCrystalLabel2 : MonoBehaviour
{
	public TUIMeshSprite icon;

	public TUILabel label;

	private float crystal;

	private Vector3 firstLabelPos;

	private void Awake()
	{
		firstLabelPos = label.transform.localPosition;
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
		label.transform.localPosition = firstLabelPos;
		label.transform.position = label.transform.TransformPoint(0f - bounds.extents.x - num, 0f, 0f);
		position.x = label.transform.TransformPoint(bounds.max.x, 0f, 0f).x + num;
		position.z = label.transform.position.z;
		icon.transform.position = position;
	}
}
