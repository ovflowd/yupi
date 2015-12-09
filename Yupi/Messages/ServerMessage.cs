using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yupi.Messages
{
    /// <summary>
    /// Class ServerMessage.
    /// </summary>
    internal class ServerMessage : IDisposable
    {
        /// <summary>
        /// The _message
        /// </summary>
        private List<byte> _message = new List<byte>(), _messageArray, _messageArrayJunk;

        /// <summary>
        /// The _on array
        /// </summary>
        private bool _onArray, _disposed;

        /// <summary>
        /// The _array count
        /// </summary>
        private int _arrayCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMessage"/> class.
        /// </summary>
        public ServerMessage()
        {
            Id = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMessage"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        public ServerMessage(int header)
        {
            Id = 0;
            Init(header);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the c message.
        /// </summary>
        /// <value>The c message.</value>
        private List<byte> CMessage
        {
            get
            {
                if (_onArray)
                    return _messageArrayJunk;

                return _message;
            }
        }

        /// <summary>
        /// Initializes the specified header.
        /// </summary>
        /// <param name="header">The header.</param>
        public void Init(int header)
        {
            _message = new List<byte>();
            Id = header;
            AppendShort(header);
        }

        /// <summary>
        /// Sets the pointer to a Temporary Buffer
        /// </summary>
        public void StartArray()
        {
            _onArray = true;
            _arrayCount = 0;

            _messageArray = new List<byte>();
            _messageArrayJunk = new List<byte>();
        }

        /// <summary>
        /// Saves the Temporary Buffer in a Safe Buffer (not main)
        /// and cleans the Temporal Buffer.
        /// </summary>
        public void SaveArray()
        {
            if (_onArray == false || !_messageArrayJunk.Any())
                return;

            _messageArray.AddRange(_messageArrayJunk);
            _messageArrayJunk.Clear();

            _arrayCount++;
        }

        /// <summary>
        /// Cleans the Temporal Buffer.
        /// </summary>
        public void Clear()
        {
            if (_onArray == false)
                return;

            _messageArrayJunk.Clear();
        }

        /// <summary>
        /// Saves the Safe Buffer to Main Buffer
        /// After disposes the other buffers.
        /// </summary>
        public void EndArray()
        {
            if (_onArray == false)
                return;

            _onArray = false;

            AppendInteger(_arrayCount);
            _message.AddRange(_messageArray);

            _messageArray.Clear();
            _messageArrayJunk.Clear();
            _messageArray = _messageArrayJunk = null;
        }

        /// <summary>
        /// Appends the server message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AppendServerMessage(ServerMessage message)
        {
            AppendBytes(message.GetBytes(), false);
        }

        /// <summary>
        /// Appends the server messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public void AppendServerMessages(List<ServerMessage> messages)
        {
            foreach (ServerMessage message in messages)
                AppendServerMessage(message);
        }

        /// <summary>
        /// Appends the short.
        /// </summary>
        /// <param name="i">The i.</param>
        public void AppendShort(int i)
        {
            short value = (short)i;

            AppendBytes(BitConverter.GetBytes(value), true);
        }

        /// <summary>
        /// Appends the integer.
        /// </summary>
        /// <param name="value">The i.</param>
        public void AppendInteger(int value)
        {
            AppendBytes(BitConverter.GetBytes(value), true);
        }

        /// <summary>
        /// Appends the integer.
        /// </summary>
        /// <param name="i">The i.</param>
        public void AppendInteger(uint i)
        {
            AppendInteger((int)i);
        }

        /// <summary>
        /// Appends the integer.
        /// </summary>
        /// <param name="i">if set to <c>true</c> [i].</param>
        public void AppendInteger(bool i)
        {
            AppendInteger(i ? 1 : 0);
        }

        public void AppendIntegersArray(string str, char delimiter, int lenght, int defaultValue = 0, int maxValue = 0)
        {
            if (string.IsNullOrEmpty(str))
                throw new Exception("String is null or empty");

            string[] array = str.Split(delimiter);

            if (array.Length == 0)
                return;

            uint i = 0u;

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
        /// Appends the bool.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        public void AppendBool(bool b)
        {
            AppendBytes(new[] { (byte)(b ? 1 : 0) }, false);
        }

        /// <summary>
        /// Appends the string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="isUtf8">If string is UTF8</param>
        public void AppendString(string s, bool isUtf8 = false)
        {
            var toAdd = isUtf8 ? Yupi.GetDefaultEncoding().GetBytes(s) : Encoding.UTF8.GetBytes(s);

            AppendShort(toAdd.Length);
            AppendBytes(toAdd, false);
        }

        /// <summary>
        /// Appends the bytes.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="isInt">if set to <c>true</c> [is int].</param>
        public void AppendBytes(byte[] b, bool isInt)
        {
            if (isInt)
            {
                for (var i = (b.Length - 1); i > -1; i--)
                    CMessage.Add(b[i]);
            }
            else
                CMessage.AddRange(b);
        }

        /// <summary>
        /// Appends the byted.
        /// </summary>
        /// <param name="number">The number.</param>
        public void AppendByte(int number)
        {
            AppendBytes(new[] { (byte)number }, false);
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] GetBytes() => CMessage.ToArray();

        /// <summary>
        /// Gets the reversed bytes.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] GetReversedBytes()
        {
            List<byte> final = new List<byte>();
            final.AddRange(BitConverter.GetBytes(CMessage.Count));
            final.Reverse();
            final.AddRange(_message);

            if (Yupi.DebugMode)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine();
                Console.Write("OUTGOING ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("PREPARED ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Id + Environment.NewLine + HabboEncoding.GetCharFilter(Yupi.GetDefaultEncoding().GetString(final.ToArray())));
                Console.WriteLine();
            }

            return final.ToArray();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => HabboEncoding.GetCharFilter(Yupi.GetDefaultEncoding().GetString(GetReversedBytes()));

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _message.Clear();

            if (_onArray)
            {
                _messageArray.Clear();
                _messageArrayJunk.Clear();
            }

            _message = _messageArray = _messageArrayJunk = null;
            _disposed = true;
        }
    }
}