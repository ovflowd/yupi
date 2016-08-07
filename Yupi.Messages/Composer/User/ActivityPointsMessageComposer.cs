using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class ActivityPointsMessageComposer : Yupi.Messages.Contracts.ActivityPointsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint duckets, uint diamonds)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(2);
				message.AppendInteger(0);
				message.AppendInteger(duckets);
				message.AppendInteger(5);
				message.AppendInteger(diamonds);
				session.Send (message);
			}
		}
	}
}

