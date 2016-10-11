// ---------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Linq;
    using NHibernate.Transform;

    public class Repository<T> : IRepository<T>
        where T : class
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

        public T Find(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).FirstOrDefault();
        }

        public T Find(object id)
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