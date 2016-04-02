using System;
using Yupi.Messages.Buffers;
using CodeProject.ObjectPool;

namespace Yupi.Protocol
{
	public class ClientMessagePool
	{
		private const int minObjects = 5;
		private const int maxObjects = 20;

		private ObjectPool<SimpleClientMessageBuffer> pool;

		public ClientMessagePool ()
		{
			pool = new ObjectPool<SimpleClientMessageBuffer> (minObjects, maxObjects, () => new SimpleClientMessageBuffer ());
		}

		public SimpleClientMessageBuffer GetMessageBuffer() {
			return pool.GetObject ();
		}
	}
}

