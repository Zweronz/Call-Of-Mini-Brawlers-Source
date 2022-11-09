public class ZS_PublishCurrentAvatarEvent
{
	public delegate void GetCurrentAvatar(ZS_AvatarInfo avatar);

	public GetCurrentAvatar CurrentAvatarDel { get; private set; }

	public ZS_PublishCurrentAvatarEvent(GetCurrentAvatar del)
	{
		CurrentAvatarDel = del;
	}
}
