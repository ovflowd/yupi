using System;
using CodeProject.ObjectPool;
using Yupi.Protocol.Buffers;

namespace Yupi.Protocol
{
	public class ServerMessagePool
	{
		private const int minObjects = 5;
		private const int maxObjects = 20;

		private ObjectPool<ServerMessage> pool;

		public ServerMessagePool ()
		{
			pool = new ObjectPool<ServerMessage> (minObjects, maxObjects, () => new ServerMessage ());
		}

		public ServerMessage GetMessageBuffer(short id) {
			ServerMessage message = pool.GetObject ();
			message.Init (id);
			return message;
		}
	}
}

