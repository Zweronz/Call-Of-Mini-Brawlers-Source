public class ZS_PublishGetItemCGInfoEvent
{
	public delegate void PublishItemCGInfo(ZS_ItemCGInfo info);

	public ZS_ItemInfo Info { get; private set; }

	public PublishItemCGInfo CallBack { get; private set; }

	public ZS_PublishGetItemCGInfoEvent(ZS_ItemInfo info, PublishItemCGInfo callBack)
	{
		Info = info;
		CallBack = callBack;
	}
}
