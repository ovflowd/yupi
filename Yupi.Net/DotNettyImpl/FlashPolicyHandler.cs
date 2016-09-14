// ---------------------------------------------------------------------------------
// <copyright file="FlashPolicyHandler.cs" company="https://github.com/sant0ro/Yupi">
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

    public class FlashPolicyHandler : ByteToMessageDecoder
    {
        #region Fields

        private CrossDomainSettings FlashPolicy;

        #endregion Fields

        #region Constructors

        public FlashPolicyHandler(CrossDomainSettings flashPolicy)
        {
            this.FlashPolicy = flashPolicy;
        }

        #endregion Constructors

        #region Methods

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 2)
            {
                return;
            }

            input.MarkReaderIndex();

            byte magic1 = input.ReadByte();
            byte magic2 = input.ReadByte();

            bool isFlashPolicyRequest = (magic1 == '<' && magic2 == 'p');

            if (isFlashPolicyRequest)
            {
                input.SkipBytes(input.ReadableBytes);

                // Make sure no downstream handler can interfere with sending our policy response
                removeAllPipelineHandlers(context.Channel.Pipeline);

                // Write the policy and close the connection
                context.WriteAndFlushAsync(FlashPolicy.GetBytes()).ContinueWith(delegate { context.CloseAsync(); });
            }
            else
            {
                // Remove ourselves
                context.Channel.Pipeline.Remove(this);
            }
        }

        private void removeAllPipelineHandlers(IChannelPipeline pipeline)
        {
            while (pipeline.First() != null)
            {
                pipeline.RemoveFirst();
            }
        }

        #endregion Methods
    }
}