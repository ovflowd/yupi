using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Catalog
{
	public class GiftWrappingConfigurationMessageComposer : Yupi.Messages.Contracts.GiftWrappingConfigurationMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				throw new NotImplementedException ();
			}
		}
	}
}

