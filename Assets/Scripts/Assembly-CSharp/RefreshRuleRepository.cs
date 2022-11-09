using System;
using System.Collections.Generic;

public class RefreshRuleRepository : IRefreshRuleRepository, IRepository<string, RefreshRuleData>
{
	private Dictionary<string, RefreshRuleData> datas = new Dictionary<string, RefreshRuleData>();

	private List<RefreshRuleData> randomDatas = new List<RefreshRuleData>();

	private List<RefreshRuleData> temp = new List<RefreshRuleData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		RefreshRuleData[] array = dataReadWriteModel.Deserialize<RefreshRuleData[]>();
		datas.Clear();
		if (array != null)
		{
			RefreshRuleData[] array2 = array;
			foreach (RefreshRuleData refreshRuleData in array2)
			{
				datas.Add(refreshRuleData.id, refreshRuleData);
			}
		}
		randomDatas.Clear();
		randomDatas.AddRange(datas.Values);
	}

	public RefreshRuleData Find(string id)
	{
		return datas[id];
	}

	public List<RefreshRuleData> FindAll(Predicate<RefreshRuleData> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}

	public void Add(string id, RefreshRuleData entity)
	{
		throw new NotImplementedException();
	}

	public void Remove(string id)
	{
		throw new NotImplementedException();
	}

	public void Remove(RefreshRuleData entity)
	{
		throw new NotImplementedException();
	}

	public void Update(string id, RefreshRuleData entity)
	{
		throw new NotImplementedException();
	}

	public void Save()
	{
		throw new NotImplementedException();
	}
}
