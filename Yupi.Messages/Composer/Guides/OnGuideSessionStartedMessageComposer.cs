using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Guides
{
	public class OnGuideSessionStartedMessageComposer : Yupi.Messages.Contracts.OnGuideSessionStartedMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, UserInfo habbo)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				message.AppendString(habbo.Look);
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				message.AppendString(habbo.Look);
				session.Send (message);
			}
		}
	}
}

