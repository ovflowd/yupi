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

using System.Collections.Generic;
using System;

namespace Yupi.Protocol.Buffers
{
    /// <summary>
    ///     Class QueuedServerMessageBuffer.
    /// </summary>
    public class QueuedServerMessageBuffer : IDisposable
    {
        /// <summary>
        ///     The _packet
        /// </summary>
        private List<byte> _packet;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QueuedServerMessageBuffer" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
		public QueuedServerMessageBuffer()
        {
            _packet = new List<byte>();
        }

        /// <summary>
        ///     Gets the get packet.
        /// </summary>
        /// <value>The get packet.</value>
        internal byte[] GetPacket => _packet.ToArray();

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            _packet = null;
        }
			
        /// <summary>
        ///     Appends the response.
        /// </summary>
        /// <param name="messageBuffer">The messageBuffer.</param>
		internal void AppendResponse(ServerMessage messageBuffer) {
			AppendBytes (messageBuffer.GetReversedBytes ());
		}

		// TODO Remove alias
        /// <summary>
        ///     Adds the bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
		internal void AddBytes(byte[] bytes) {
			AppendBytes (bytes);
		}

        /// <summary>
        ///     Appends the bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
		private void AppendBytes(IEnumerable<byte> bytes) {
			_packet.AddRange (bytes);
		}
    }
}