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
using System.Text;
using Yupi.Net;
using CodeProject.ObjectPool;

namespace Yupi.Protocol.Buffers
{
    /// <summary>
    ///     Class SimpleClientMessageBuffer.
    /// </summary>
    public class ClientMessage : PooledObject
    {
        /// <summary>
        ///     The _body
        /// </summary>
        private byte[] _body;

        /// <summary>
        ///     The _position
        /// </summary>
        private int _position;

		public short Id { get; private set; }

		public void Setup(byte[] body)
        {
			_body = body;
			Id = GetShort ();
        }
			
        protected override void OnResetState ()
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
			
		public string GetString()
        {
            int stringLength = GetShort();

			if (stringLength == 0 || _position + stringLength > _body.Length) {
				// TODO Print warning
				return string.Empty;
			}

			string value = Encoding.UTF8.GetString(_body, _position, stringLength);

            _position += stringLength;

            return value;
        }
			
		public bool GetBool() {
			return _body[_position++] == 1;
		}
			
		public short GetShort() {
			short value = BinaryHelper.ToShort (_body, _position);
			_position += 2;
			return value;
		}
			
		public int GetInteger() {
			int value = BinaryHelper.ToInt (_body, _position);
			_position += 4;
			return value;
		}

		// TODO Probably inappropriate
		public bool GetIntegerAsBool() {
			return GetInteger () == 1;
		}
			
		public uint GetUInt32()
        {
			return (uint)GetInteger ();
        }
    }
}