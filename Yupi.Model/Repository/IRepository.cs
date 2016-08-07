using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Yupi.Model.Repository
{
	public interface IRepository<T>
	{
		void Save(T entity);
		void Delete(T entity);

		IList<T>  ToList();
		IQueryable<T> All ();
		T FindBy(int id);
		T FindBy(Expression<Func<T, bool>> expression);
		IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
	}
}

