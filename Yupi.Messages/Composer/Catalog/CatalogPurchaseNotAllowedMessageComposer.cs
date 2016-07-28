using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
	public class CatalogPurchaseNotAllowedMessageComposer : AbstractComposer<bool>
	{
		public override void Compose ( Yupi.Protocol.ISender session, bool isForbidden)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (isForbidden);
				session.Send (message);
			}
		}
	}
}

