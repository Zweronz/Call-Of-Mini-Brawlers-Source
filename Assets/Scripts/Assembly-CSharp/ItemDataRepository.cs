using System;
using System.Collections.Generic;

public class ItemDataRepository
{
	private Dictionary<string, IItem> datas = new Dictionary<string, IItem>();

	private List<IItem> temp = new List<IItem>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		IItem[] array = dataReadWriteModel.Deserialize<IItem[]>();
		datas.Clear();
		if (array != null)
		{
			IItem[] array2 = array;
			foreach (IItem item in array2)
			{
				datas.Add(item.BaseData.id, item);
			}
		}
	}

	public IItem Find(string id)
	{
		return datas[id];
	}

	public List<IItem> FindAll(Predicate<IItem> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}
}
