using System;
using NHibernate;
using NHibernate.SqlTypes;
using System.Diagnostics;
using System.Data;
using Headspring;

namespace Yupi.Model
{
    public class EnumAsInt32<T> : UserType where T : Headspring.Enumeration<T>
    {
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

        public override SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.Int32}; }
        }

        public override Type ReturnedType
        {
            get { return typeof(T); }
        }
    }
}