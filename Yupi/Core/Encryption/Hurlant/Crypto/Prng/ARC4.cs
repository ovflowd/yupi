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

namespace Yupi.Core.Encryption.Hurlant.Crypto.Prng
{
    public class Arc4
    {
        public const int PoolSize = 256;

        private readonly byte[] _bytes;

        private int _i, _j;

        public Arc4()
        {
            _bytes = new byte[PoolSize];
        }

        public Arc4(byte[] key)
        {
            _bytes = new byte[PoolSize];

            Initialize(key);
        }

        public void Initialize(byte[] key)
        {
            _i = 0;
            _j = 0;

            for (_i = 0; _i < PoolSize; ++_i)
                _bytes[_i] = (byte) _i;

            for (_i = 0; _i < PoolSize; ++_i)
            {
                _j = (_j + _bytes[_i] + key[_i%key.Length]) & (PoolSize - 1);
                Swap(_i, _j);
            }

            _i = 0;
            _j = 0;
        }

        public byte Next()
        {
            _i = ++_i & (PoolSize - 1);
            _j = (_j + _bytes[_i]) & (PoolSize - 1);
            Swap(_i, _j);

            return _bytes[(_bytes[_i] + _bytes[_j]) & 255];
        }

        public void Parse(ref byte[] src)
        {
            for (int k = 0; k < src.Length; k++)
                src[k] ^= Next();
        }

        private void Swap(int a, int b)
        {
            byte t = _bytes[a];

            _bytes[a] = _bytes[b];
            _bytes[b] = t;
        }
    }
}