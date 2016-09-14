using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using System.Net;

namespace Yupi.Model
{
    public class IPAddressConvention : IUserTypeConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType<IpAddressAsString>();
        }

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType == typeof(IPAddress));
        }
    }
}