namespace Yupi.Model
{
    using System;
    using System.Data;
    using System.Diagnostics;

    using Headspring;

    using NHibernate;
    using NHibernate.SqlTypes;

    public class EnumAsInt32<T> : UserType
        where T : Headspring.Enumeration<T>
    {
        #region Properties

        public override Type ReturnedType
        {
            get { return typeof(T); }
        }

        public override SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.Int32}; }
        }

        #endregion Properties

        #region Methods

        public override object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            object obj = NHibernateUtil.Int32.NullSafeGet(rs, names);
            return Enumeration<T>.FromValue(Convert.ToInt32(obj));
        }

        public override void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            Debug.Assert(cmd != null);
            if (value == null)
            {
                ((IDataParameter) cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                ((IDataParameter) cmd.Parameters[index]).Value = ((T) value).Value;
            }
        }

        #endregion Methods
    }
}