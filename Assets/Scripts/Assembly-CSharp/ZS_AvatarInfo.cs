public class ZS_AvatarInfo
{
	private ZS_AvatarPhotoInfo currentAvatarPhoto;

	private ZS_Money money;

	private float experience;

	private bool canBuyOrUpdate;

	private bool isAchieveComplete;

	private object data;

	public ZS_Money Money
	{
		get
		{
			return money;
		}
		set
		{
			money = value;
		}
	}

	public ZS_AvatarPhotoInfo CurrentAvatarPhoto
	{
		get
		{
			return currentAvatarPhoto;
		}
		set
		{
			currentAvatarPhoto = value;
		}
	}

	public object Data
	{
		get
		{
			return data;
		}
		set
		{
			data = value;
		}
	}

	public float Experience
	{
		get
		{
			return experience;
		}
		set
		{
			experience = value;
		}
	}

	public bool CanBuyOrUpdate
	{
		get
		{
			return canBuyOrUpdate;
		}
		set
		{
			canBuyOrUpdate = value;
		}
	}

	public bool IsAchieveComplete
	{
		get
		{
			return isAchieveComplete;
		}
		set
		{
			isAchieveComplete = value;
		}
	}
}
