public class ZS_PublishSpecialIAPResultEvent
{
	public int IapEventResult { get; private set; }

	public ZS_PublishSpecialIAPResultEvent(int IAPResult)
	{
		IapEventResult = IAPResult;
	}
}
