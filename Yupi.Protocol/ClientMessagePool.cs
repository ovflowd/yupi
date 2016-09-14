using Yupi.Protocol.Buffers;

namespace Yupi.Protocol
{
    public class ClientMessagePool
    {
        private const int minObjects = 5;
        private const int maxObjects = 20;

        private readonly ObjectPool<ClientMessage> pool;

        public ClientMessagePool()
        {
            pool = new ObjectPool<ClientMessage>(minObjects, maxObjects, () => new ClientMessage());
        }

        public ClientMessage GetMessageBuffer()
        {
            return pool.GetObject();
        }
    }
}