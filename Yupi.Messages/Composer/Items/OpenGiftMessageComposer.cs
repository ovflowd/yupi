using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class OpenGiftMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, Item item, string text)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(item.Type.ToString());
				message.AppendInteger(item.SpriteId);
				message.AppendString(item.Name);
				message.AppendInteger(item.ItemId);
				message.AppendString(item.Type.ToString());
				message.AppendBool(true);
				message.AppendString(text);
				session.Send (message);
			}
		}
	}
}

