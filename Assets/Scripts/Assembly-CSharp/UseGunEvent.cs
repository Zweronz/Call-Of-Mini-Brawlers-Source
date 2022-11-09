public class UseGunEvent
{
	public string GunID { get; private set; }

	public UseGunEvent(string gunID)
	{
		GunID = gunID;
	}
}
