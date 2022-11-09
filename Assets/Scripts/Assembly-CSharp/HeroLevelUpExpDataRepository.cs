using System;
using System.Collections.Generic;

public class HeroLevelUpExpDataRepository
{
	private Dictionary<int, HeroLevelUpExpData> datas = new Dictionary<int, HeroLevelUpExpData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		HeroLevelUpExpData[] array = dataReadWriteModel.Deserialize<HeroLevelUpExpData[]>();
		datas.Clear();
		if (array != null)
		{
			HeroLevelUpExpData[] array2 = array;
			foreach (HeroLevelUpExpData heroLevelUpExpData in array2)
			{
				datas.Add(heroLevelUpExpData.level, heroLevelUpExpData);
			}
		}
	}

	public HeroLevelUpExpData Find(int id)
	{
		if (datas.ContainsKey(id))
		{
			return datas[id];
		}
		return null;
	}

	public List<HeroLevelUpExpData> FindAll(Predicate<HeroLevelUpExpData> match)
	{
		throw new NotImplementedException();
	}
}
