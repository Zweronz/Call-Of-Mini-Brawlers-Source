using System;

public class GetReviveIAPEvent
{
	public string iapId;

	public Action<ReviveIAPData> action { get; private set; }

	public GetReviveIAPEvent(string iapId, Action<ReviveIAPData> action)
	{
		this.iapId = iapId;
		this.action = action;
	}
}
