using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Support
{
	public class ModerationRoomToolMessageComposer : AbstractComposer<RoomData, bool>
	{
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISender session, RoomData data, bool isLoaded)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(data.Id);
				message.AppendInteger(data.UsersNow);

				message.AppendBool(false); // TODO Meaning? (isOwnerInRoom?)

				message.AppendInteger(data.Owner.Id);
				message.AppendString(data.Owner.UserName);
				message.AppendBool(isLoaded);
				message.AppendString(data.Name);
				message.AppendString(data.Description);
				message.AppendInteger(data.Tags.Count);

				foreach (string current in data.Tags)
					message.AppendString(current);

				message.AppendBool(false);

				session.Send (message);
			}
		}
	}
}

