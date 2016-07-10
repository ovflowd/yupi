using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Yupi.Model
{
	public class Conventions : IHasManyConvention, IHasManyToManyConvention
	{
		// Taken from http://stackoverflow.com/questions/6091654/fluentnhibernate-automapping-onetomany-relation-using-attribute-and-convention
		public void Apply(IOneToManyCollectionInstance instance)
		{
			var keyColumnAttribute = (OneToManyAttribute)Attribute.GetCustomAttribute(instance.Member, typeof(OneToManyAttribute));
			if (keyColumnAttribute != null)
			{ 
				instance.Key.Column(instance.EntityType.Name + "Ref");
			}
		}

		public void Apply(IManyToManyCollectionInstance instance)
		{
			var keyColumnAttribute = (ManyToManyAttribute)Attribute.GetCustomAttribute(instance.Member, typeof(ManyToManyAttribute));
			if (keyColumnAttribute != null)
			{ 
				instance.Key.Column(instance.EntityType.Name + "Ref");
			}
		}
	}
}

