using System;
using SuperSocket.SocketBase.Protocol;

namespace Yupi.Net.SuperSocketImpl
{
    public class FlashReceiveFilter : IReceiveFilter<RequestInfo>
    {
        public int LeftBufferSize { get; private set; }

        public IReceiveFilter<RequestInfo> NextReceiveFilter { get; private set; }

        public FilterState State
        {
            get { return FilterState.Normal; }
        }

        public FlashReceiveFilter()
        {
            this.NextReceiveFilter = new ReceiveFilter();
        }

        public RequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            // did not read anything
            rest = length;

            if (length < 2)
            {
                return null;
            }

            if (readBuffer[offset] == '<' && readBuffer[offset + 1] == 'p')
            {
                return new RequestInfo(null, true);
            }
            else
            {
                return null;
            }
        }

        public void Reset()
        {
            // Do nothing
        }
    }
}