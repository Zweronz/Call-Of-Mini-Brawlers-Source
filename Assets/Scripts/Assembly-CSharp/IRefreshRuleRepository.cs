public interface IRefreshRuleRepository : IRepository<string, RefreshRuleData>
{
	void Initialize(IDataReadWriteModel dataReadWriteModel);
}
