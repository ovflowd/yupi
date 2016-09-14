namespace Yupi.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T>
    {
        #region Methods

        IQueryable<T> All();

        void Delete(T entity);

        bool Exists(Expression<Func<T, bool>> expression);

        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);

        T FindBy(int id);

        T FindBy(Expression<Func<T, bool>> expression);

        void Save(T entity);

        IList<T> ToList();

        #endregion Methods
    }
}