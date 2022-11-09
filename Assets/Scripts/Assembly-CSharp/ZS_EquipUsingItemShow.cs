using System.Collections.Generic;
using Event;
using UnityEngine;

public class ZS_EquipUsingItemShow : MonoBehaviour
{
	public TUIMeshSprite[] tms;

	public TUILabel[] counts;

	public static int count;

	public static List<ZS_ItemInfo> usingItemInfo;

	private void Start()
	{
		count = tms.Length;
		EventCenter.Instance.Publish(this, new ZS_PublishUsingItemEvent(GetUsingItem, count));
		SetUsingItemInfo();
	}

	private void GetUsingItem(List<ZS_ItemInfo> usingItem)
	{
		usingItemInfo = usingItem;
	}

	public void SetUsingItemInfo()
	{
		for (int i = 0; i < count; i++)
		{
			if (usingItemInfo[i] != null)
			{
				tms[i].texture = usingItemInfo[i].Image + "_y";
				counts[i].gameObject.SetActiveRecursively(true);
				counts[i].Text = usingItemInfo[i].Count.ToString();
				ZS_EquipPanelItemDelagate component = tms[i].transform.parent.GetComponent<ZS_EquipPanelItemDelagate>();
				component.itemInfo = usingItemInfo[i];
			}
			else
			{
				tms[i].texture = ZS_TUIMisc.textLv[i];
				counts[i].gameObject.SetActiveRecursively(false);
				ZS_EquipPanelItemDelagate component2 = tms[i].transform.parent.GetComponent<ZS_EquipPanelItemDelagate>();
				component2.itemInfo = null;
			}
		}
	}

	private bool EnterItemPage(ZS_ItemInfo info)
	{
		if (info != null)
		{
			return true;
		}
		return false;
	}
}
