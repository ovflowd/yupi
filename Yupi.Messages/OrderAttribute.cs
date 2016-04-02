using System;
using System.Runtime.CompilerServices;

namespace Yupi.Messages
{
	/// <summary>
	/// Order attribute used for serializing messages
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class OrderAttribute : Attribute
	{
		private readonly int order_;
		public OrderAttribute([CallerLineNumber]int order = 0)
		{
			order_ = order;
		}

		public int Order { get { return order_; } }
	}
}

