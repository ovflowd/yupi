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

using System.Collections;

namespace Yupi.Data
{
    /// <summary>
    ///     Class ServerMemoryContainer.
    /// </summary>
    public class ServerMemoryContainer
    {
        /// <summary>
        ///     The _buffer size
        /// </summary>
        private readonly int _bufferSize;

        /// <summary>
        ///     The _container
        /// </summary>
        private readonly Queue _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerMemoryContainer" /> class.
        /// </summary>
        /// <param name="initSize"></param>
        /// <param name="bufferSize">
        ///     Size of the initialize.
        ///     Size of the buffer.
        /// </param>
        public ServerMemoryContainer(int initSize, int bufferSize)
        {
            _container = new Queue(initSize);
            _bufferSize = bufferSize;

            for (int i = 0; i < initSize; i++)
                _container.Enqueue(new byte[bufferSize]);
        }

        /// <summary>
        ///     Takes the buffer.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] TakeBuffer()
        {
            if (_container.Count > 0)
                lock (_container.SyncRoot)
                    return (byte[]) _container.Dequeue();

            return new byte[_bufferSize];
        }

        /// <summary>
        ///     Gives the buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void GiveBuffer(byte[] buffer)
        {
            lock (_container.SyncRoot)
                _container.Enqueue(buffer);
        }
    }
}