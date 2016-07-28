using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Protocol
{
	public interface ISession<T> : Yupi.Net.ISession<T>
	{
		void Send (ServerMessage message);
	}
}

