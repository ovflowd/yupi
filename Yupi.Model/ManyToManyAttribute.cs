using System;

namespace Yupi.Model
{
	[System.AttributeUsage(System.AttributeTargets.Property)]
	public sealed class ManyToManyAttribute : Attribute
	{
		public ManyToManyAttribute ()
		{
		}
	}
}

