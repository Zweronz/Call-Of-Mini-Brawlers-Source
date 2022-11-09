using System;
using System.IO;
using System.Xml.Serialization;

public class XmlDataReadWrite : IDataReadWrite
{
	private static XmlDataReadWrite instance;

	private XmlSerializer bf;

	public static XmlDataReadWrite Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new XmlDataReadWrite();
			}
			return instance;
		}
	}

	public object Deserialize(Stream serializationStream, Type type)
	{
		if (bf == null)
		{
			bf = new XmlSerializer(type);
		}
		return bf.Deserialize(serializationStream);
	}

	public void Serialize(Stream serializationStream, object graph)
	{
		if (bf == null)
		{
			bf = new XmlSerializer(graph.GetType());
		}
		bf.Serialize(serializationStream, graph);
	}
}
