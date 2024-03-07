using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollageBoard : MonoBehaviour
{
	public void Dismiss(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			gameObject.SetActive(false);
		}
	}
}
