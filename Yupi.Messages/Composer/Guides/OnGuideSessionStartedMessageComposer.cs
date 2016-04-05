using System;
using Yupi.Emulator.Game.Users;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.Guides
{
	public class OnGuideSessionStartedMessageComposer : AbstractComposer
	{
		public override void Compose (GameClient session, Habbo habbo, GameClient requester)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				message.AppendString(habbo.Look);
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				message.AppendString(habbo.Look);
				session.Send (message);
				requester.Send (message);
			}
		}
	}
}

