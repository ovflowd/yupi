// ---------------------------------------------------------------------------------
// <copyright file="EnumTypeConvention.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Collections.Generic;
    using System.Linq;

    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Inspections;

    using Yupi.Model.Domain;

    public class EnumTypeConvention : IUserTypeConvention
    {
        #region Methods

        public static bool IsEnumerationType(Type type)
        {
            return GetTypeHierarchy(type)
                .Where(t => t.IsGenericType)
                .Select(t => t.GetGenericTypeDefinition())
                .Any(t => t == typeof(Headspring.Enumeration<>));
        }

        public void Accept(
            FluentNHibernate.Conventions.AcceptanceCriteria.IAcceptanceCriteria
            <FluentNHibernate.Conventions.Inspections.IPropertyInspector> criteria)
        {
            criteria.Expect(x => IsEnumerationType(x.Property.PropertyType));
        }

        public void Apply(FluentNHibernate.Conventions.Instances.IPropertyInstance instance)
        {
            var closedType = typeof(EnumAsInt32<>).MakeGenericType(instance.Property.PropertyType);
            instance.CustomType(closedType);
        }

        private static IEnumerable<Type> GetTypeHierarchy(Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }

        #endregion Methods
    }
}