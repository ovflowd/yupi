using System;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System.Text;
using SuperSocket.Common;

namespace Yupi.Net.SuperSocketImpl
{
	public class SuperReceiveFilter : FixedHeaderReceiveFilter<SuperRequestInfo>
	{
		public SuperReceiveFilter () : base(6)
		{
		}

		protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
		{
			if (header [offset] == '<') {
				return 1;
			}

			int bodyLength = BinaryHelper.ToInt32(header, offset);
			return bodyLength - 2; // Remove ID
		}

		protected override SuperRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
		{
			short id;

			if (header.Array [header.Offset] == '<') {
				id = 0; // TODO Hardcoded number
			} else {
				id = BinaryHelper.ToShort(header.Array, header.Offset + 4);
			}

			// TODO Handle edge cases
			return new SuperRequestInfo(id, bodyBuffer.CloneRange(offset, length));
		}
	}
}

