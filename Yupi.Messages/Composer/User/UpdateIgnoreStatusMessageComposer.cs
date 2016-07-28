using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class UpdateIgnoreStatusMessageComposer : AbstractComposer<UpdateIgnoreStatusMessageComposer.State, string>
	{
		public enum State {
			IGNORE = 1,
			LISTEN = 3 // TODO Any other valid values?
		}

		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, State state, string username)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (state);
				message.AppendString (username);
				session.Send (message);
			}
		}
	}
}

