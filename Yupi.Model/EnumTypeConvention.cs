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