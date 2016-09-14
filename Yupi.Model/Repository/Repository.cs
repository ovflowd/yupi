namespace Yupi.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using NHibernate;
    using NHibernate.Linq;

    public class Repository<T> : IRepository<T>
    {
        #region Fields

        private ISession session;

        #endregion Fields

        #region Constructors

        public Repository(ISession session)
        {
            this.session = session;
        }

        #endregion Constructors

        #region Methods

        public IQueryable<T> All()
        {
            return session.Query<T>();
        }

        public void Delete(T entity)
        {
            session.Delete(entity);
        }

        public void Delete(int Id)
        {
            session.Delete(session.Load<T>(Id));
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).Any();
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return All().Where(expression).AsQueryable();
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).SingleOrDefault();
        }

        public T FindBy(int id)
        {
            return session.Get<T>(id);
        }

        public void Save(T entity)
        {
            session.SaveOrUpdate(entity);
            session.Flush();
        }

        public IList<T> ToList()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}