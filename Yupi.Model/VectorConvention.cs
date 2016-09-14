namespace Yupi.Model
{
    using System;
    using System.Numerics;

    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;

    public class VectorConvention : IUserTypeConvention
    {
        #region Methods

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof(Vector3));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType<Vector3UserType>();
        }

        #endregion Methods
    }
}