using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Protocol;

namespace Yupi.Messages
{
	public interface IComposer
	{
		void Init(short id, ServerMessagePool pool);
	}
}

