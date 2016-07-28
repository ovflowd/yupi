using System;

using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.User
{
	// TODO Shouldn't this be called NameCHECKED

	public class NameChangedUpdatesMessageComposer : AbstractComposer
	{
		public enum Status {
			OK = 0,
			// TODO What is 1 ?
			TOO_SHORT = 2,
			TOO_LONG = 3,
			INVALID_CHARS = 4,
			IS_TAKEN = 5
		}

		public void Compose( Yupi.Protocol.ISender session, Status status, string newName, List<string> alternatives = null) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (status);
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

