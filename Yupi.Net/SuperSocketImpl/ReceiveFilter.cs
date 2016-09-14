namespace Yupi.Net.SuperSocketImpl
{
    using System;
    using System.Text;

    using SuperSocket.Common;
    using SuperSocket.Facility.Protocol;
    using SuperSocket.SocketBase.Protocol;

    public class ReceiveFilter : FixedHeaderReceiveFilter<RequestInfo>
    {
        #region Constructors

        public ReceiveFilter()
            : base(4)
        {
        }

        #endregion Constructors

        #region Methods

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return BinaryHelper.ToInt(header, offset);
        }

        protected override RequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset,
            int length)
        {
            return new RequestInfo(bodyBuffer.CloneRange(offset, length));
        }

        #endregion Methods
    }
}