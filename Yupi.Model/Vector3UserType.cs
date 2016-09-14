using System;

namespace Yupi.Model
{
    public class Vector3UserType : ICompositeUserType
    {
        public string[] PropertyNames
        {
            get { return new[] {"X", "Y", "Z"}; }
        }

        public IType[] PropertyTypes
        {
            get { return new[] {NHibernateUtil.Single, NHibernateUtil.Single, NHibernateUtil.Single}; }
        }

        public Type ReturnedClass
        {
            get { return typeof(Vector3); }
        }

        public bool IsMutable
        {
            get { return false; }
        }

        bool ICompositeUserType.Equals(object x, object y)
        {
            return Equals(x, y);
        }

        public object GetPropertyValue(object component, int property)
        {
            Vector3 vector = (Vector3) component;
            switch (property)
            {
                case 0:
                    return vector.X;
                case 1:
                    return vector.Y;
                case 2:
                    return vector.Z;
                default:
                    throw new Exception("No implementation for property index of '" + property + "'.");
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            Vector3 vector = (Vector3) component;

            switch (property)
            {
                case 0:
                    vector.X = (float) value;
                    break;
                case 1:
                    vector.Y = (float) value;
                    break;
                case 2:
                    vector.Z = (float) value;
                    break;
                default:
                    throw new Exception("No implementation for property index of '" + property + "'.");
            }
        }

        public int GetHashCode(object x)
        {
            return x == null ? 0 : x.GetHashCode();
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value, NHibernate.Engine.ISessionImplementor session)
        {
            return value;
        }

        public object Assemble(object cached, NHibernate.Engine.ISessionImplementor session, object owner)
        {
            return cached;
        }

        public object Replace(object original, object target, NHibernate.Engine.ISessionImplementor session,
            object owner)
        {
            return original;
        }

        public object NullSafeGet(IDataReader rs, string[] names, NHibernate.Engine.ISessionImplementor session,
            object owner)
        {
            object vector = null;

            int x = rs.GetOrdinal(names[0]);
            int y = rs.GetOrdinal(names[1]);
            int z = rs.GetOrdinal(names[2]);
            if (!rs.IsDBNull(x) && !rs.IsDBNull(y) && !rs.IsDBNull(z))
            {
                var X = (float) Convert.ToDouble(rs[x].ToString());
                var Y = (float) Convert.ToDouble(rs[y].ToString());
                var Z = (float) Convert.ToDouble(rs[z].ToString());
                vector = new Vector3(X, Y, Z);
            }
            return vector;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index, bool[] settable,
            NHibernate.Engine.ISessionImplementor session)
        {
            Vector3 vector = (Vector3) value;
            ((IDataParameter) cmd.Parameters[index]).Value = vector.X;
            ((IDataParameter) cmd.Parameters[index + 1]).Value = vector.Y;
            ((IDataParameter) cmd.Parameters[index + 2]).Value = vector.Z;
        }
    }
}