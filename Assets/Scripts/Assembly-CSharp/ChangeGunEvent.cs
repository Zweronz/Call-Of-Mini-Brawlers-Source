public class ChangeGunEvent
{
	public string Icon { get; private set; }

	public ChangeGunEvent(string icon)
	{
		Icon = icon;
	}
}
