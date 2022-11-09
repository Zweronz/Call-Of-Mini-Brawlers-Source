using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class GunRepository : IGunRepository, IRepository<string, GunData>
{
	[CompilerGenerated]
	private sealed class _003CInitialize_003Ec__AnonStorey1E
	{
		internal string typeName;

		internal bool _003C_003Em__7(GunData data)
		{
			return data.typeName == typeName;
		}

		internal bool _003C_003Em__8(GunData data)
		{
			return data.typeName == typeName;
		}
	}

	private Dictionary<string, GunData> datas = new Dictionary<string, GunData>();

	private Dictionary<string, List<GunData>> datas2 = new Dictionary<string, List<GunData>>();

	private List<GunData> temp = new List<GunData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		GunData[] array = dataReadWriteModel.Deserialize<GunData[]>();
		datas.Clear();
		if (array != null)
		{
			GunData[] array2 = array;
			foreach (GunData gunData in array2)
			{
				datas.Add(gunData.id, gunData);
			}
		}
		List<GunData> list = new List<GunData>();
		list.AddRange(array);
		List<GunData> list2 = new List<GunData>();
		List<GunData> list3 = new List<GunData>();
		string empty = string.Empty;
		while (list.Count > 0)
		{
			_003CInitialize_003Ec__AnonStorey1E _003CInitialize_003Ec__AnonStorey1E = new _003CInitialize_003Ec__AnonStorey1E();
			_003CInitialize_003Ec__AnonStorey1E.typeName = list[0].typeName;
			list2.Clear();
			list2.AddRange(list.FindAll(_003CInitialize_003Ec__AnonStorey1E._003C_003Em__7));
			list.RemoveAll(_003CInitialize_003Ec__AnonStorey1E._003C_003Em__8);
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
				foreach (GunData item in list2)
				{
					if (item.nextId == null)
					{
						item.nextId = string.Empty;
					}
					if (item.nextId.Equals(empty))
					{
						list3.Add(item);
						list2.Remove(item);
						empty = item.id;
						break;
					}
				}
			}
			if (list3.Count > 0)
			{
				List<GunData> list4 = new List<GunData>();
				for (int num2 = list3.Count - 1; num2 > -1; num2--)
				{
					list4.Add(list3[num2]);
				}
				datas2.Add(list4[0].typeName, list4);
			}
		}
	}

	public GunData Find(string id)
	{
		return datas[id];
	}

	public List<GunData> Find(params string[] ids)
	{
		List<GunData> list = new List<GunData>();
		foreach (string id in ids)
		{
			list.Add(Find(id));
		}
		return list;
	}

	public List<GunData> FindByTypeName(string typeName)
	{
		return datas2[typeName];
	}

	public List<GunData> FindAll(Predicate<GunData> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}

	public void Add(string id, GunData entity)
	{
		throw new NotImplementedException();
	}

	public void Remove(string id)
	{
		throw new NotImplementedException();
	}

	public void Remove(GunData entity)
	{
		throw new NotImplementedException();
	}

	public void Update(string id, GunData entity)
	{
		throw new NotImplementedException();
	}

	public void Save()
	{
		throw new NotImplementedException();
	}
}
