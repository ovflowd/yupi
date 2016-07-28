using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Guides
{
	public class OnGuideSessionStartedMessageComposer : AbstractComposer<Habbo, Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Habbo habbo, Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> requester)
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

