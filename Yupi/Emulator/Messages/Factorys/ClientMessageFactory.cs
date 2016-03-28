/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

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