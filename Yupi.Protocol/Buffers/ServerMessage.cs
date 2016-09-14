// ---------------------------------------------------------------------------------
// <copyright file="ServerMessage.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeProject.ObjectPool;

namespace Yupi.Protocol.Buffers
{
    // TODO Refactor + implement pooled object properly
    public class ServerMessage : PooledObject, IDisposable
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
        public ServerMessage(short header)
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
        ///     Get the current messageBuffer.
        ///     When StartArray is called, it'll return _arrayCurrentBuffer. Else it will return _buffer.
        /// </summary>
        /// <value>The c messageBuffer.</value>
        private MemoryStream CurrentMessage => _onArray ? _arrayCurrentBuffer : _buffer;

        /// <summary>
        ///     Initializes the specified header.
        /// </summary>
        /// <param name="header">The header.</param>
        public void Init(short header)
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
                throw new InvalidOperationException("The array has already started.");

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
        ///     Appends the short.
        /// </summary>
        /// <param name="i">The i.</param>
        public void AppendShort(short value)
        {
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
        public void AppendInteger(uint i) => AppendInteger((int) i);

        /// <summary>
        ///     Appends the integer.
        /// </summary>
        /// <param name="i">if set to <c>true</c> [i].</param>
        public void AppendInteger(bool i) => AppendInteger(i ? 1 : 0);

        /*
        public void AppendIntegersArray(string str, char delimiter, int lenght, int defaultValue = 0, int maxValue = 0)
        {
            if (string.IsNullOrEmpty(str))
                throw new Exception("String is null or empty");

            string[] array = str.Split(delimiter);

            if (array.Length == 0)
                return;

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
*/

        /// <summary>
        ///     Appends the bool.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        public void AppendBool(bool b) => AppendByte(b ? 1 : 0);

        /// <summary>
        ///     Appends the string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="isUtf8">If string is UTF8</param>
        public void AppendString(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            // TODO Pay attention to length!
            AppendShort((short) bytes.Length);
            AppendBytes(bytes, false);
        }

        /// <summary>
        ///     Appends the bytes.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="isInt">if set to <c>true</c> [is int].</param>
        public void AppendBytes(byte[] b, bool isInt)
        {
            // TODO Proper BigEndian Encoding!
            if (isInt)
                Array.Reverse(b);

            CurrentMessage.Write(b, 0, b.Length);
        }

        /// <summary>
        ///     Appends the byted.
        /// </summary>
        /// <param name="number">The number.</param>
        public void AppendByte(int number) => CurrentMessage.WriteByte((byte) number);

        /// <summary>
        ///     Gets the bytes.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        //public byte[] GetBytes() => CurrentMessage.ToArray();
        /// <summary>
        ///     Gets the reversed bytes.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] GetReversedBytes()
        {
            byte[] bytes;
            // TODO Why do we need to copy
            using (MemoryStream finalBuffer = new MemoryStream())
            {
                byte[] length = BitConverter.GetBytes((int) CurrentMessage.Length);

                Array.Reverse(length);

                finalBuffer.Write(length, 0, length.Length);

                CurrentMessage.WriteTo(finalBuffer);

                bytes = finalBuffer.ToArray();
            }

            //if (Yupi.PacketDebugMode) {
            // string package = Encoding.UTF8.GetString (bytes);

            // TODO Packet debugging
            // TODO Escape special chars

            /*YupiWriterManager.WriteLine(
                $"Handled: {Id}: " + Environment.NewLine + package + Environment.NewLine,
                "Yupi.Outgoing", ConsoleColor.DarkGray);*/
            //}

            return bytes;
        }
    }
}