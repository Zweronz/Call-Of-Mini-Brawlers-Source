using System.Collections.Generic;

public class ZS_PublishAllItemEvent
{
	public delegate void GetAllItem(List<ZS_ItemInfo> items);

	public GetAllItem AllItemDel { get; private set; }

	public ZS_PublishAllItemEvent(GetAllItem del)
	{
		AllItemDel = del;
	}
}
