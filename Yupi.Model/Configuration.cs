using System;
using FluentNHibernate.Automapping;

namespace Yupi.Model
{
	public class Configuration : DefaultAutomappingConfiguration
	{
		public override bool IsComponent (Type type)
		{
			return type == typeof(Vector<>);
		}
	}
}

