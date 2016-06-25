using System;
using Yupi.Emulator.Game.Users;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.User
{
	public class UpdateUserDataMessageComposer : AbstractComposer
	{ // TODO Does -1 mean self???
		public override void Compose (Yupi.Protocol.ISender room, Habbo habbo, int roomUserId = -1)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(roomUserId);
				message.AppendString(habbo.Look);
				message.AppendString(habbo.Gender.ToLower()); // TODO ToLower here whereas ToUpper in UpdateAvatarAspectMessageComposer ?!
				message.AppendString(habbo.Motto);
				message.AppendInteger(habbo.AchievementPoints);

				room.Send (message);
			}
		}
	}
}

