using System;
using System.Collections.Generic;

public interface IRepository<TId, TEntity> where TEntity : class
{
	TEntity Find(TId id);

	List<TEntity> FindAll(Predicate<TEntity> match);

	void Add(TId id, TEntity entity);

	void Remove(TId id);

	void Remove(TEntity entity);

	void Update(TId id, TEntity entity);

	void Save();
}
