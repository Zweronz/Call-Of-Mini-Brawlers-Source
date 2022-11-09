using System.Collections.Generic;

public class ZS_PublishAllGunEvent
{
	public delegate void GetAllGun(List<ZS_EquipmentInfo> listSward);

	public GetAllGun AllGuns { get; private set; }

	public ZS_PublishAllGunEvent(GetAllGun allGuns)
	{
		AllGuns = allGuns;
	}
}
