// ---------------------------------------------------------------------------------
// <copyright file="Vector3UserType.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model
{
    using System;
    using System.Data;
    using System.Numerics;

    using NHibernate;
    using NHibernate.SqlTypes;
    using NHibernate.Type;
    using NHibernate.UserTypes;

    public class Vector3UserType : ICompositeUserType
    {
        #region Properties

        public bool IsMutable
        {
            get { return false; }
        }

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

        #endregion Properties

        #region Methods

        public object Assemble(object cached, NHibernate.Engine.ISessionImplementor session, object owner)
        {
            return cached;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value, NHibernate.Engine.ISessionImplementor session)
        {
            return value;
        }

        public int GetHashCode(object x)
        {
            return x == null ? 0 : x.GetHashCode();
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

        bool ICompositeUserType.Equals(object x, object y)
        {
            return Equals(x, y);
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
                float X = (Single) Convert.ToDouble(rs[x].ToString());
                float Y = (Single) Convert.ToDouble(rs[y].ToString());
                float Z = (Single) Convert.ToDouble(rs[z].ToString());
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

        public object Replace(object original, object target, NHibernate.Engine.ISessionImplementor session,
            object owner)
        {
            return original;
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

        #endregion Methods
    }
}