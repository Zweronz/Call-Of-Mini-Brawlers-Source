using System;
using System.Collections.Generic;

public class HeroDataRepository
{
	private Dictionary<int, HeroData> datas = new Dictionary<int, HeroData>();

	private List<HeroData> temp = new List<HeroData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		HeroData[] array = dataReadWriteModel.Deserialize<HeroData[]>();
		datas.Clear();
		if (array != null)
		{
			HeroData[] array2 = array;
			foreach (HeroData heroData in array2)
			{
				datas.Add(heroData.id, heroData);
			}
		}
	}

	public HeroData Find(int id)
	{
		return datas[id];
	}

	public List<HeroData> FindAll(Predicate<HeroData> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}
}
