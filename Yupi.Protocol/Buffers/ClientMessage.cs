// ---------------------------------------------------------------------------------
// <copyright file="ClientMessage.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Protocol.Buffers
{
    using System;
    using System.Text;

    using CodeProject.ObjectPool;

    using Yupi.Net;

    /// <summary>
    ///     Class SimpleClientMessageBuffer.
    /// </summary>
    public class ClientMessage : PooledObject
    {
        #region Fields

        /// <summary>
        ///     The _body
        /// </summary>
        private byte[] _body;

        /// <summary>
        ///     The _position
        /// </summary>
        private int _position;

        #endregion Fields

        #region Properties

        public short Id
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public byte[] GetBody()
        {
            return _body;
        }

        public bool GetBool()
        {
            return _body[_position++] == 1;
        }

        public byte[] GetBytes(int len)
        {
            byte[] arrayBytes = new byte[len];
            int pos = _position;

            for (int i = 0; i < len; i++)
            {
                arrayBytes[i] = _body[pos];

                pos++;
            }

            return arrayBytes;
        }

        public int GetInteger()
        {
            int value = BinaryHelper.ToInt(_body, _position);
            _position += 4;
            return value;
        }

        // TODO Probably inappropriate
        public bool GetIntegerAsBool()
        {
            return GetInteger() == 1;
        }

        public short GetShort()
        {
            short value = BinaryHelper.ToShort(_body, _position);
            _position += 2;
            return value;
        }

        // TODO Rename to ReadString()
        public string GetString()
        {
            int stringLength = GetShort();

            if (stringLength == 0 || _position + stringLength > _body.Length)
            {
                // TODO Print warning
                return string.Empty;
            }

            string value = Encoding.UTF8.GetString(_body, _position, stringLength);

            _position += stringLength;

            return value;
        }

        public uint GetUInt32()
        {
            return (uint) GetInteger();
        }

        public void Setup(byte[] body)
        {
            _body = body;
            Id = GetShort();
        }

        public override string ToString()
        {
            return String.Join(",", _body);
        }

        protected override void OnResetState()
        {
            Id = 0;
            _body = null;
        }

        private byte[] ReadBytes(int len)
        {
            byte[] arrayBytes = new byte[len];

            for (int i = 0; i < len; i++)
                arrayBytes[i] = _body[_position++];

            return arrayBytes;
        }

        #endregion Methods
    }
}