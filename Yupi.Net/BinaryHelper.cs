using System;

namespace Yupi.Net
{
    public class BinaryHelper
    {
        public static int ToInt(byte[] data, int offset = 0)
        {
            CheckRange(data, offset, 4);

            return (data[offset] << 24) | (data[offset + 1] << 16) | (data[offset + 2] << 8) | (data[offset + 3]);
        }

        public static short ToShort(byte[] data, int offset = 0)
        {
            CheckRange(data, offset, 2);

            return (short) ((data[offset] << 8) | (data[offset + 1]));
        }

        private static void CheckRange(byte[] data, int offset, int count)
        {
            if (offset + count > data.Length || offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "Index was"
                                                                +
                                                                " out of range. Must be non-negative and less than the"
                                                                + " size of the collection.");
            }
        }

        public static byte[] GetBytes(int value)
        {
            byte[] data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return data;
        }

        public static byte[] GetBytes(short value)
        {
            byte[] data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return data;
        }
    }
}