public interface IEnemyBaseDataRepository : IRepository<int, EnemyBaseData>
{
	void Initialize(IDataReadWriteModel dataReadWriteModel);
}
