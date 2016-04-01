using System;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System.Text;
using SuperSocket.Common;

namespace Yupi.Net.SuperSocketImpl
{
	public class ReceiveFilter : FixedHeaderReceiveFilter<RequestInfo>
	{
		public ReceiveFilter () : base(4)
		{
		}

		protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
		{
			int bodyLength = BinaryHelper.ToInt32(header, offset);
			return bodyLength;
		}

		protected override RequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
		{
			// TODO Handle edge cases
			return new RequestInfo(bodyBuffer.CloneRange(offset, length));
		}
	}
}

