using System;
using System.Collections.Generic;

public class EnemyBaseDataRepository : IEnemyBaseDataRepository, IRepository<int, EnemyBaseData>
{
	private Dictionary<int, EnemyBaseData> datas = new Dictionary<int, EnemyBaseData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		EnemyBaseData[] array = dataReadWriteModel.Deserialize<EnemyBaseData[]>();
		datas.Clear();
		if (array != null)
		{
			EnemyBaseData[] array2 = array;
			foreach (EnemyBaseData enemyBaseData in array2)
			{
				datas.Add(enemyBaseData.id, enemyBaseData);
			}
		}
	}

	public EnemyBaseData Find(int id)
	{
		return datas[id];
	}

	public List<EnemyBaseData> FindAll(Predicate<EnemyBaseData> match)
	{
		throw new NotImplementedException();
	}

	public void Add(int id, EnemyBaseData entity)
	{
		throw new NotImplementedException();
	}

	public void Remove(int id)
	{
		throw new NotImplementedException();
	}

	public void Remove(EnemyBaseData entity)
	{
		throw new NotImplementedException();
	}

	public void Update(int id, EnemyBaseData entity)
	{
		throw new NotImplementedException();
	}

	public void Save()
	{
		throw new NotImplementedException();
	}
}
