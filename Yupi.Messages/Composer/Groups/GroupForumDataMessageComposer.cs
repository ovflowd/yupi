using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Util;

namespace Yupi.Messages.Groups
{
	public class GroupForumDataMessageComposer : Yupi.Messages.Contracts.GroupForumDataMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, Group group, UserInfo user)
		{ // TODO Refactor
			string string1 = string.Empty, string2 = string.Empty, string3 = string.Empty, string4 = string.Empty;

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(group.Id);
				message.AppendString(group.Name);
				message.AppendString(group.Description);
				message.AppendString(group.Badge);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(group.Forum.GetMessageCount());
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(group.Forum.GetLastPost().Poster.Id);
				message.AppendString(group.Forum.GetLastPost().Poster.UserName);
				message.AppendInteger((int)group.Forum.GetLastPost().Timestamp.ToUnix().SecondsSinceEpoch);
				message.AppendInteger(group.Forum.WhoCanRead);
				message.AppendInteger(group.Forum.WhoCanPost);
				message.AppendInteger(group.Forum.WhoCanThread);
				message.AppendInteger(group.Forum.WhoCanMod);

				if (!group.Members.Contains (user)) {
					if (group.Forum.WhoCanRead == 1) {
						string1 = "not_member";
					}

					if (group.Forum.WhoCanPost == 1) {
						string2 = "not_member";
					}

					if (group.Forum.WhoCanThread == 1) {
						string3 = "not_member";
					}
				}

				if (!group.Admins.Contains (user)) {
					if (group.Forum.WhoCanRead == 2) {
						string1 = "not_admin";
					}

					if (group.Forum.WhoCanPost == 2) {
						string2 = "not_admin";
					}

					if (group.Forum.WhoCanThread == 2) {
						string3 = "not_admin";
					}

					if (group.Forum.WhoCanMod == 2) {
						string4 = "not_admin";
					}
				}

				if (user != group.Creator) {
					if (group.Forum.WhoCanRead == 3) {
						string1 = "not_owner";
					}

					if (group.Forum.WhoCanPost == 3) {
						string2 = "not_owner";
					}

					if (group.Forum.WhoCanThread == 3) {
						string3 = "not_owner";
					}

					if (group.Forum.WhoCanMod == 3) {
						string4 = "not_owner";
					}
				}

				message.AppendString(string1);
				message.AppendString(string2);
				message.AppendString(string3);
				message.AppendString(string4);
				message.AppendString(string.Empty);
				message.AppendBool(user == group.Creator);
				message.AppendBool(true);
				session.Send (message);
			}
		}
	}
}

