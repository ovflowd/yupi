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
using Yupi.Emulator.Messages.Encoding;
using Yupi.Emulator.Messages.Factorys;

namespace Yupi.Emulator.Messages.Buffers
{
    /// <summary>
    ///     Class SimpleClientMessageBuffer.
    /// </summary>
    public class SimpleClientMessageBuffer : IDisposable
    {
        /// <summary>
        ///     The _body
        /// </summary>
        private byte[] _body;

        /// <summary>
        ///     The length
        /// </summary>
        internal int _length;

        /// <summary>
        ///     The _position
        /// </summary>
        private int _position;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimpleClientMessageBuffer" /> class.
        /// </summary>
        internal SimpleClientMessageBuffer(int messageId, byte[] body, int position, int packetLength)
        {
            Init(messageId, body, position, packetLength);
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        internal int Id { get; private set; }

        public int Length => _length;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ClientMessageFactory.ObjectCallback(this);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            string stringValue = string.Empty;

            stringValue += System.Text.Encoding.Default.GetString(_body);

            for (int i = 0; i < 13; i++)
                stringValue = stringValue.Replace(char.ToString((char) i), $"[{i}]");

            return stringValue;
        }

        /// <summary>
        ///     Initializes the specified message identifier.
        /// </summary>
        internal void Init(int messageId, byte[] body, int position, int packetLength)
        {
            Id = messageId;
            _body = body;
            _position = position;
            _length = packetLength;
        }

        /// <summary>
        ///     Reads the bytes.
        /// </summary>
        /// <param name="len">The bytes length.</param>
        /// <returns>System.Byte[].</returns>
        internal byte[] ReadBytes(int len)
        {
            byte[] arrayBytes = new byte[len];

            for (int i = 0; i < len; i++)
                arrayBytes[i] = _body[_position++];

            return arrayBytes;
        }

        /// <summary>
        ///     Gets the bytes.
        /// </summary>
        /// <param name="len">The bytes length.</param>
        /// <returns>System.Byte[].</returns>
        internal byte[] GetBytes(int len)
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

        /// <summary>
        ///     Gets the string.
        /// </summary>
        /// <returns>System.String.</returns>
        internal string GetString() => GetString(System.Text.Encoding.UTF8);

        /// <summary>
        ///     Gets the string.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns>System.String.</returns>
        internal string GetString(System.Text.Encoding encoding)
        {
            int stringLength = GetInteger16();

            if (stringLength == 0 || _position + stringLength > _body.Length)
                return string.Empty;

            string value = encoding.GetString(_body, _position, stringLength);

            _position += stringLength;

            return value;
        }

        /// <summary>
        ///     Gets the integer from string.
        /// </summary>
        /// <returns>System.Int32.</returns>
        internal int GetIntegerFromString()
        {
            int result;

            string stringValue = GetString(System.Text.Encoding.ASCII);

            int.TryParse(stringValue, out result);

            return result;
        }

        /// <summary>
        ///     Gets the bool.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool GetBool() => _body[_position++] == 1;

        /// <summary>
        ///     Gets the integer16.
        /// </summary>
        /// <returns>System.Int16.</returns>
        internal short GetInteger16() => HabboEncoding.DecodeInt16(_body, ref _position);

        /// <summary>
        ///     Gets the integer.
        /// </summary>
        /// <returns>System.Int32.</returns>
        internal int GetInteger() => HabboEncoding.DecodeInt32(_body, ref _position);

        internal bool GetIntegerAsBool() => HabboEncoding.DecodeInt32(_body, ref _position) == 1;

        /// <summary>
        ///     Gets the integer32.
        /// </summary>
        /// <returns>System.UInt32.</returns>
        internal uint GetUInteger()
        {
            int value = GetInteger();

            return value < 0 ? 0 : (uint) value;
        }

        /// <summary>
        ///     Gets the integer16.
        /// </summary>
        /// <returns>System.UInt16.</returns>
        internal ushort GetUInteger16() => (ushort) GetInteger16();
    }
}