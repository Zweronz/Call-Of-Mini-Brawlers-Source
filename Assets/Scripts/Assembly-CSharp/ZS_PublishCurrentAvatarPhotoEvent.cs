public class ZS_PublishCurrentAvatarPhotoEvent
{
	public delegate void GetCurrentAvatarPhoto(ZS_AvatarPhotoInfo avatarPhoto);

	public GetCurrentAvatarPhoto CurrentAvatarPhoto { get; private set; }

	public ZS_PublishCurrentAvatarPhotoEvent(GetCurrentAvatarPhoto del)
	{
		CurrentAvatarPhoto = del;
	}
}
