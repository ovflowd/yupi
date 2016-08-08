using System;

using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.User
{
	// TODO Shouldn't this be called NameCHECKED

	public class NameChangedUpdatesMessageComposer : Yupi.Messages.Contracts.NameChangedUpdatesMessageComposer
	{
		public override void Compose( Yupi.Protocol.ISender session, Status status, string newName, IList<string> alternatives = null) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger ((int)status);
				message.AppendString (newName);

				if (alternatives == null) {
					message.AppendInteger (0); // TODO Magic constant
				} else {
					message.Append (alternatives);
				}
				session.Send(message);
			}
		}
	}
}

