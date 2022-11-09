using System;

public class ZS_AvatarPhotoInfo
{
	public string id;

	public string model;

	public string image;

	public string name;

	public ZS_Money money;

	public int hp;

	public int level;

	public bool isLock;

	public bool isCanBuy;

	public string desc;

	public string unlockCondition;

	public object data;

	public string MeleeWeapon;

	public string specialId;

	private Func<ZS_AvatarPhotoInfo, int> buyAvatarCallBack;

	private Func<ZS_AvatarPhotoInfo, int> useAvatarCallBack;

	public Func<ZS_AvatarPhotoInfo, int> BuyAvatarCallBack
	{
		get
		{
			return buyAvatarCallBack;
		}
		set
		{
			buyAvatarCallBack = value;
		}
	}

	public Func<ZS_AvatarPhotoInfo, int> UseAvatarCallBack
	{
		get
		{
			return useAvatarCallBack;
		}
		set
		{
			useAvatarCallBack = value;
		}
	}

	public override string ToString()
	{
		return " id " + id + " model " + model + " image " + image + " name " + name + " hp " + hp + " level " + level + " isLock " + isLock + "isCanBuy" + isCanBuy + " descs " + desc + " unlockCondition " + unlockCondition;
	}
}
