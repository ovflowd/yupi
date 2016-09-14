using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Protocol
{
	public interface ISender
	{
		void Send (ServerMessage message);
	}
}

