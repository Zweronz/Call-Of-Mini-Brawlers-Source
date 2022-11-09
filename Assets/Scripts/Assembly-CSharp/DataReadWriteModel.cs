using System;
using System.IO;
using UnityEngine;

public class DataReadWriteModel : IDataReadWriteModel
{
	private IDataReadWrite dataReadWrite;

	private string readPath;

	private string writePath;

	private bool isWindowsModel;

	public DataReadWriteModel(IDataReadWrite dataReadWrite, string path, bool isWindowsModel)
		: this(dataReadWrite, path, path, isWindowsModel)
	{
	}

	public DataReadWriteModel(IDataReadWrite dataReadWrite, string readPath, string writePath, bool isWindowsModel)
	{
		this.dataReadWrite = dataReadWrite;
		this.readPath = readPath;
		this.writePath = writePath;
		this.isWindowsModel = isWindowsModel;
	}

	public object Deserialize(Type type)
	{
		Stream stream = ReadStream();
		object result = dataReadWrite.Deserialize(stream, type);
		stream.Close();
		stream.Dispose();
		return result;
	}

	public T Deserialize<T>()
	{
		return (T)Deserialize(typeof(T));
	}

	public void Serialize(object data)
	{
		Stream stream = WriteStream();
		dataReadWrite.Serialize(stream, data);
		stream.Close();
		stream.Dispose();
	}

	private Stream ReadStream()
	{
		Stream stream = null;
		if (isWindowsModel)
		{
			return new FileStream(readPath, FileMode.Open, FileAccess.Read);
		}
		TextAsset textAsset = (TextAsset)Resources.Load(readPath, typeof(TextAsset));
		return new MemoryStream(textAsset.bytes);
	}

	private Stream WriteStream()
	{
		Stream stream = null;
		return new FileStream(writePath, FileMode.Create, FileAccess.Write);
	}
}
