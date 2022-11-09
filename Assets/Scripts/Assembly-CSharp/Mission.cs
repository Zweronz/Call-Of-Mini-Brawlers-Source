using System;
using System.Collections.Generic;

[Serializable]
public abstract class Mission<TMissionData> : IMission where TMissionData : MissionData
{
	public TMissionData data;

	public int Type
	{
		get
		{
			return data.type;
		}
	}

	public string ID
	{
		get
		{
			return data.id;
		}
	}

	public int Priority
	{
		get
		{
			return data.priority;
		}
	}

	public float RefreshZombieInterval
	{
		get
		{
			return data.refreshZombieInterval;
		}
	}

	public float SceneLength
	{
		get
		{
			return data.sceneLength;
		}
	}

	public List<string> RefreshRules
	{
		get
		{
			return data.refreshRules;
		}
	}

	public bool NeedSpecialRefresh
	{
		get
		{
			return data.specialRefreshZombieInterval > 0f;
		}
	}

	public float SpecialRefreshZombieInterval
	{
		get
		{
			return data.specialRefreshZombieInterval;
		}
	}

	public List<string> SpecialRefreshRules
	{
		get
		{
			return data.specialRefrashRules;
		}
	}

	public MissionState State { get; protected set; }

	public string DescId
	{
		get
		{
			return data.descId;
		}
	}

	public string Icon
	{
		get
		{
			return data.icon;
		}
	}

	public virtual bool IsAvailable(int level)
	{
		if (level < data.minLevel)
		{
			return false;
		}
		if (level == data.minLevel)
		{
			return true;
		}
		if (data.maxLevel != 0 && level > data.maxLevel)
		{
			return false;
		}
		return 0 == (level - data.minLevel) % (data.interval + 1);
	}

	public abstract void Start();

	public abstract void Reset(bool resetInfo = true);

	public abstract void Initialize(int level);

	public abstract void InitializeUI();

	public abstract float GetProcess();

	public abstract List<object> GetDescData(int level);
}
