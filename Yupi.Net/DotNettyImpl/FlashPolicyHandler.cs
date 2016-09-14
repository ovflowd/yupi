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