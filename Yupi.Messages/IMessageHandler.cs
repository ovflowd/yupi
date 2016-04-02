using System;
using Yupi.Protocol.Buffers;
using Yupi.Net;

namespace Yupi.Messages
{
	public interface IMessageHandler
	{
		string Name { get; }

		void HandleMessage(ISession session, ClientMessage message);
	}
}

