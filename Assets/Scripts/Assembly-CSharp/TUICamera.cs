using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class TUICamera : MonoBehaviour
{
	public bool lock960x640;

	public Rect m_viewRect;

	public void Initialize(int layer, int depth)
	{
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		bool flag = TUI.IsRetina();
		bool flag2 = TUI.IsDoubleHD();
		base.GetComponent<Camera>().transform.localPosition = new Vector3(1f / ((!flag) ? 2f : 4f) / (float)((!flag2) ? 1 : 2), -1f / ((!flag) ? 2f : 4f) / (float)((!flag2) ? 1 : 2), 0f);
		base.GetComponent<Camera>().nearClipPlane = -128f;
		base.GetComponent<Camera>().farClipPlane = 128f;
		base.GetComponent<Camera>().orthographic = true;
		base.GetComponent<Camera>().depth = depth;
		base.GetComponent<Camera>().cullingMask = 1 << layer;
		base.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
		m_viewRect = new Rect(0f, 0f, Screen.width, Screen.height);
		if (lock960x640)
		{
			if (Screen.width >= 960 && Screen.height >= 640)
			{
				float left = ((float)Screen.width - 960f) / 2f;
				float top = ((float)Screen.height - 640f) / 2f;
				m_viewRect = new Rect(left, top, 960f, 640f);
			}
			else if (Screen.width >= 640 && Screen.height >= 960)
			{
				float left2 = ((float)Screen.width - 640f) / 2f;
				float top2 = ((float)Screen.height - 960f) / 2f;
				m_viewRect = new Rect(left2, top2, 640f, 960f);
			}
		}
		base.GetComponent<Camera>().pixelRect = m_viewRect;
		base.GetComponent<Camera>().aspect = m_viewRect.width / m_viewRect.height;
		float num = (float)Mathf.Max(Screen.width, Screen.height) * ((!flag) ? 1f : 0.5f);
		float num2 = (float)Mathf.Min(Screen.width, Screen.height) * ((!flag) ? 1f : 0.5f);
		float num3 = 568f / num;
		float num4 = 480f / num;
		float num5 = 384f / num2;
		float num6 = 320f / num2;
		float num7 = num / num5;
		float num8 = num / num6;
		float num9 = num2 / num3;
		float num10 = num2 / num4;
		float num11 = ((num8 >= 480f && num8 <= 568f) ? num6 : ((num7 >= 480f && num7 <= 568f) ? num5 : ((num10 >= 320f && num10 <= 384f) ? num4 : ((!(num9 >= 320f) || !(num9 <= 384f)) ? Mathf.Max(num3, num4, num5, num6) : num3))));
		base.GetComponent<Camera>().orthographicSize = m_viewRect.height / ((!flag) ? 2f : 4f) * num11;
	}
}
