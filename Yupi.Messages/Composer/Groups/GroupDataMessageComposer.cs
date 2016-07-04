using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Users;

namespace Yupi.Messages.Groups
{
	public class GroupDataMessageComposer : AbstractComposer<Group, Habbo, bool>
	{
		public override void Compose (Yupi.Protocol.ISender session, Group group, Habbo habbo, bool newWindow = false)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				// TODO Hide conversion between Unix <-> DateTime
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
				DateTime dateTime2 = dateTime.AddSeconds(group.CreateTime);

				message.Init(PacketLibraryManager.OutgoingHandler("GroupDataMessageComposer"));

				message.AppendInteger(group.Id);
				message.AppendBool(true);
				message.AppendInteger(group.State);
				message.AppendString(group.Name);
				message.AppendString(group.Description);
				message.AppendString(group.Badge);
				message.AppendInteger(group.RoomId);
				message.AppendString(Yupi.GetGame().GetRoomManager().GenerateRoomData(@group.RoomId) == null
					? "No room found.."
					: Yupi.GetGame().GetRoomManager().GenerateRoomData(@group.RoomId).Name);
				message.AppendInteger(@group.CreatorId == session.GetHabbo().Id
					? 3
					: (group.Requests.ContainsKey(session.GetHabbo().Id)
						? 2
						: (group.Members.ContainsKey(session.GetHabbo().Id) ? 1 : 0)));
				message.AppendInteger(group.Members.Count);
				message.AppendBool(session.GetHabbo().FavouriteGroup == group.Id);
				message.AppendString($"{dateTime2.Day.ToString("00")}-{dateTime2.Month.ToString("00")}-{dateTime2.Year}");
				message.AppendBool(group.CreatorId == session.GetHabbo().Id);
				message.AppendBool(group.Admins.ContainsKey(session.GetHabbo().Id));
				message.AppendString(Yupi.GetHabboById(@group.CreatorId) == null
					? string.Empty
					: Yupi.GetHabboById(group.CreatorId).UserName);
				message.AppendBool(newWindow);
				message.AppendBool(group.AdminOnlyDeco == 0u);
				message.AppendInteger(group.Requests.Count);
				message.AppendBool(group.Forum.Id != 0);
				session.Send (message);
			}
		}
	}
}

