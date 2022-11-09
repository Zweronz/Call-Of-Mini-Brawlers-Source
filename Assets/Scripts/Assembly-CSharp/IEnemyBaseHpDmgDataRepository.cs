public interface IEnemyBaseHpDmgDataRepository : IRepository<int, EnemyBaseHpDmgData>
{
	void Initialize(IDataReadWriteModel dataReadWriteModel);
}
