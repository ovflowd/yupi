using System;
using System.Collections.Generic;
using System.Linq;
using Headspring;

namespace Yupi.Model
{
    public class EnumTypeConvention : IUserTypeConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IPropertyInstance instance)
        {
            var closedType = typeof(EnumAsInt32<>).MakeGenericType(instance.Property.PropertyType);
            instance.CustomType(closedType);
        }

        public void Accept(
            FluentNHibernate.Conventions.AcceptanceCriteria.IAcceptanceCriteria
                <FluentNHibernate.Conventions.Inspections.IPropertyInspector> criteria)
        {
            criteria.Expect(x => IsEnumerationType(x.Property.PropertyType));
        }

        public static bool IsEnumerationType(Type type)
        {
            return GetTypeHierarchy(type)
                .Where(t => t.IsGenericType)
                .Select(t => t.GetGenericTypeDefinition())
                .Any(t => t == typeof(Enumeration<>));
        }

        private static IEnumerable<Type> GetTypeHierarchy(Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}