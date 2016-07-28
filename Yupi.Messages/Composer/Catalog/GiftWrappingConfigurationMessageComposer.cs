using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Catalog
{
	public class GiftWrappingConfigurationMessageComposer : AbstractComposerVoid
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			// TODO Hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(true);
				message.AppendInteger(1);
				message.AppendInteger(GiftWrapper.GiftWrappersList.Count);

				foreach (int i in GiftWrapper.GiftWrappersList)
					message.AppendInteger(i);

				message.AppendInteger(8);

				for (uint i = 0u; i != 8; i++)
					message.AppendInteger(i);

				message.AppendInteger(11);

				for (uint i = 0u; i != 11; i++)
					message.AppendInteger(i);

				message.AppendInteger(GiftWrapper.OldGiftWrappers.Count);

				foreach (int i in GiftWrapper.OldGiftWrappers)
					message.AppendInteger(i);
				
				session.Send (message);
			}
		}
	}
}

