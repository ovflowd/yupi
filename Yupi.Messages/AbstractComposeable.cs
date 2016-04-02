using System;
using System.Linq;
using System.Reflection;
using Yupi.Net;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages
{
	// TODO Rethink this...
	public abstract class AbstractComposeable : IComposeable
	{
		public abstract string Name { get; }

		private IOrderedEnumerable<PropertyInfo> properties;

		public AbstractComposeable ()
		{
			properties = this.GetType()
				.GetProperties()
				.OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false)[0]).Order);
		}

		public void BuildMessage(SimpleServerMessageBuffer message)
		{
			foreach (PropertyInfo property in properties) {
				TypeCode typeCode = Type.GetTypeCode (property.PropertyType);

				object value = property.GetValue (this);

				switch (typeCode) {
				case TypeCode.Boolean:
					message.AppendBool ((bool)value);
					break;
				case TypeCode.Int16:
					message.AppendShort ((short)value);
					break;
				case TypeCode.Int32:
					message.AppendInteger((int)value);
					break;
				case TypeCode.UInt32:
					message.AppendInteger((uint)value);
					break;
				case TypeCode.String:
					message.AppendString ((string)value);
					break;
				case TypeCode.Object:
					if(property.PropertyType == typeof(SimpleServerMessageBuffer)) {
						message.AppendServerMessage ((SimpleServerMessageBuffer)value);
					}
					break;
				}
			}
		}
	}
}

