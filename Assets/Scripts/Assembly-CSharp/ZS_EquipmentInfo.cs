using System;

public class ZS_EquipmentInfo
{
	private string id;

	private string model;

	private string anima;

	private string name;

	private string image;

	private string group;

	private float criticalHit;

	private float criticalChance;

	private float knockBack;

	private int attack;

	private int ammo;

	private float nextCriticalHit;

	private float nextCriticalChance;

	private float nextKnockBack;

	private int nextAttack;

	private int nextAmmo;

	private ZS_Money buyMoney;

	private ZS_Money updateMoney;

	private bool canUpdate;

	private bool isCanBuy;

	private bool isCanEquip;

	private bool isOwn;

	private bool isEquiped;

	private string buyCondition;

	private string equipCondition;

	private int type;

	private object data;

	private Func<ZS_EquipmentInfo, int> buyCallBack;

	private Func<int, ZS_EquipmentInfo, int> equipCallBack;

	private Func<ZS_EquipmentInfo, int> upgraedCallBack;

	private Func<int, ZS_EquipmentInfo, int> unwieldCallBack;

	public float minCriticalHit;

	public float minCriticalChance;

	public float minKnockBack;

	public int minAttack;

	public int minAmmo;

	public int level;

	public float maxCriticalHit;

	public float maxCriticalChance;

	public float maxKnockBack;

	public int maxAttack;

	public int maxAmmo;

	public bool isMaxLevel;

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

	public float NextCriticalHit
	{
		get
		{
			return nextCriticalHit;
		}
		set
		{
			nextCriticalHit = value;
		}
	}

	public float NextCriticalChance
	{
		get
		{
			return nextCriticalChance;
		}
		set
		{
			nextCriticalChance = value;
		}
	}

	public float NextKnockBack
	{
		get
		{
			return nextKnockBack;
		}
		set
		{
			nextKnockBack = value;
		}
	}

	public int NextAttack
	{
		get
		{
			return nextAttack;
		}
		set
		{
			nextAttack = value;
		}
	}

	public int NextAmmo
	{
		get
		{
			return nextAmmo;
		}
		set
		{
			nextAmmo = value;
		}
	}

	public int Attack
	{
		get
		{
			return attack;
		}
		set
		{
			attack = value;
		}
	}

	public int Ammo
	{
		get
		{
			return ammo;
		}
		set
		{
			ammo = value;
		}
	}

	public float CriticalHit
	{
		get
		{
			return criticalHit;
		}
		set
		{
			criticalHit = value;
		}
	}

	public float CriticalChance
	{
		get
		{
			return criticalChance;
		}
		set
		{
			criticalChance = value;
		}
	}

	public float KnockBack
	{
		get
		{
			return knockBack;
		}
		set
		{
			knockBack = value;
		}
	}

	public ZS_Money BuyMoney
	{
		get
		{
			return buyMoney;
		}
		set
		{
			buyMoney = value;
		}
	}

	public ZS_Money UpdateMoney
	{
		get
		{
			return updateMoney;
		}
		set
		{
			updateMoney = value;
		}
	}

	public bool CanUpdate
	{
		get
		{
			return canUpdate;
		}
		set
		{
			canUpdate = value;
		}
	}

	public bool IsCanBuy
	{
		get
		{
			return isCanBuy;
		}
		set
		{
			isCanBuy = value;
		}
	}

	public bool IsCanEquip
	{
		get
		{
			return isCanEquip;
		}
		set
		{
			isCanEquip = value;
		}
	}

	public int Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
		}
	}

	public string BuyCondition
	{
		get
		{
			return buyCondition;
		}
		set
		{
			buyCondition = value;
		}
	}

	public string EquipCondition
	{
		get
		{
			return equipCondition;
		}
		set
		{
			equipCondition = value;
		}
	}

	public Func<ZS_EquipmentInfo, int> BuyCallBack
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

	public Func<int, ZS_EquipmentInfo, int> EquipCallBack
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

	public Func<ZS_EquipmentInfo, int> UpGradeCallBack
	{
		get
		{
			return upgraedCallBack;
		}
		set
		{
			upgraedCallBack = value;
		}
	}

	public Func<int, ZS_EquipmentInfo, int> UnwieldCallBack
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

	public string Anima
	{
		get
		{
			return anima;
		}
		set
		{
			anima = value;
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

	public bool IsEquiped
	{
		get
		{
			return isEquiped;
		}
		set
		{
			isEquiped = value;
		}
	}

	public string Group
	{
		get
		{
			return group;
		}
		set
		{
			group = value;
		}
	}

	public override string ToString()
	{
		return criticalHit + " -hit - " + criticalChance + " -chance - " + knockBack + "  -back- " + attack + " -att - " + ammo + "  -amm- " + nextCriticalHit + " - " + nextCriticalChance + " - " + nextKnockBack + " - " + nextAttack + " - " + nextAmmo;
	}
}
