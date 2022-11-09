using UnityEngine;

public class ZS_YingXiongRotate : MonoBehaviour
{
	public Transform avatarObj;

	public float avatarRotateSpeed = 1f;

	private void Start()
	{
	}

	private void AvatarRotate(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 2 && null != avatarObj && avatarObj.gameObject.active)
		{
			avatarObj.Rotate(0f, (0f - avatarRotateSpeed) * wparam, 0f, Space.World);
		}
	}
}
