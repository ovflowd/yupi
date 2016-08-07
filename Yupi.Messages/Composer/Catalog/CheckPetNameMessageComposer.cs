using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
	public class CheckPetNameMessageComposer : Yupi.Messages.Contracts.CheckPetNameMessageComposer
	{
		public override void Compose( Yupi.Protocol.ISender session, int status, string name) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(status);
				message.AppendString(name);
				session.Send (message);
			}
		}
	}
}

