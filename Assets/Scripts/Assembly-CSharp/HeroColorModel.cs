using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class HeroColorModel : MonoBehaviour
{
	public List<Color32> normalColors;

	public List<Color32> angryColors;

	public float angryColorChangeTime = 0.1f;

	private ColorChanger changer;

	private bool isAngry;

	private bool isAngry2NormalState;

	private void Awake()
	{
		changer = GetComponent<ColorChanger>();
	}

	public void BeNormal()
	{
		if (isAngry)
		{
			isAngry = false;
		}
	}

	public void BeAngry()
	{
		if (!isAngry)
		{
			changer.StartChangeColor(angryColors, angryColorChangeTime, AngryPingpang);
			isAngry2NormalState = false;
			isAngry = true;
		}
	}

	public void AngryPingpang()
	{
		if (!isAngry2NormalState)
		{
			changer.StartChangeColor(normalColors, angryColorChangeTime, AngryPingpang);
			isAngry2NormalState = true;
		}
		else if (isAngry)
		{
			changer.StartChangeColor(angryColors, angryColorChangeTime, AngryPingpang);
			isAngry2NormalState = false;
		}
	}
}
