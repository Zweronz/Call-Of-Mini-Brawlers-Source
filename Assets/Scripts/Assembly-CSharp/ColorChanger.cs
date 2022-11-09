using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	public delegate void ColorChangeOver();

	public List<Renderer> renderers;

	private List<Color32> baseColors = new List<Color32>();

	private List<Color32> targetColors = new List<Color32>();

	private ColorChangeOver del;

	private float time;

	private float timer;

	private bool isChanging;

	public void ChangeColorImmediately(List<Color32> colors)
	{
		StopChangeColor();
		DoChangeColor(colors);
	}

	public void StartChangeColor(List<Color32> colors, float time, ColorChangeOver del = null)
	{
		this.del = del;
		this.time = time;
		timer = 0f;
		isChanging = true;
		baseColors.Clear();
		foreach (Renderer renderer in renderers)
		{
			baseColors.Add(renderer.material.color);
		}
		targetColors.Clear();
		targetColors.AddRange(colors);
	}

	private void StopChangeColor()
	{
		time = 0f;
		timer = 0f;
		isChanging = false;
	}

	private void NotifyColorChangeOver()
	{
		if (del != null)
		{
			del();
		}
	}

	private void DoChangeColor(List<Color32> colors)
	{
		for (int i = 0; i < renderers.Count && i < colors.Count; i++)
		{
			if (null != renderers[i].material)
			{
				renderers[i].material.color = colors[i];
			}
		}
	}

	private void LerpChangeColor(float t)
	{
		float t2 = 1f;
		if (time != 0f)
		{
			t2 = Mathf.Clamp01(t / time);
		}
		for (int i = 0; i < renderers.Count && i < targetColors.Count; i++)
		{
			if (null != renderers[i].material)
			{
				renderers[i].material.color = Color32.Lerp(baseColors[i], targetColors[i], t2);
			}
		}
	}

	private void LateUpdate()
	{
		if (isChanging)
		{
			timer += Time.deltaTime;
			LerpChangeColor(timer);
			if (timer >= time)
			{
				StopChangeColor();
				NotifyColorChangeOver();
			}
		}
	}
}
