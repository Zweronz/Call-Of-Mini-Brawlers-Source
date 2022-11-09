using System;
using System.Collections.Generic;

[Serializable]
public abstract class Achievement<TAchievementData> : IAchievement where TAchievementData : AchievementData
{
	public TAchievementData data;

	public string ID
	{
		get
		{
			return data.id;
		}
	}

	public string NextID
	{
		get
		{
			return data.nextId;
		}
	}

	public int Type
	{
		get
		{
			return data.type;
		}
	}

	public float Gold
	{
		get
		{
			return data.gold;
		}
	}

	public float Crystal
	{
		get
		{
			return data.crystal;
		}
	}

	public string ItemId
	{
		get
		{
			return data.itemId;
		}
	}

	public int ItemCount
	{
		get
		{
			return data.itemCount;
		}
	}

	public string Desc
	{
		get
		{
			return data.descId;
		}
	}

	public AchievementState State { get; protected set; }

	public float Progress { get; protected set; }

	public void Initialize()
	{
		List<IAchievement> list = DataCenter.Instance.Achievements.FindByType(Type);
		if (Player.Instance.ContainscompletedAchievementType(Type))
		{
			IAchievement achievement = DataCenter.Instance.Achievements.Find(Player.Instance.GetCompletedAchievementIdByType(Type));
			if (list.IndexOf(achievement) >= list.IndexOf(this))
			{
				State = AchievementState.Completed;
			}
			else if (achievement.NextID != null && ID.Equals(achievement.NextID))
			{
				State = AchievementState.Processing;
			}
			else
			{
				State = AchievementState.NotStart;
			}
		}
		else if (list[0].ID.Equals(ID))
		{
			State = AchievementState.Processing;
		}
		else
		{
			State = AchievementState.NotStart;
		}
	}

	public void Clear()
	{
		Progress = 0f;
	}

	public void Process()
	{
		if (State == AchievementState.Processing && Progress < 1f)
		{
			DoProcess();
		}
	}

	protected abstract void DoProcess();

	public void GetReward()
	{
		if (State == AchievementState.Processing && Progress >= 1f)
		{
			if (data.gold > 0f)
			{
				Player.Instance.AddGold(data.gold);
			}
			if (data.crystal > 0f)
			{
				Player.Instance.AddCrystal(data.crystal);
			}
			if (!string.IsNullOrEmpty(data.itemId))
			{
				Player.Instance.AddItem(data.itemId, data.itemCount);
			}
		}
	}
}
