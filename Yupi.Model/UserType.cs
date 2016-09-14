using System;
using NHibernate.UserTypes;
using System.Data;
using NHibernate.SqlTypes;

namespace Yupi.Model
{
	public abstract class UserType : IUserType
	{
		#region Implementation of IUserType

		bool IUserType.Equals(object x, object y)
		{
			return Equals(x, y);
		}

		public int GetHashCode(object x)
		{
			return x == null ? 0 : x.GetHashCode();
		}

		public abstract object NullSafeGet(IDataReader rs, string[] names, object owner);

		public abstract void NullSafeSet(IDbCommand cmd, object value, int index);

		public virtual object DeepCopy(object value)
		{
			return value;
		}

		public virtual object Replace(object original, object target, object owner)
		{
			return original;
		}

		public virtual object Assemble(object cached, object owner)
		{
			return cached;
		}

		public virtual object Disassemble(object value)
		{
			return value;
		}

		/// <summary>
		/// The SQL types for the columns mapped by this type.
		/// </summary>
		public abstract SqlType[] SqlTypes{ get;}

		/// <summary>
		/// The type returned by <c>NullSafeGet()</c>
		/// </summary>
		public abstract Type ReturnedType { get; }

		public bool IsMutable
		{
			get { return false; }
		}
		#endregion
	}

}

