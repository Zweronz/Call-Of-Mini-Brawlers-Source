using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class MeleeWeaponRepository : IMeleeWeaponRepository, IRepository<string, MeleeWeaponData>
{
	[CompilerGenerated]
	private sealed class _003CInitialize_003Ec__AnonStorey1F
	{
		internal string typeName;

		internal bool _003C_003Em__9(MeleeWeaponData data)
		{
			return data.typeName == typeName;
		}

		internal bool _003C_003Em__A(MeleeWeaponData data)
		{
			return data.typeName == typeName;
		}
	}

	private Dictionary<string, MeleeWeaponData> datas = new Dictionary<string, MeleeWeaponData>();

	private Dictionary<string, List<MeleeWeaponData>> datas2 = new Dictionary<string, List<MeleeWeaponData>>();

	private List<MeleeWeaponData> temp = new List<MeleeWeaponData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		MeleeWeaponData[] array = dataReadWriteModel.Deserialize<MeleeWeaponData[]>();
		datas.Clear();
		if (array != null)
		{
			MeleeWeaponData[] array2 = array;
			foreach (MeleeWeaponData meleeWeaponData in array2)
			{
				datas.Add(meleeWeaponData.id, meleeWeaponData);
			}
		}
		List<MeleeWeaponData> list = new List<MeleeWeaponData>();
		list.AddRange(array);
		List<MeleeWeaponData> list2 = new List<MeleeWeaponData>();
		List<MeleeWeaponData> list3 = new List<MeleeWeaponData>();
		string empty = string.Empty;
		while (list.Count > 0)
		{
			_003CInitialize_003Ec__AnonStorey1F _003CInitialize_003Ec__AnonStorey1F = new _003CInitialize_003Ec__AnonStorey1F();
			_003CInitialize_003Ec__AnonStorey1F.typeName = list[0].typeName;
			list2.Clear();
			list2.AddRange(list.FindAll(_003CInitialize_003Ec__AnonStorey1F._003C_003Em__9));
			list.RemoveAll(_003CInitialize_003Ec__AnonStorey1F._003C_003Em__A);
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
				foreach (MeleeWeaponData item in list2)
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
				List<MeleeWeaponData> list4 = new List<MeleeWeaponData>();
				for (int num2 = list3.Count - 1; num2 > -1; num2--)
				{
					list4.Add(list3[num2]);
				}
				datas2.Add(list4[0].typeName, list4);
			}
		}
	}

	public MeleeWeaponData Find(string id)
	{
		return datas[id];
	}

	public List<MeleeWeaponData> FindAll(Predicate<MeleeWeaponData> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}

	public List<MeleeWeaponData> FindByTypeName(string typeName)
	{
		return datas2[typeName];
	}

	public void Add(string id, MeleeWeaponData entity)
	{
		throw new NotImplementedException();
	}

	public void Remove(string id)
	{
		throw new NotImplementedException();
	}

	public void Remove(MeleeWeaponData entity)
	{
		throw new NotImplementedException();
	}

	public void Update(string id, MeleeWeaponData entity)
	{
		throw new NotImplementedException();
	}

	public void Save()
	{
		throw new NotImplementedException();
	}
}
