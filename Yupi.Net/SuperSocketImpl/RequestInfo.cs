using System;
using SuperSocket.SocketBase.Protocol;

namespace Yupi.Net.SuperSocketImpl
{
    public class RequestInfo : BinaryRequestInfo
    {
        public bool IsFlashRequest { get; private set; }

        public RequestInfo(byte[] body, bool isFlashRequest = false) : base("__MESSAGE__", body)
        {
            IsFlashRequest = isFlashRequest;
        }
    }
}