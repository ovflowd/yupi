#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Yupi.Core.Encryption.Utils;

#endregion

namespace Yupi.Core.Encryption.Hurlant.Crypto.Rsa
{
    public class RsaKey
    {
        protected bool CanDecrypt;
        protected bool CanEncrypt;

        public RsaKey(BigInteger n, int e,
            BigInteger d,
            BigInteger p, BigInteger q,
            BigInteger dmp1, BigInteger dmq1,
            BigInteger coeff)
        {
            E = e;
            N = n;
            D = d;
            P = p;
            Q = q;
            Dmp1 = dmp1;
            Dmq1 = dmq1;
            Coeff = coeff;

            CanEncrypt = (N != 0 && E != 0);
            CanDecrypt = (CanEncrypt && D != 0);
        }

        public int E { get; private set; }

        public BigInteger N { get; private set; }

        public BigInteger D { get; private set; }

        public BigInteger P { get; private set; }

        public BigInteger Q { get; private set; }

        public BigInteger Dmp1 { get; private set; }

        public BigInteger Dmq1 { get; private set; }

        public BigInteger Coeff { get; private set; }

        public static RsaKey ParsePrivateKey(string n, string e,
            string d,
            string p = null, string q = null,
            string dmp1 = null, string dmq1 = null,
            string coeff = null)
        {
            if (p == null)
                return new RsaKey(
                    BigInteger.Parse(n, NumberStyles.HexNumber), Convert.ToInt32(e, 16),
                    BigInteger.Parse(d, NumberStyles.HexNumber),
                    0, 0,
                    0, 0,
                    0);
            return new RsaKey(
                BigInteger.Parse(n, NumberStyles.HexNumber), Convert.ToInt32(e, 16),
                BigInteger.Parse(d, NumberStyles.HexNumber),
                BigInteger.Parse(p, NumberStyles.HexNumber), BigInteger.Parse(q, NumberStyles.HexNumber),
                BigInteger.Parse(dmp1, NumberStyles.HexNumber), BigInteger.Parse(dmq1, NumberStyles.HexNumber),
                BigInteger.Parse(coeff, NumberStyles.HexNumber));
        }

        public int GetBlockSize()
        {
            return N.ToByteArray().Length - 1;
        }

        public byte[] Encrypt(byte[] src)
        {
            return DoEncrypt(DoPublic, src, Pkcs1PadType.FullByte);
        }

        public byte[] Decrypt(byte[] src)
        {
            return DoDecrypt(DoPublic, src, Pkcs1PadType.FullByte);
        }

        public byte[] Sign(byte[] src)
        {
            return DoEncrypt(DoPrivate, src, Pkcs1PadType.FullByte);
        }

        public byte[] Verify(byte[] src)
        {
            return DoDecrypt(DoPrivate, src, Pkcs1PadType.FullByte);
        }

        protected BigInteger DoPublic(BigInteger m)
        {
            return BigInteger.ModPow(m, E, N);
        }

        protected BigInteger DoPrivate(BigInteger m)
        {
            if (P != 0 || Q != 0)
                return 0;
            return BigInteger.ModPow(m, D, N);
        }

        private byte[] DoEncrypt(DoCalculateionDelegate method, byte[] src, Pkcs1PadType type)
        {
            try
            {
                var bl = GetBlockSize();

                var paddedBytes = Pkcs1Pad(src, bl, type);
                Array.Reverse(paddedBytes);
                var m = new BigInteger(paddedBytes);

                if (m == 0)
                    return null;

                var c = method(m);
                var data = c.ToByteArray();

                if (data[data.Length - 1] == 0)
                    Array.Resize(ref data, data.Length - 1);
                Array.Reverse(data);

                return c == 0 ? null : data;
            }
            catch
            {
                return null;
            }
        }

        private byte[] DoDecrypt(DoCalculateionDelegate method, byte[] src, Pkcs1PadType type)
        {
            try
            {
                var c = new BigInteger(src);

                var m = method(c);
                if (m == 0)
                    return null;

                var bl = GetBlockSize();
                var data = m.ToByteArray();
                Array.Reverse(data);

                var bytes = Pkcs1Unpad(data, bl, type);

                return bytes;
            }
            catch
            {
                return null;
            }
        }

        private static byte[] Pkcs1Pad(IList<byte> src, int n, Pkcs1PadType type)
        {
            var bytes = new byte[n];

            var i = src.Count - 1;
            while (i >= 0 && n > 11)
                bytes[--n] = src[i--];

            bytes[--n] = 0;
            while (n > 2)
            {
                byte x = 0;
                switch (type)
                {
                    case Pkcs1PadType.FullByte:
                        x = 0xFF;
                        break;

                    case Pkcs1PadType.RandomByte:
                        x = Randomizer.NextByte(1, 255);
                        break;
                }
                bytes[--n] = x;
            }

            bytes[--n] = (byte)type;
            bytes[--n] = 0;

            return bytes;
        }

        private static byte[] Pkcs1Unpad(byte[] src, int n, Pkcs1PadType type)
        {
            var i = 0;
            while (i < src.Length && src[i] == 0)
                ++i;

            if (src.Length - i != n - 1 || src[i] > 2)
            {
                Console.WriteLine("PKCS#1 unpad: i={0}, expected src[i]==[0,1,2], got src[i]={1}", i,
                    src[i].ToString("X"));
                return null;
            }

            ++i;

            while (src[i] != 0)
                if (++i >= src.Length)
                    Console.WriteLine("PKCS#1 unpad: i={0}, src[i-1]!=0 (={1})", i, src[i - 1].ToString("X"));

            var bytes = new byte[src.Length - i - 1];
            for (var p = 0; ++i < src.Length; p++)
                bytes[p] = src[i];

            return bytes;
        }
    }
}