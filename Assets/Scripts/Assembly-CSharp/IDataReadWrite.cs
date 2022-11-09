using System;
using System.IO;

public interface IDataReadWrite
{
	object Deserialize(Stream serializationStream, Type type);

	void Serialize(Stream serializationStream, object graph);
}
