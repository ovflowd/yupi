using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages
{
	public interface IComposeable
	{
		string Name { get; }

		void BuildMessage(SimpleServerMessageBuffer message);
	}
}

