using System;
using System.Collections.Generic;

public class IAPDataRepository
{
	private Dictionary<string, IAPData> datas = new Dictionary<string, IAPData>();

	private List<IAPData> temp = new List<IAPData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		IAPData[] array = dataReadWriteModel.Deserialize<IAPData[]>();
		datas.Clear();
		if (array != null)
		{
			IAPData[] array2 = array;
			foreach (IAPData iAPData in array2)
			{
				datas.Add(iAPData.id, iAPData);
			}
		}
	}

	public IAPData Find(string id)
	{
		return datas[id];
	}

	public List<IAPData> FindAll(Predicate<IAPData> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}
}
