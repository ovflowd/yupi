using System;

namespace Yupi.Messages
{
    /// <summary>
    ///     Class HabboEncoding.
    /// </summary>
    internal class HabboEncoding
    {
        private const short ShortLength = sizeof (short);
        private const int IntLength = sizeof (int);

        /// <summary>
        ///     Decodes the int32.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="position"></param>
        /// <returns>System.Int32.</returns>
        internal static int DecodeInt32(byte[] v, ref int position)
        {
            if (position + IntLength > v.Length || position < 0)
                return 0;

            return (v[position++] << 24) + (v[position++] << 16) + (v[position++] << 8) + v[position++];
        }

        /// <summary>
        ///     Decodes the int16.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="position"></param>
        /// <returns>Int16.</returns>
        internal static short DecodeInt16(byte[] v, ref int position)
        {
            if (position + ShortLength > v.Length || position < 0)
                return 0;

            return (short) ((v[position++] << 8) + v[position++]);
        }

        /// <summary>
        ///     Gets the character filter.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        public static string GetCharFilter(string data)
        {
            for (int i = 0; i <= 13; i++)
                data = data.Replace(Convert.ToChar(i) + "", "[" + i + "]");

            return data;
        }
    }
}