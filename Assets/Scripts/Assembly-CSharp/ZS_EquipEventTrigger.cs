using Event;
using UnityEngine;

public class ZS_EquipEventTrigger : MonoBehaviour
{
	private void TriggerEquipPageEvent()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishEquipEvent());
	}
}
