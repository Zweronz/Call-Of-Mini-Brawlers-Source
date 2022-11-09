using System;
using System.Collections.Generic;

public class Crystal2GoldDataRepository
{
	private Dictionary<string, Crystal2Gold> datas = new Dictionary<string, Crystal2Gold>();

	private List<Crystal2Gold> temp = new List<Crystal2Gold>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		Crystal2Gold[] array = dataReadWriteModel.Deserialize<Crystal2Gold[]>();
		datas.Clear();
		if (array != null)
		{
			Crystal2Gold[] array2 = array;
			foreach (Crystal2Gold crystal2Gold in array2)
			{
				datas.Add(crystal2Gold.id, crystal2Gold);
			}
		}
	}

	public Crystal2Gold Find(string id)
	{
		return datas[id];
	}

	public List<Crystal2Gold> FindAll(Predicate<Crystal2Gold> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}
}
