public class ZS_PublishIAPResultEvent
{
	public int IapEventResult { get; private set; }

	public ZS_PublishIAPResultEvent(int IAPResult)
	{
		IapEventResult = IAPResult;
	}
}
