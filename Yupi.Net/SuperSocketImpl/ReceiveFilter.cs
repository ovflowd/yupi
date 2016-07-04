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
			return BinaryHelper.ToInt(header, offset);
		}

		protected override RequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
		{
			return new RequestInfo(bodyBuffer.CloneRange(offset, length));
		}
	}
}
