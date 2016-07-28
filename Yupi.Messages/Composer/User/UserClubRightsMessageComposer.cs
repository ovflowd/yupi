using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class UserClubRightsMessageComposer : AbstractComposer<bool, int, bool>
	{
		public override void Compose ( Yupi.Protocol.ISender session, bool hasVIP, int rank, bool isAmbadassor = false)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(hasVIP);
				message.AppendInteger(rank);
				message.AppendBool(isAmbadassor);
				session.Send (message);
			}
		}
	}
}

