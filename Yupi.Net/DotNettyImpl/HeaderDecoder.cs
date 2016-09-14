// ---------------------------------------------------------------------------------
// <copyright file="HeaderDecoder.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Net.DotNettyImpl
{
    using System;
    using System.Collections.Generic;

    using DotNetty.Buffers;
    using DotNetty.Codecs;
    using DotNetty.Transport.Channels;

    public class HeaderDecoder : ByteToMessageDecoder
    {
        #region Methods

        /// <summary>
        /// Decode the header of a message if available
        /// </summary>
        /// <param name="context">The socket context</param>
        /// <param name="input">The input buffer.</param>
        /// <param name="output">List containing decoded messages (if any)</param>
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 4)
            {
                return;
            }

            // Mark is used by ResetReaderIndex
            input.MarkReaderIndex();

            // It is possible to call ReadInt here since it decodes the int as big-endian (network order)
            int length = input.ReadInt();

            if (input.ReadableBytes < length)
            {
                input.ResetReaderIndex();
                return;
            }

            output.Add(input.ReadBytes(length));
        }

        #endregion Methods
    }
}