using System.Collections.Generic;

public class ZS_PublishRongyuEvent
{
	public delegate void GetRongyuInfo(List<ZS_RongyuInfo> list);

	public GetRongyuInfo RongYuInfo { get; private set; }

	public ZS_PublishRongyuEvent(GetRongyuInfo rongYuInfo)
	{
		RongYuInfo = rongYuInfo;
	}
}
