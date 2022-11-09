using System;

public class ZS_ItemInfo
{
	private string id;

	private string model;

	private string name;

	private string image;

	private ZS_Money money;

	private string desc;

	private bool isUsing;

	private bool isOwn;

	public bool canBuy;

	private int count;

	public object data;

	private Func<ZS_ItemInfo, int> buyCallBack;

	private Func<int, ZS_ItemInfo, int> equipCallBack;

	private Func<int, ZS_ItemInfo, int> unwieldCallBack;

	public string Id
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	public string Model
	{
		get
		{
			return model;
		}
		set
		{
			model = value;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public string Image
	{
		get
		{
			return image;
		}
		set
		{
			image = value;
		}
	}

	public string Desc
	{
		get
		{
			return desc;
		}
		set
		{
			desc = value;
		}
	}

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

	public Func<ZS_ItemInfo, int> BuyCallBack
	{
		get
		{
			return buyCallBack;
		}
		set
		{
			buyCallBack = value;
		}
	}

	public Func<int, ZS_ItemInfo, int> UnwieldCallBack
	{
		get
		{
			return unwieldCallBack;
		}
		set
		{
			unwieldCallBack = value;
		}
	}

	public Func<int, ZS_ItemInfo, int> EquipCallBack
	{
		get
		{
			return equipCallBack;
		}
		set
		{
			equipCallBack = value;
		}
	}

	public bool IsUsing
	{
		get
		{
			return isUsing;
		}
		set
		{
			isUsing = value;
		}
	}

	public bool IsOwn
	{
		get
		{
			return isOwn;
		}
		set
		{
			isOwn = value;
		}
	}

	public int Count
	{
		get
		{
			return count;
		}
		set
		{
			count = value;
		}
	}
}
