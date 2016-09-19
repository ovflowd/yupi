using System;
using System.Numerics;
using System.Security.Cryptography;
using Yupi.Crypto.Utils;

namespace Yupi.Crypto.Cryptography
{
    public class RSACrypto
    {
        private RSACParameters _parameters;

        public RSACParameters Parameters
        {
            get
            {
                return this._parameters;
            }
        }

        public RSACrypto(RSACParameters parameters)
        {
            this._parameters = parameters;
        }

        public static RSACrypto CreateInstance(int bitLength, BigInteger exponent) // DOES NOT WORK
        {
            RandomNumberGenerator random = Randomizer.GetRandom();

            int qs = bitLength >> 1;

            BigInteger d = 0;
            BigInteger dp = 0;
            BigInteger dq = 0;
            BigInteger inverseQ = 0;
            BigInteger modules = 0;
            BigInteger p = 0;
            BigInteger q = 0;

            while (true) // bad, but it works.
            {
                do
                {
                    p = BigIntegerPrime.GeneratePseudoPrime(bitLength - qs, 10, random);
                }
                while (BigInteger.GreatestCommonDivisor(p - 1, exponent).CompareTo(1) != 0);

                do
                {
                    q = BigIntegerPrime.GeneratePseudoPrime(qs, 10, random);
                }
                while (BigInteger.GreatestCommonDivisor(q - 1, exponent).CompareTo(1) != 0);

                if (p.CompareTo(q) <= 0)
                {
                    BigInteger temp = p;
                    p = q;
                    q = temp;
                }

                BigInteger p1 = p - 1;
                BigInteger q1 = q - 1;
                BigInteger phi = p1 * q1;
                if (BigInteger.GreatestCommonDivisor(phi, exponent).CompareTo(1) == 0)
                {
                    modules = p * q;
                    d = exponent.ModInverse(phi);
                    //d = BigInteger.ModPow(ee, phi - 2, phi); // ERRORRRRRR
                    dp = d % p1;
                    dq = d % q1;
                    inverseQ = q.ModInverse(p);
                    //inverseQ = BigInteger.ModPow(q, p - 2, p);
                    break;
                }
            }

            RSACParameters parameters = new RSACParameters(d, dp, dq, exponent, inverseQ, modules, p, q);

            return new RSACrypto(parameters);
        }

        public byte[] Encrypt(byte[] src)
        {
            return this.Encrypt(src, false);
        }

        public byte[] Encrypt(byte[] src, bool usePrivate)
        {
            if (usePrivate)
            {
                return this.DoEncrypt(src, this.RSAPrivate);
            }

            return this.DoEncrypt(src, this.RSAPublic);
        }

        public byte[] Decrypt(byte[] src)
        {
            return this.Decrypt(src, false);
        }

        public byte[] Decrypt(byte[] src, bool usePrivate)
        {
            if (usePrivate)
            {
                return this.DoDecrypt(src, this.RSAPrivate);
            }

            return this.DoDecrypt(src, this.RSAPublic);
        }

        private byte[] PerformCalculation(byte[] src, RSACalculateDelegate method)
        {
            // Big integer requires little endian order!
            Array.Reverse(src);
            BigInteger data = new BigInteger(src);

            data = method(data);

            return data.ToByteArray(false);
        }

        private byte[] DoEncrypt(byte[] src, RSACalculateDelegate method)
        {
            if (src.Length > this._parameters.ModulesBlockSize)
            {
                throw new ArgumentException("Src is to long to encrypt.");
            }

            byte[] data = this.pkcs1pad(src);

            return this.PerformCalculation(data, method);
        }

        private byte[] DoDecrypt(byte[] src, RSACalculateDelegate method)
        {
            byte[] data = this.PerformCalculation(src, method);

            return this.pkcs1unpad(data);
        }

        private BigInteger RSAPublic(BigInteger data)
        {
            return BigInteger.ModPow(data, this._parameters.Exponent, this._parameters.Modules);
        }

        private BigInteger RSAPrivate(BigInteger data)
        {
            return BigInteger.ModPow(data, this._parameters.D, this._parameters.Modules);
        }

        private byte[] pkcs1pad(byte[] src)
        {
            RandomNumberGenerator random = Randomizer.GetRandom();

            int n = this._parameters.ModulesBlockSize;
            byte[] dst = new byte[n];
            dst[0] = 0;
            dst[1] = 2;

            byte[] paddingBytes = new byte[n - src.Length - 3];
            random.GetNonZeroBytes(paddingBytes);
            Array.Copy(paddingBytes, 0, dst, 2, paddingBytes.Length);

            Array.Copy(src, 0, dst, n - src.Length, src.Length);

            return dst;
        }

        private byte[] pkcs1unpad(byte[] src)
        {
            if (src[0] == 2)
            {
                byte[] temp = new byte[src.Length + 1];
                Array.Copy(src, 0, temp, 1, src.Length);
                src = temp;
            }

            if (src[0] == 0 && src[1] == 2)
            {
                int startIndex = 2;
                do
                {
                    if (src.Length < startIndex)
                    {
                        throw new CryptographicException("PKCS v1.5 Decode Error");
                    }
                }
                while (src[startIndex++] != 0);

                byte[] dst = new byte[src.Length - startIndex];
                Array.Copy(src, startIndex, dst, 0, dst.Length);

                return dst;
            }
            else
            {
                throw new CryptographicException("PKCS v1.5 Decode Error");
            }
        }
    }
}
