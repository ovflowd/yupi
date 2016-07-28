using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
	public class OnGuideSessionDetachedMessageComposer : AbstractComposer<int>
	{
		// TODO Meaning of value (enum)
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int value)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(value); 
				session.Send (message);
			}
		}
	}
}

