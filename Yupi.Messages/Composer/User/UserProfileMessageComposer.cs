using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.User
{
	public class UserProfileMessageComposer : AbstractComposer<Habbo>
	{
		public override void Compose (Yupi.Protocol.ISender session, Habbo habbo)
		{
			// TODO Refactor unix time
			DateTime createTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(habbo.CreateDate);

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				message.AppendString(habbo.Look);
				message.AppendString(habbo.Motto);
				message.AppendString(createTime.ToString("dd/MM/yyyy"));
				message.AppendInteger(habbo.AchievementPoints);
				message.AppendInteger(GetFriendsCount(userId));
				message.AppendBool(habbo.Id != session.GetHabbo().Id &&
					session.GetHabbo().GetMessenger().FriendshipExists(habbo.Id));
				message.AppendBool(habbo.Id != session.GetHabbo().Id &&
					!session.GetHabbo().GetMessenger().FriendshipExists(habbo.Id) &&
					session.GetHabbo().GetMessenger().RequestExists(habbo.Id));
				message.AppendBool(Yupi.GetGame().GetClientManager().GetClientByUserId(habbo.Id) != null);
				HashSet<GroupMember> groups = Yupi.GetGame().GetGroupManager().GetUserGroups(habbo.Id);
				message.AppendInteger(groups.Count);

				// TODO Refactor

				foreach (Group group in groups.Select(groupUs => Yupi.GetGame().GetGroupManager().GetGroup(groupUs.GroupId))
				)
					if (group != null)
					{
						message.AppendInteger(group.Id);
						message.AppendString(group.Name);
						message.AppendString(group.Badge);
						message.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(group.Colour1, true));
						message.AppendString(Yupi.GetGame().GetGroupManager().GetGroupColour(group.Colour2, false));
						message.AppendBool(group.Id == habbo.FavouriteGroup);
						message.AppendInteger(-1);
						message.AppendBool(group.Forum.Id != 0);
					}
					else
					{
						message.AppendInteger(1);
						message.AppendString("THIS GROUP IS INVALID");
						message.AppendString("");
						message.AppendString("");
						message.AppendString("");
						message.AppendBool(false);
						message.AppendInteger(-1);
						message.AppendBool(false);
					}
				if (habbo.PreviousOnline == 0)
					message.AppendInteger(-1);
				else if (Yupi.GetGame().GetClientManager().GetClientByUserId(habbo.Id) == null)
					message.AppendInteger(Yupi.GetUnixTimeStamp() - habbo.PreviousOnline);
				else
					message.AppendInteger(Yupi.GetUnixTimeStamp() - habbo.LastOnline);

				message.AppendBool(true);

				session.Send (message);
			}
		}
	}
}

