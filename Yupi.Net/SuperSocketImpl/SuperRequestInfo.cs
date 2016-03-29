using System;
using SuperSocket.SocketBase.Protocol;

namespace Yupi.Net.SuperSocketImpl
{
	public class SuperRequestInfo : BinaryRequestInfo
	{
		public short Id { get; private set; }

		public SuperRequestInfo(short id, byte[] body) : base(id.ToString(), body) {
			Id = id;
		}
	}
}

