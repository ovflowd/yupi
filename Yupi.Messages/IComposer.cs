using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages
{
	public interface IComposer<T>
	{
		string Name { get; }

		void Compose(ISession session, T data);
	}
}

