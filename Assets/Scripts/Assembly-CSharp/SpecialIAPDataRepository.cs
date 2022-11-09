using System;
using System.Collections.Generic;

public class SpecialIAPDataRepository
{
	private Dictionary<string, SpecailIAPData> datas = new Dictionary<string, SpecailIAPData>();

	private List<SpecailIAPData> temp = new List<SpecailIAPData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		SpecailIAPData[] array = dataReadWriteModel.Deserialize<SpecailIAPData[]>();
		datas.Clear();
		if (array != null)
		{
			SpecailIAPData[] array2 = array;
			foreach (SpecailIAPData specailIAPData in array2)
			{
				datas.Add(specailIAPData.id, specailIAPData);
			}
		}
	}

	public SpecailIAPData Find(string id)
	{
		return datas[id];
	}

	public List<SpecailIAPData> FindAll(Predicate<SpecailIAPData> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}
}
