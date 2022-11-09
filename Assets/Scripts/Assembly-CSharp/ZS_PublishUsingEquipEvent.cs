using System.Collections.Generic;

public class ZS_PublishUsingEquipEvent
{
	public delegate void GetUsingEquip(List<ZS_EquipmentInfo> usingEquipList);

	public GetUsingEquip UsingEquipDel { get; private set; }

	public int Count { get; private set; }

	public ZS_PublishUsingEquipEvent(GetUsingEquip usingEquip, int count)
	{
		UsingEquipDel = usingEquip;
		Count = count;
	}
}
