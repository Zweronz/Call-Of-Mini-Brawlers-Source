using System;

public interface IDataReadWriteModel
{
	object Deserialize(Type type);

	T Deserialize<T>();

	void Serialize(object data);
}
