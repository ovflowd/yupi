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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Yupi.Messages
{
    /// <summary>
    ///     Class ServerMessage.
    /// </summary>
    /// <summary>
    ///     Class ServerMessage.
    /// </summary>
    internal class ServerMessage : IDisposable
    {
        /// <summary>
        ///     The buffer for the ServerMessage.
        /// </summary>
        private readonly MemoryStream _buffer;

        /// <summary>
        ///     The buffer for the Arrays.
        /// </summary>
        private MemoryStream _arrayBuffer;

        /// <summary>
        ///     The _array count
        /// </summary>
        private int _arrayCount;

        /// <summary>
        ///     The current buffer for the Arrays.
        /// </summary>
        private MemoryStream _arrayCurrentBuffer;

        /// <summary>
        ///     The _on array
        /// </summary>
        private bool _onArray, _disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerMessage" /> class.
        /// </summary>
        public ServerMessage()
        {
            Id = 0;
            _buffer = new MemoryStream();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerMessage" /> class.
        /// </summary>
        /// <param name="header">The header.</param>
        public ServerMessage(int header)
            : this()
        {
            Init(header);
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; private set; }

        /// <summary>
        ///     Get the current message.
        ///     When StartArray is called, it'll return _arrayCurrentBuffer. Else it will return _buffer.
        /// </summary>
        /// <value>The c message.</value>
        private MemoryStream CurrentMessage
        {
            get { return _onArray ? _arrayCurrentBuffer : _buffer; }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _buffer.Dispose();

            if (_onArray)
            {
                _arrayBuffer.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        ///     Initializes the specified header.
        /// </summary>
        /// <param name="header">The header.</param>
        public void Init(int header)
        {
            _buffer.SetLength(0);
            Id = header;
            AppendShort(header);
        }

        /// <summary>
        ///     Sets the pointer to a Temporary Buffer
        /// </summary>
        public void StartArray()
        {
            if (_onArray)
            {
                throw new InvalidOperationException("The array has already started.");
            }

            _onArray = true;
            _arrayCount = 0;

            _arrayBuffer = new MemoryStream();
            _arrayCurrentBuffer = new MemoryStream();
        }

        /// <summary>
        ///     Saves the Temporary Buffer in a Safe Buffer (not main)
        ///     and cleans the Temporal Buffer.
        /// </summary>
        public void SaveArray()
        {
            if (_onArray == false || _arrayCurrentBuffer.Length == 0)
                return;

            _arrayCurrentBuffer.WriteTo(_arrayBuffer);
            _arrayCurrentBuffer.SetLength(0);
            _arrayCount++;
        }

        /// <summary>
        ///     Cleans the Temporal Buffer.
        /// </summary>
        public void Clear()
        {
            if (_onArray == false)
                return;

            _arrayCurrentBuffer.SetLength(0);
        }

        /// <summary>
        ///     Saves the Safe Buffer to Main Buffer
        ///     After disposes the other buffers.
        /// </summary>
        public void EndArray()
        {
            if (_onArray == false)
                return;

            _onArray = false;

            AppendInteger(_arrayCount);

            _arrayBuffer.WriteTo(_buffer);
            _arrayBuffer.Dispose();
            _arrayBuffer = null;

            _arrayCurrentBuffer.Dispose();
            _arrayCurrentBuffer = null;
        }

        /// <summary>
        ///     Appends the server message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AppendServerMessage(ServerMessage message)
        {
            AppendBytes(message.GetBytes(), false);
        }

        /// <summary>
        ///     Appends the server messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public void AppendServerMessages(List<ServerMessage> messages)
        {
            foreach (ServerMessage message in messages)
            {
                AppendServerMessage(message);
            }
        }

        /// <summary>
        ///     Appends the short.
        /// </summary>
        /// <param name="i">The i.</param>
        public void AppendShort(int i)
        {
            short value = (short) i;

            AppendBytes(BitConverter.GetBytes(value), true);
        }

        /// <summary>
        ///     Appends the integer.
        /// </summary>
        /// <param name="value">The i.</param>
        public void AppendInteger(int value)
        {
            AppendBytes(BitConverter.GetBytes(value), true);
        }

        /// <summary>
        ///     Appends the integer.
        /// </summary>
        /// <param name="i">The i.</param>
        public void AppendInteger(uint i)
        {
            AppendInteger((int) i);
        }

        /// <summary>
        ///     Appends the integer.
        /// </summary>
        /// <param name="i">if set to <c>true</c> [i].</param>
        public void AppendInteger(bool i)
        {
            AppendInteger(i ? 1 : 0);
        }

        public void AppendIntegersArray(string str, char delimiter, int lenght, int defaultValue = 0, int maxValue = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("String is null or empty");
            }

            string[] array = str.Split(delimiter);

            if (array.Length == 0)
            {
                return;
            }

            uint i = 0;

            foreach (string text in array.TakeWhile(text => i != lenght))
            {
                i++;

                int value;
                if (!int.TryParse(text, out value))
                    value = defaultValue;

                if (maxValue != 0 && value > maxValue)
                    value = maxValue;

                AppendInteger(value);
            }
        }

        /// <summary>
        ///     Appends the bool.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        public void AppendBool(bool b)
        {
            AppendByte(b ? 1 : 0);
        }

        /// <summary>
        ///     Appends the string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="isUtf8">If string is UTF8</param>
        public void AppendString(string s, bool isUtf8 = false)
        {
            Encoding encoding = isUtf8 ? Encoding.UTF8 : Yupi.GetDefaultEncoding();

            byte[] bytes = encoding.GetBytes(s);
            AppendShort(bytes.Length);
            AppendBytes(bytes, false);
        }

        /// <summary>
        ///     Appends the bytes.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="isInt">if set to <c>true</c> [is int].</param>
        public void AppendBytes(byte[] b, bool isInt)
        {
            if (isInt)
            {
                Array.Reverse(b);
            }

            CurrentMessage.Write(b, 0, b.Length);
        }

        /// <summary>
        ///     Appends the byted.
        /// </summary>
        /// <param name="number">The number.</param>
        public void AppendByte(int number)
        {
            CurrentMessage.WriteByte((byte) number);
        }

        /// <summary>
        ///     Gets the bytes.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] GetBytes() => CurrentMessage.ToArray();

        /// <summary>
        ///     Gets the reversed bytes.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] GetReversedBytes()
        {
            byte[] bytes;

            using (MemoryStream finalBuffer = new MemoryStream())
            {
                byte[] length = BitConverter.GetBytes((int) CurrentMessage.Length);
                Array.Reverse(length);
                finalBuffer.Write(length, 0, length.Length);

                CurrentMessage.WriteTo(finalBuffer);

                bytes = finalBuffer.ToArray();
            }

            if (Yupi.PacketDebugMode)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine();
                Console.Write("OUTGOING ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("PREPARED ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Id + Environment.NewLine +
                              HabboEncoding.GetCharFilter(Yupi.GetDefaultEncoding().GetString(bytes)));
                Console.WriteLine();
            }

            return bytes;
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
            => HabboEncoding.GetCharFilter(Yupi.GetDefaultEncoding().GetString(GetReversedBytes()));
    }
}