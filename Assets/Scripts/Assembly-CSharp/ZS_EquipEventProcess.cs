using UnityEngine;

public class ZS_EquipEventProcess : MonoBehaviour
{
	private bool rotateFlag = true;

	private float avatarRotateSpeed = 2f;

	public GameObject avatarObj;

	public GameObject equipObj;

	public GameObject itemObj;

	public bool RotateFlag
	{
		get
		{
			return rotateFlag;
		}
		set
		{
			rotateFlag = value;
		}
	}

	private void AvatarRoate(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 2 && null != avatarObj && rotateFlag && avatarObj.gameObject.active)
		{
			avatarObj.transform.Rotate(0f, (0f - avatarRotateSpeed) * wparam, 0f, Space.World);
		}
	}

	private void EquipRotate(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 2 && null != equipObj && rotateFlag && equipObj.gameObject.active)
		{
			equipObj.transform.Rotate(0f, (0f - avatarRotateSpeed) * wparam, 0f, Space.World);
		}
	}

	private void ItemRotate(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 2 && null != itemObj && rotateFlag && itemObj.gameObject.active)
		{
			itemObj.transform.Rotate(0f, (0f - avatarRotateSpeed) * wparam, 0f, Space.World);
		}
	}
}
