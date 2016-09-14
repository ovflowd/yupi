namespace Yupi.Model
{
    using System;
    using System.Net;

    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;

    public class IPAddressConvention : IUserTypeConvention
    {
        #region Methods

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof(IPAddress));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType<IpAddressAsString>();
        }

        #endregion Methods
    }
}