using System.Collections.Generic;
using Event;
using UnityEngine;

public class ZS_EquipUsingShow : MonoBehaviour
{
	public TUIMeshSprite[] tms;

	public static List<ZS_EquipmentInfo> usingEquipInfo;

	public static int count;

	private void Awake()
	{
		Time.timeScale = 1f;
	}

	private void Start()
	{
		count = tms.Length;
		EventCenter.Instance.Publish(this, new ZS_PublishUsingEquipEvent(GetUsingEquipInfo, count));
		SetUsingEquipInfo();
	}

	private void GetUsingEquipInfo(List<ZS_EquipmentInfo> equipList)
	{
		usingEquipInfo = equipList;
	}

	public void SetUsingEquipInfo()
	{
		for (int i = 0; i < count; i++)
		{
			if (usingEquipInfo[i] != null)
			{
				tms[i].texture = usingEquipInfo[i].Image;
				ZS_EquipPanelBtnDelagate component = tms[i].transform.parent.GetComponent<ZS_EquipPanelBtnDelagate>();
				component.equipInfo = usingEquipInfo[i];
				component.levLab.gameObject.SetActiveRecursively(true);
				component.levLab.Text = "Lv : " + usingEquipInfo[i].level;
			}
			else
			{
				ZS_EquipPanelBtnDelagate component2 = tms[i].transform.parent.GetComponent<ZS_EquipPanelBtnDelagate>();
				tms[i].texture = ZS_TUIMisc.textLan[i];
				component2.equipInfo = null;
				component2.levLab.gameObject.SetActiveRecursively(false);
			}
		}
	}
}
