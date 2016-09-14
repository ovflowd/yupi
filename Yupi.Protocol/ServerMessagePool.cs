namespace Yupi.Protocol
{
    using System;

    using CodeProject.ObjectPool;

    using Yupi.Protocol.Buffers;

    public class ServerMessagePool
    {
        #region Fields

        private const int maxObjects = 20;
        private const int minObjects = 5;

        private ObjectPool<ServerMessage> pool;

        #endregion Fields

        #region Constructors

        public ServerMessagePool()
        {
            pool = new ObjectPool<ServerMessage>(minObjects, maxObjects, () => new ServerMessage());
        }

        #endregion Constructors

        #region Methods

        public ServerMessage GetMessageBuffer(short id)
        {
            ServerMessage message = pool.GetObject();
            message.Init(id);
            return message;
        }

        #endregion Methods
    }
}