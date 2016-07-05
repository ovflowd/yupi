using System;
using Yupi.Protocol.Buffers;

using Yupi.Net;

namespace Yupi.Messages.User
{
	public class CitizenshipStatusMessageComposer : AbstractComposer<string>
	{
		// TODO Replace value with a proper name
		public override void Compose (Yupi.Protocol.ISender session, string value)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (value);
				message.AppendInteger(4);
				message.AppendInteger(4); // TODO magic constant
				session.Send (message);
			}
		}
	}
}

