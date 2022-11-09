using System.Collections.Generic;

public class ZS_PublishAllSwardEvent
{
	public delegate void GetAllSward(List<ZS_EquipmentInfo> listSward);

	public GetAllSward AllSwards { get; private set; }

	public ZS_PublishAllSwardEvent(GetAllSward allSwards)
	{
		AllSwards = allSwards;
	}
}
