namespace Yupi.Net.SuperSocketImpl
{
    using System;

    using SuperSocket.SocketBase.Protocol;

    public class RequestInfo : BinaryRequestInfo
    {
        #region Properties

        public bool IsFlashRequest
        {
            get; private set;
        }

        #endregion Properties

        #region Constructors

        public RequestInfo(byte[] body, bool isFlashRequest = false)
            : base("__MESSAGE__", body)
        {
            IsFlashRequest = isFlashRequest;
        }

        #endregion Constructors
    }
}