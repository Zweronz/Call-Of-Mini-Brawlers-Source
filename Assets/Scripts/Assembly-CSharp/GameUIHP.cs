using UnityEngine;

public class GameUIHP : MonoBehaviour
{
	[SerializeField]
	protected TUISlider slider;

	[SerializeField]
	protected TUILabel hp;

	public void SetHP(float hp, float maxHp)
	{
		slider.sliderValue = hp / maxHp;
		this.hp.Text = (int)hp + "/" + (int)maxHp;
	}
}
