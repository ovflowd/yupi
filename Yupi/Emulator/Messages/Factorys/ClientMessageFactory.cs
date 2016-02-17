using System.Collections.Concurrent;

namespace Yupi.Messages.Factorys
{
    internal class ClientMessageFactory
    {
        private static ConcurrentQueue<ClientMessage> _freeObjects;

        internal static void Init()
        {
            _freeObjects = new ConcurrentQueue<ClientMessage>();
        }

        internal static ClientMessage GetClientMessage(int messageId, byte[] body, int position, int packetLength)
        {
            ClientMessage message;

            if (_freeObjects.Count > 0 && _freeObjects.TryDequeue(out message))
            {
                message.Init(messageId, body, position, packetLength);
                return message;
            }

            return new ClientMessage(messageId, body, position, packetLength);
        }

        internal static void ObjectCallback(ClientMessage message)
        {
            _freeObjects.Enqueue(message);
        }
    }
}