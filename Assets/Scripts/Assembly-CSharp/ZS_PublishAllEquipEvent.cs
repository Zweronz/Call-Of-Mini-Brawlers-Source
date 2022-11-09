public class ZS_PublishAllEquipEvent
{
	public delegate void GetAllEquip(params ZS_EquipmentInfo[] items);

	public GetAllEquip AllEquipDel { get; private set; }

	public ZS_PublishAllEquipEvent(GetAllEquip del)
	{
		AllEquipDel = del;
	}
}
