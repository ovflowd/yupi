namespace Yupi.Model
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Net;

    using NHibernate;
    using NHibernate.SqlTypes;

    /// <summary>
    /// Stores System.Net.IPAddress as String
    /// </summary>
    /// <see cref="http://t-code.pl/blog/2011/07/ipaddress-nvarchar-nhibernate-custom-mapping/"/>
    public class IpAddressAsString : UserType
    {
        #region Properties

        public override Type ReturnedType
        {
            get { return typeof(IPAddress); }
        }

        public override SqlType[] SqlTypes
        {
            get { return new SqlType[] {SqlTypeFactory.GetString(15)}; }
        }

        #endregion Properties

        #region Methods

        public override object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            object obj = NHibernateUtil.Double.NullSafeGet(rs, names);
            if (obj == null)
            {
                return null;
            }
            return IPAddress.Parse(obj.ToString());
        }

        public override void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            Debug.Assert(cmd != null);
            if (value == null)
            {
                ((IDataParameter) cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                ((IDataParameter) cmd.Parameters[index]).Value = value.ToString();
            }
        }

        #endregion Methods
    }
}