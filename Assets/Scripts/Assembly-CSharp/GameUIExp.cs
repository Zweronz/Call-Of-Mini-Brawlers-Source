using UnityEngine;

public class GameUIExp : MonoBehaviour
{
	[SerializeField]
	protected TUISlider slider;

	public void SetExp(double current, double max)
	{
		if (max < 0.0)
		{
			slider.sliderValue = 1f;
		}
		else
		{
			slider.sliderValue = (float)(current / max);
		}
	}
}
