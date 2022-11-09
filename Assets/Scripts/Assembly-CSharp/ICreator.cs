using System.Collections.Generic;

public interface ICreator<T, TData>
{
	T Create(TData data);

	List<T> Create(params TData[] datas);
}
