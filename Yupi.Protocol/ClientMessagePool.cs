namespace Yupi.Protocol
{
    using System;

    using CodeProject.ObjectPool;

    using Yupi.Protocol.Buffers;

    public class ClientMessagePool
    {
        #region Fields

        private const int maxObjects = 20;
        private const int minObjects = 5;

        private ObjectPool<ClientMessage> pool;

        #endregion Fields

        #region Constructors

        public ClientMessagePool()
        {
            pool = new ObjectPool<ClientMessage>(minObjects, maxObjects, () => new ClientMessage());
        }

        #endregion Constructors

        #region Methods

        public ClientMessage GetMessageBuffer()
        {
            return pool.GetObject();
        }

        #endregion Methods
    }
}