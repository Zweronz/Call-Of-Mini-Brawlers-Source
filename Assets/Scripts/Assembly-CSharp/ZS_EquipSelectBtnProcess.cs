using UnityEngine;

public class ZS_EquipSelectBtnProcess : MonoBehaviour
{
	private void TriggerSelectEquip(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			ZS_EquipBtnShowInfo component = control.GetComponent<ZS_EquipBtnShowInfo>();
			component.NotifySelectEvent();
		}
	}

	private void TriggerSelectItem(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			ZS_EquipBtnItemShowInfo component = control.GetComponent<ZS_EquipBtnItemShowInfo>();
			component.NotifySelectEvent();
		}
	}
}
