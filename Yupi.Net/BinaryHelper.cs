// ---------------------------------------------------------------------------------
// <copyright file="BinaryHelper.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Net
{
    using System;

    public class BinaryHelper
    {
        #region Methods

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

        #endregion Methods
    }
}