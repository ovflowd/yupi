using System.Collections.Concurrent;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Messages.Factorys
{
    internal class ClientMessageFactory
    {
        private static ConcurrentQueue<SimpleClientMessageBuffer> _freeObjects;

        internal static void Init() => _freeObjects = new ConcurrentQueue<SimpleClientMessageBuffer>();

        internal static SimpleClientMessageBuffer GetClientMessage(int messageId, byte[] body, int position, int packetLength)
        {
            SimpleClientMessageBuffer messageBuffer;

            if (_freeObjects.Count > 0 && _freeObjects.TryDequeue(out messageBuffer))
            {
                messageBuffer.Init(messageId, body, position, packetLength);

                return messageBuffer;
            }

            return new SimpleClientMessageBuffer(messageId, body, position, packetLength);
        }

        internal static void ObjectCallback(SimpleClientMessageBuffer messageBuffer) => _freeObjects.Enqueue(messageBuffer);
    }
}