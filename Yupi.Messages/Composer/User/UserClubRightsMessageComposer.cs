using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class UserClubRightsMessageComposer : AbstractComposer<bool, int>
	{
		public override void Compose (Yupi.Protocol.ISender session, bool hasVIP, int rank)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(hasVIP);
				message.AppendInteger(rank);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

