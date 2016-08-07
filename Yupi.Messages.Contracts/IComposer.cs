using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
	public interface IComposer
	{
		void Init(short id, ServerMessagePool pool);
	}
}

