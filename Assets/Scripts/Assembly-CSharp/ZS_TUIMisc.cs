using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZS_TUIMisc
{
	public enum Arrangement
	{
		Horizontal = 0,
		Vertical = 1
	}

	public const int maxLevel = 100;

	public const float maxHit = 1000f;

	public const float maxChance = 100f;

	public const float maxBack = 10f;

	public const int maxAttack = 500;

	public const int maxAmmo = 800;

	public static string indexScene = "MapUI";

	public static string equipScene = "EquipmentUI";

	public static string optionScene = "OptionUI";

	public static string heroScene = "YingXiongUI";

	public static string creditScene = "CreditsUI";

	public static string gloryScene = "RongYuUI";

	public static Stack<string> stack;

	public static string[] textLan = new string[4] { "wuqi", "wuqi", "wuqi", "wuqi" };

	public static string[] textLv = new string[4] { "lv1", "lv2", "lv3", "lv4" };

	public static string[] loadingImgs = new string[11]
	{
		"1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
		"11"
	};

	public static void SetLabel(TUILabel label, string text)
	{
		if (null != label)
		{
			label.Text = text;
		}
	}

	public static void SetImage(TUIMeshSprite meshSprite, string image)
	{
		if (null != meshSprite)
		{
			meshSprite.texture = image;
			meshSprite.NeedUpdate = true;
		}
	}

	public static void ShowGameObjectRecursively(GameObject ob, bool show)
	{
		if (ob != null)
		{
			ob.SetActiveRecursively(show);
		}
	}

	public static void ShowGameObjectRecursively(GameObject ob, Vector3 showPositon, Vector3 hidePosition, bool show)
	{
		if (ob != null)
		{
			if (show)
			{
				ob.transform.localPosition = showPositon;
			}
			else
			{
				ob.transform.localPosition = hidePosition;
			}
		}
	}

	public static void SetPosition(GameObject go, float current, Arrangement arrangement)
	{
		switch (arrangement)
		{
		case Arrangement.Horizontal:
			go.transform.position = new Vector3(current, go.transform.position.y, go.transform.position.z);
			break;
		case Arrangement.Vertical:
			go.transform.position = new Vector3(go.transform.position.x, current, go.transform.position.z);
			break;
		}
	}

	public static IEnumerator ChangeNumCoroutine(TUILabel label, double addValue, int perCount, double startValue)
	{
		double speed = addValue / (double)perCount;
		double totalValue = startValue + addValue;
		GameObject audioObj = null;
		TAudioEffectRandom clip = null;
		if (null == audioObj)
		{
			audioObj = Object.Instantiate(Resources.Load("SoundEvent/UI_count")) as GameObject;
			clip = audioObj.GetComponent<TAudioEffectRandom>();
			if ((bool)clip)
			{
				clip.loopMode = TAudioEffectRandom.LoopMode.MultiLoop;
				clip.Trigger();
			}
		}
		while (true)
		{
			startValue += speed;
			if (startValue >= totalValue)
			{
				break;
			}
			SetLabel(label, FormatToString((int)startValue));
			yield return true;
		}
		SetLabel(label, FormatToString((int)totalValue));
		if (null != audioObj)
		{
			if (clip.isPlaying)
			{
				clip.Stop();
			}
			Object.Destroy(audioObj);
		}
	}

	public static void SetClipImage(TUIMeshSprite sprite, Arrangement arrangement, float percent, Vector2 size, Vector3 position)
	{
		if (sprite != null)
		{
			if (arrangement == Arrangement.Horizontal)
			{
				position.x += (percent - 1f) * size.x;
			}
			else
			{
				position.y += (percent - 1f) * size.y;
			}
			sprite.transform.position = position;
			sprite.NeedUpdate = true;
		}
	}

	public static void ResetButtonSelectState(GameObject ob, int index)
	{
		if (!(ob != null))
		{
			return;
		}
		TUIButtonSelect[] componentsInChildren = ob.GetComponentsInChildren<TUIButtonSelect>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (i != index && componentsInChildren[i].IsSelected())
			{
				componentsInChildren[i].SetSelected(false);
				break;
			}
		}
	}

	public static void DestoryObjectChild(Transform parent)
	{
		for (int i = 0; i < parent.GetChildCount(); i++)
		{
			Object.Destroy(parent.GetChild(i).gameObject);
		}
	}

	public static string FormatToString(object num)
	{
		return string.Format("{0:n0}", num);
	}

	public static void SetLabsFontById(TUILabel lab, string textId, params object[] content)
	{
		if (content.Length > 0)
		{
			lab.SetFormatText(textId, content);
		}
		else
		{
			lab.TextID = textId;
		}
	}

	public static void Push(string obj)
	{
		if (stack != null)
		{
			stack = new Stack<string>();
			stack.Push(obj);
		}
		else
		{
			stack.Push(obj);
		}
	}

	public static string Pop()
	{
		string text = stack.Pop();
		if (text != null)
		{
			return text;
		}
		return indexScene;
	}
}
