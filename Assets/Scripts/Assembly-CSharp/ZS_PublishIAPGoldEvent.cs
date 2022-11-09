using System.Collections.Generic;

public class ZS_PublishIAPGoldEvent
{
	public delegate void GetAllIapGoldInfo(List<ZS_IapGoldInfo> list);

	public GetAllIapGoldInfo allIAPGoldInfo { get; private set; }

	public ZS_PublishIAPGoldEvent(GetAllIapGoldInfo IAPGoldInfos)
	{
		allIAPGoldInfo = IAPGoldInfos;
	}
}
