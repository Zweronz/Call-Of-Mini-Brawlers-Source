using System;
using System.Collections.Generic;

public class MissionRepository
{
	private Dictionary<string, IMission> datas = new Dictionary<string, IMission>();

	private List<IMission> temp = new List<IMission>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		IMission[] array = dataReadWriteModel.Deserialize<IMission[]>();
		datas.Clear();
		if (array != null)
		{
			IMission[] array2 = array;
			foreach (IMission mission in array2)
			{
				datas.Add(mission.ID, mission);
			}
		}
	}

	public IMission Find(string id)
	{
		return datas[id];
	}

	public List<IMission> FindAll(Predicate<IMission> match)
	{
		temp.Clear();
		temp.AddRange(datas.Values);
		return temp.FindAll(match);
	}

	public void Add(IMission mission)
	{
		if (!datas.ContainsKey(mission.ID))
		{
			datas.Add(mission.ID, mission);
		}
	}
}
