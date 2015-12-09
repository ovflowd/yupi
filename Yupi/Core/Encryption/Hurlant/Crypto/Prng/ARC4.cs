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
                _bytes[_i] = (byte)_i;

            for (_i = 0; _i < PoolSize; ++_i)
            {
                _j = (_j + _bytes[_i] + key[_i % key.Length]) & (PoolSize - 1);
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
            for (var k = 0; k < src.Length; k++)
                src[k] ^= Next();
        }

        private void Swap(int a, int b)
        {
            var t = _bytes[a];
            _bytes[a] = _bytes[b];
            _bytes[b] = t;
        }
    }
}