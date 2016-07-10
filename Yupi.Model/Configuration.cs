using System;
using FluentNHibernate.Automapping;
using Yupi.Model.Domain.Rooms;
using FluentNHibernate.Utils;
using FluentNHibernate.Data;

namespace Yupi.Model
{
	public class ORMConfiguration : DefaultAutomappingConfiguration
	{
		public override bool ShouldMap(Type type)
		{
			return type.Namespace.StartsWith ("Yupi.Model.Domain") 
				&& !type.IsEnum; // Don't map enums. These will be mapped automatically where required.
		}

		public override bool IsComponent (Type type)
		{
			return type.Namespace.EndsWith ("Components");
		}
	}
}

