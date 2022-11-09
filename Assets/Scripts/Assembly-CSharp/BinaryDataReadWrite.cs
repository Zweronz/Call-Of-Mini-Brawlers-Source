using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryDataReadWrite : IDataReadWrite
{
	private static BinaryDataReadWrite instance;

	private BinaryFormatter bf;

	public static BinaryDataReadWrite Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new BinaryDataReadWrite();
			}
			return instance;
		}
	}

	public object Deserialize(Stream serializationStream, Type type)
	{
		if (bf == null)
		{
			bf = new BinaryFormatter();
		}
		return bf.Deserialize(serializationStream);
	}

	public void Serialize(Stream serializationStream, object graph)
	{
		if (bf == null)
		{
			bf = new BinaryFormatter();
		}
		bf.Serialize(serializationStream, graph);
	}
}
