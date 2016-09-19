using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yupi.Crypto.Cryptography
{
    public class ARC4
    {
        private int i;
        private int j;
        private byte[] bytes;

        public const int POOLSIZE = byte.MaxValue + 1;

        public ARC4()
        {
            bytes = new byte[POOLSIZE];
        }

        public ARC4(byte[] key)
        {
            bytes = new byte[POOLSIZE];
            this.Initialize(key);
        }

        public void Initialize(byte[] key)
        {
            this.i = 0;
            this.j = 0;

            for (i = 0; i < POOLSIZE; ++i)
            {
                this.bytes[i] = (byte)i;
            }

            for (i = 0; i < POOLSIZE; ++i)
            {
                j = (j + bytes[i] + key[i % key.Length]) & (POOLSIZE - 1);
                this.Swap(i, j);
            }

            this.i = 0;
            this.j = 0;
        }

        private void Swap(int a, int b)
        {
            byte t = this.bytes[a];
            this.bytes[a] = this.bytes[b];
            this.bytes[b] = t;
        }

        public byte Next()
        {
            this.i = ++this.i & (POOLSIZE - 1);
            this.j = (this.j + this.bytes[i]) & (POOLSIZE - 1);
            this.Swap(i, j);
            return this.bytes[(this.bytes[i] + this.bytes[j]) & 255];
        }

        public void Parse(ref byte[] src)
        {
            for (int k = 0; k < src.Length; k++)
            {
                src[k] ^= this.Next();
            }
        }
    }
}
