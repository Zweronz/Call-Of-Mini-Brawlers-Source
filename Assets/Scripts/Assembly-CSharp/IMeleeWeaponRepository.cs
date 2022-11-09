using System.Collections.Generic;

public interface IMeleeWeaponRepository : IRepository<string, MeleeWeaponData>
{
	void Initialize(IDataReadWriteModel dataReadWriteModel);

	List<MeleeWeaponData> FindByTypeName(string typeName);
}
