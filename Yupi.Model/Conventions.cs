using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.AcceptanceCriteria;

namespace Yupi.Model
{
	public class Conventions : IHasManyConvention, IHasManyToManyConvention
	{
		// TODO Use AttributePropertyConvention (https://github.com/jagregory/fluent-nhibernate/wiki/Available-conventions)
		// Taken from http://stackoverflow.com/questions/6091654/fluentnhibernate-automapping-onetomany-relation-using-attribute-and-convention
		public void Apply(IOneToManyCollectionInstance instance)
		{
			if (instance == null) {
				return;
			}

			var keyColumnAttribute = (OneToManyAttribute)Attribute.GetCustomAttribute(instance.Member, typeof(OneToManyAttribute));
			if (keyColumnAttribute != null)
			{ 
				instance.Key.Column(instance.EntityType.Name + "Ref");
			}
		}

		public void Apply(IManyToManyCollectionInstance instance)
		{
			if (instance == null) {
				return;
			}

			var keyColumnAttribute = (ManyToManyAttribute)Attribute.GetCustomAttribute(instance.Member, typeof(ManyToManyAttribute));
			if (keyColumnAttribute != null)
			{ 
				instance.Key.Column(instance.EntityType.Name + "ManyRef");
			}
		}
	}
}

