namespace Yupi.Model
{
    using System;
    using System.Data;

    using NHibernate.SqlTypes;
    using NHibernate.UserTypes;

    public abstract class UserType : IUserType
    {
        #region Properties

        public bool IsMutable
        {
            get { return false; }
        }

        /// <summary>
        /// The type returned by <c>NullSafeGet()</c>
        /// </summary>
        public abstract Type ReturnedType
        {
            get;
        }

        /// <summary>
        /// The SQL types for the columns mapped by this type.
        /// </summary>
        public abstract SqlType[] SqlTypes
        {
            get;
        }

        #endregion Properties

        #region Methods

        public virtual object Assemble(object cached, object owner)
        {
            return cached;
        }

        public virtual object DeepCopy(object value)
        {
            return value;
        }

        public virtual object Disassemble(object value)
        {
            return value;
        }

        public int GetHashCode(object x)
        {
            return x == null ? 0 : x.GetHashCode();
        }

        bool IUserType.Equals(object x, object y)
        {
            return Equals(x, y);
        }

        public abstract object NullSafeGet(IDataReader rs, string[] names, object owner);

        public abstract void NullSafeSet(IDbCommand cmd, object value, int index);

        public virtual object Replace(object original, object target, object owner)
        {
            return original;
        }

        #endregion Methods
    }
}