using System;
using FluentNHibernate.Automapping;
using Yupi.Model.Rooms;
using FluentNHibernate.Utils;
using FluentNHibernate.Data;

namespace Yupi.Model
{
	public class ORMConfiguration : DefaultAutomappingConfiguration
	{
		public override bool ShouldMap(Type type)
		{
			return typeof(Entity).IsAssignableFrom(type);
		}

		public override bool IsComponent (Type type)
		{
			return type == typeof(Vector);
		}
	}
}

