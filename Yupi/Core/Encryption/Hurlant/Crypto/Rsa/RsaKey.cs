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
using System.Globalization;
using System.Numerics;
using Yupi.Core.Encryption.Utils;

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

            CanEncrypt = N != 0 && E != 0;
            CanDecrypt = CanEncrypt && D != 0;
        }

        public int E { get; }

        public BigInteger N { get; }

        public BigInteger D { get; }

        public BigInteger P { get; }

        public BigInteger Q { get; }

        public BigInteger Dmp1 { get; private set; }

        public BigInteger Dmq1 { get; private set; }

        public BigInteger Coeff { get; private set; }

        public static RsaKey ParsePrivateKey(string n, string e, string d, string p = null, string q = null,
            string dmp1 = null, string dmq1 = null, string coeff = null)
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

        public int GetBlockSize() => N.ToByteArray().Length - 1;

        public byte[] Encrypt(byte[] src) => DoEncrypt(DoPublic, src, Pkcs1PadType.FullByte);

        public byte[] Decrypt(byte[] src) => DoDecrypt(DoPublic, src, Pkcs1PadType.FullByte);

        public byte[] Sign(byte[] src) => DoEncrypt(DoPrivate, src, Pkcs1PadType.FullByte);

        public byte[] Verify(byte[] src) => DoDecrypt(DoPrivate, src, Pkcs1PadType.FullByte);

        protected BigInteger DoPublic(BigInteger m) => BigInteger.ModPow(m, E, N);

        protected BigInteger DoPrivate(BigInteger m) => P != 0 || Q != 0 ? BigInteger.ModPow(m, D, N) : 0;

        private byte[] DoEncrypt(DoCalculateionDelegate method, byte[] src, Pkcs1PadType type)
        {
            try
            {
                int bl = GetBlockSize();

                byte[] paddedBytes = Pkcs1Pad(src, bl, type);

                Array.Reverse(paddedBytes);

                BigInteger m = new BigInteger(paddedBytes);

                if (m == 0)
                    return null;

                BigInteger c = method(m);
                byte[] data = c.ToByteArray();

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
                BigInteger c = new BigInteger(src);

                BigInteger m = method(c);

                if (m == 0)
                    return null;

                int bl = GetBlockSize();
                byte[] data = m.ToByteArray();

                Array.Reverse(data);

                byte[] bytes = Pkcs1Unpad(data, bl, type);

                return bytes;
            }
            catch
            {
                return null;
            }
        }

        private static byte[] Pkcs1Pad(IList<byte> src, int n, Pkcs1PadType type)
        {
            byte[] bytes = new byte[n];

            int i = src.Count - 1;
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

            bytes[--n] = (byte) type;
            bytes[--n] = 0;

            return bytes;
        }

        private static byte[] Pkcs1Unpad(byte[] src, int n, Pkcs1PadType type)
        {
            int i = 0;
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

            byte[] bytes = new byte[src.Length - i - 1];
            for (int p = 0; ++i < src.Length; p++)
                bytes[p] = src[i];

            return bytes;
        }
    }
}