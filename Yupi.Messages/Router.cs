using System;
using System.Linq;
using System.Collections.Generic;

namespace Yupi.Messages
{
	public class Router
	{
		private Dictionary<int, IMessageHandler> incoming;

		public Router ()
		{
		}

		private void LoadHandlers() {
			incoming = new Dictionary<int, IMessageHandler> ();

			IEnumerable<Type> handlers = GetType ().Assembly.GetTypes ().Where (p => typeof(IMessageHandler).IsAssignableFrom (p));

			foreach (Type handlerType in handlers) {
				IMessageHandler handler = (IMessageHandler)Activator.CreateInstance(handlerType);
				incoming.Add(GetIncomingId(handler.Name), handler);
			}
		}

		private int GetIncomingId(string name) {
			throw new NotImplementedException();
		}
	}
}

