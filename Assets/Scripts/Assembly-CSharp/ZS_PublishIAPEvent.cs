using System.Collections.Generic;

public class ZS_PublishIAPEvent
{
	public delegate void GetAllIapInfo(List<ZS_IapInfo> list);

	public GetAllIapInfo allIAPInfo { get; private set; }

	public ZS_PublishIAPEvent(GetAllIapInfo IAPInfos)
	{
		allIAPInfo = IAPInfos;
	}
}
