using UnityEngine;

public class TapjoyBtn : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void HandleTapjoyBtn(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			MyTapjoy.Show();
		}
	}
}
