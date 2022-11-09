public class ZS_PublishIAPCrystalExchangeGoldEvent
{
	public delegate void GetExchanceInfo(ZS_IapToGold info);

	public GetExchanceInfo ExchanceInfo { get; private set; }

	public ZS_PublishIAPCrystalExchangeGoldEvent(GetExchanceInfo ChangeInfo)
	{
		ExchanceInfo = ChangeInfo;
	}
}
