using System.Collections;

namespace Yupi.Data
{
    /// <summary>
    /// Class ServerMemoryContainer.
    /// </summary>
    public class ServerMemoryContainer
    {
        /// <summary>
        /// The _container
        /// </summary>
        private readonly Queue _container;
        /// <summary>
        /// The _buffer size
        /// </summary>
        private readonly int _bufferSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMemoryContainer"/> class.
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
        /// Takes the buffer.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] TakeBuffer()
        {
            if (_container.Count > 0)
                lock (_container.SyncRoot)
                    return (byte[])_container.Dequeue();

            return new byte[_bufferSize];
        }

        /// <summary>
        /// Gives the buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void GiveBuffer(byte[] buffer)
        {
            lock (_container.SyncRoot)
                _container.Enqueue(buffer);
        }
    }
}