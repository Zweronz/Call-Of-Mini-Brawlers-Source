using System.Collections.Generic;

public interface IGunRepository : IRepository<string, GunData>
{
	void Initialize(IDataReadWriteModel dataReadWriteModel);

	List<GunData> Find(params string[] ids);

	List<GunData> FindByTypeName(string typeName);
}
