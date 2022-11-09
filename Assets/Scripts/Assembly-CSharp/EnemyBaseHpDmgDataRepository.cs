using System;
using System.Collections.Generic;

public class EnemyBaseHpDmgDataRepository : IEnemyBaseHpDmgDataRepository, IRepository<int, EnemyBaseHpDmgData>
{
	private Dictionary<int, EnemyBaseHpDmgData> datas = new Dictionary<int, EnemyBaseHpDmgData>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		EnemyBaseHpDmgData[] array = dataReadWriteModel.Deserialize<EnemyBaseHpDmgData[]>();
		datas.Clear();
		if (array != null)
		{
			EnemyBaseHpDmgData[] array2 = array;
			foreach (EnemyBaseHpDmgData enemyBaseHpDmgData in array2)
			{
				datas.Add(enemyBaseHpDmgData.level, enemyBaseHpDmgData);
			}
		}
	}

	public EnemyBaseHpDmgData Find(int id)
	{
		if (!datas.ContainsKey(id))
		{
			datas.Add(id, Formula(id));
		}
		return datas[id];
	}

	public List<EnemyBaseHpDmgData> FindAll(Predicate<EnemyBaseHpDmgData> match)
	{
		throw new NotImplementedException();
	}

	public void Add(int id, EnemyBaseHpDmgData entity)
	{
		throw new NotImplementedException();
	}

	public void Remove(int id)
	{
		throw new NotImplementedException();
	}

	public void Remove(EnemyBaseHpDmgData entity)
	{
		throw new NotImplementedException();
	}

	public void Update(int id, EnemyBaseHpDmgData entity)
	{
		throw new NotImplementedException();
	}

	public void Save()
	{
		throw new NotImplementedException();
	}

	public EnemyBaseHpDmgData Formula(int level)
	{
		EnemyBaseHpDmgData enemyBaseHpDmgData = new EnemyBaseHpDmgData();
		enemyBaseHpDmgData.level = level;
		enemyBaseHpDmgData.hp = (float)Math.Round(4f * (float)(level - 1) + 14f, MidpointRounding.AwayFromZero);
		enemyBaseHpDmgData.damage = (float)Math.Round(0.34 * (double)(level - 1) + 12.0, MidpointRounding.AwayFromZero);
		enemyBaseHpDmgData.gold = (float)Math.Round(5f + (float)(level - 1) * 1.3f, MidpointRounding.AwayFromZero);
		enemyBaseHpDmgData.exp = 3f + (float)(level - 1) * 2.5f;
		enemyBaseHpDmgData.extra = (float)Math.Round(0.15 * (double)(level - 1) + 2.0, MidpointRounding.AwayFromZero);
		enemyBaseHpDmgData.bonus = 500f + (float)((level - 1) * 100);
		return enemyBaseHpDmgData;
	}
}
