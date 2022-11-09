using System.Collections.Generic;

public class ZS_PublishAllAvatarPhotoInfoEvent
{
	public class Callback
	{
		public List<ZS_AvatarPhotoInfo> avatars;
	}

	public delegate void GetAllAvatarPhotoInfo(List<ZS_AvatarPhotoInfo> avatars);

	public GetAllAvatarPhotoInfo AllAvatarInfoDel { get; private set; }

	public ZS_PublishAllAvatarPhotoInfoEvent(GetAllAvatarPhotoInfo del)
	{
		AllAvatarInfoDel = del;
	}
}
