using System.Collections.Generic;
using Event;
using UnityEngine;

public class ZS_RongYuEventProcess : MonoBehaviour
{
	public TUIScrollList scrolList;

	public TUIControl honour;

	public TUILabel goldLab;

	public TUILabel tcyLab;

	public TUIClipBinder clipBinder;

	public TUIRect clipBinderRect;

	private List<ZS_RongyuInfo> list;

	private void Start()
	{
		InitialHonourInfo();
	}

	private void GetRongyuInfo(List<ZS_RongyuInfo> rongYuList)
	{
		list = rongYuList;
	}

	public void InitialHonourInfo()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishRongyuEvent(GetRongyuInfo));
		if (!(null != honour) || this.list == null)
		{
			return;
		}
		List<ZS_RongyuShow> list = new List<ZS_RongyuShow>();
		foreach (ZS_RongyuInfo item in this.list)
		{
			TUIControl tUIControl = Object.Instantiate(honour) as TUIControl;
			ZS_RongyuShow component = tUIControl.GetComponent<ZS_RongyuShow>();
			ZS_RongyuReward componentInChildren = component.GetComponentInChildren<ZS_RongyuReward>();
			componentInChildren.bindInfo = item;
			componentInChildren.SetEventHandle(item.callBack);
			component.SetInfo(item);
			scrolList.Add(tUIControl);
			list.Add(component);
		}
		clipBinder.SetClipRect(clipBinderRect);
	}
}
