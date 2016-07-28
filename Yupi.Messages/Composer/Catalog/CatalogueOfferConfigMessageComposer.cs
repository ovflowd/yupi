using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
	public class CatalogueOfferConfigMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				// TODO Hardcoded message
				message.AppendInteger(100);
				message.AppendInteger(6);
				message.AppendInteger(2);
				message.AppendInteger(1);
				message.AppendInteger(2);
				message.AppendInteger(40);
				message.AppendInteger(99);
				session.Send (message);
			}
		}
	}
}

