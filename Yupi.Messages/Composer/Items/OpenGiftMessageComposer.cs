using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class OpenGiftMessageComposer : AbstractComposer<BaseItem, string>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, BaseItem item, string text)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(item.Type.ToString());
				message.AppendInteger(item.SpriteId);
				message.AppendString(item.Name);
				message.AppendInteger(item.Id);
				message.AppendString(item.Type.ToString());
				message.AppendBool(true);
				message.AppendString(text);
				session.Send (message);
			}
		}
	}
}

