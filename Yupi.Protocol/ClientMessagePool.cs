using System;
using Yupi.Protocol.Buffers;
using CodeProject.ObjectPool;

namespace Yupi.Protocol
{
	public class ClientMessagePool
	{
		private const int minObjects = 5;
		private const int maxObjects = 20;

		private ObjectPool<ClientMessage> pool;

		public ClientMessagePool ()
		{
			pool = new ObjectPool<ClientMessage> (minObjects, maxObjects, () => new ClientMessage ());
		}

		public ClientMessage GetMessageBuffer() {
			return pool.GetObject ();
		}
	}
}

