public class ZS_PublishSpecialIAPEvent
{
	public delegate void GetSpecailIapInfo(ZS_IapInfo info);

	public GetSpecailIapInfo SpecialIAPInfo { get; private set; }

	public int NotifyDay { get; private set; }

	public ZS_PublishSpecialIAPEvent(GetSpecailIapInfo specialIAPInfo, int notifyDay)
	{
		SpecialIAPInfo = specialIAPInfo;
		NotifyDay = notifyDay;
	}
}
