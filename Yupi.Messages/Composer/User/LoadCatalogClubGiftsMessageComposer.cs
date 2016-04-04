using System;

namespace Yupi.Messages.User
{
	public class LoadCatalogClubGiftsMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Net.ISession session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(0); // i
				message.AppendInteger(0); // i2
				message.AppendInteger(1); // TODO Magic constants
				session.Send (message);
			}
		}
	}
}

