using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.User
{
	public class ActivityPointsMessageComposer : Yupi.Messages.Contracts.ActivityPointsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, UserWallet wallet)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(2);
				message.AppendInteger(0);
				message.AppendInteger(wallet.Duckets);
				message.AppendInteger(5);
				message.AppendInteger(wallet.Diamonds);
				session.Send (message);
			}
		}
	}
}

