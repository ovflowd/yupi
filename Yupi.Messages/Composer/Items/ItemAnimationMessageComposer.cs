using System;
using System.Drawing;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Items
{
	public class ItemAnimationMessageComposer : AbstractComposer
	{
		public enum Type {
			USER = 0,
			ITEM = 1
		}
		
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISender session, Tuple<Point, double> pos, Tuple<Point, double> nextPos, uint rollerId, uint affectedId, ItemAnimationMessageComposer.Type type)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(pos.Item1.X);
				message.AppendInteger(pos.Item1.Y);
				message.AppendInteger(nextPos.Item1.X);
				message.AppendInteger(nextPos.Item1.Y);

				message.AppendInteger(type);

				switch (type) {
				case Type.ITEM:
					message.AppendInteger(affectedId);
					break;
				case Type.USER:
					message.AppendInteger(rollerId);
					message.AppendInteger(2);
					message.AppendInteger(affectedId);
					break;
				}

				message.AppendString(ServerUserChatTextHandler.GetString(pos.Item2));
				message.AppendString(ServerUserChatTextHandler.GetString(nextPos.Item2));
				message.AppendInteger(rollerId);
				session.Send (message);
			}
		}
	}
}

