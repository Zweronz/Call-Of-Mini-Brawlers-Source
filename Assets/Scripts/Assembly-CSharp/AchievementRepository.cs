using System;
using System.Collections.Generic;

public class AchievementRepository
{
	private Dictionary<string, IAchievement> datas = new Dictionary<string, IAchievement>();

	private Dictionary<int, List<IAchievement>> datas2 = new Dictionary<int, List<IAchievement>>();

	private List<IAchievement> temp = new List<IAchievement>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		IAchievement[] array = dataReadWriteModel.Deserialize<IAchievement[]>();
		datas.Clear();
		if (array != null)
		{
			IAchievement[] array2 = array;
			foreach (IAchievement achievement in array2)
			{
				datas.Add(achievement.ID, achievement);
			}
		}
		List<IAchievement> list = new List<IAchievement>();
		list.AddRange(array);
		List<IAchievement> list2 = new List<IAchievement>();
		List<IAchievement> list3 = new List<IAchievement>();
		string empty = string.Empty;
		while (list.Count > 0)
		{
			int type = list[0].Type;
			list2.Clear();
			list2.AddRange(list.FindAll((IAchievement data) => data.Type == type));
			list.RemoveAll((IAchievement data) => data.Type == type);
			empty = string.Empty;
			list3.Clear();
			int num = list2.Count;
			while (list2.Count > 0)
			{
				num--;
				if (num < 0)
				{
					break;
				}
				foreach (IAchievement item in list2)
				{
					if ((item.NextID == null && string.IsNullOrEmpty(empty)) || (item.NextID != null && item.NextID.Equals(empty)))
					{
						list3.Add(item);
						list2.Remove(item);
						empty = item.ID;
						break;
					}
				}
			}
			if (list3.Count > 0)
			{
				List<IAchievement> list4 = new List<IAchievement>();
				for (int num2 = list3.Count - 1; num2 > -1; num2--)
				{
					list4.Add(list3[num2]);
				}
				datas2.Add(list4[0].Type, list4);
			}
		}
	}

	public void InitializeAllAchievements()
	{
		foreach (IAchievement value in datas.Values)
		{
			value.Initialize();
		}
	}

	public IAchievement Find(string id)
	{
		return datas[id];
	}

	public List<IAchievement> FindAll(Predicate<IAchievement> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}

	public List<IAchievement> FindByType(int type)
	{
		return datas2[type];
	}

	public List<IAchievement> GetAll()
	{
		List<IAchievement> list = new List<IAchievement>();
		list.AddRange(datas.Values);
		return list;
	}
}
