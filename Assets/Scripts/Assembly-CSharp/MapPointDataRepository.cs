using System;
using System.Collections.Generic;

public class MapPointDataRepository
{
	private Dictionary<int, MapPointData> datas = new Dictionary<int, MapPointData>();

	private List<MapPointData> temp = new List<MapPointData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		MapPointData[] array = dataReadWriteModel.Deserialize<MapPointData[]>();
		datas.Clear();
		if (array != null)
		{
			MapPointData[] array2 = array;
			foreach (MapPointData mapPointData in array2)
			{
				datas.Add(mapPointData.id, mapPointData);
			}
		}
	}

	public MapPointData Find(int id)
	{
		return datas[id];
	}

	public bool Contain(int id)
	{
		return datas.ContainsKey(id);
	}

	public List<MapPointData> FindAll(Predicate<MapPointData> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}
}
