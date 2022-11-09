using System.Collections.Generic;

public class ZS_PublishUsingItemEvent
{
	public delegate void GetUsingItem(List<ZS_ItemInfo> usingItemList);

	public GetUsingItem UsingItemDel { get; private set; }

	public int Count { get; private set; }

	public ZS_PublishUsingItemEvent(GetUsingItem usingItem, int count)
	{
		Count = count;
		UsingItemDel = usingItem;
	}
}
