using System;

using System.Collections.Generic;

using Yupi.Protocol.Buffers;

using System.Data;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
	public class GroupForumListingsMessageComposer : AbstractComposer
	{
		private const int TotalPerPage = 20;

		public void Compose(Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int selectType, int startIndex) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(selectType);

				List<Group> groupList = new List<Group>();

				// TODO Refactor
				switch (selectType)
				{
				case 0:
				case 1:
					using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
					{
						dbClient.SetQuery("SELECT count(id) FROM groups_forums_data WHERE forum_messages_count > 0");

						int qtdForums = dbClient.GetInteger();

						dbClient.SetQuery(
							"SELECT group_id FROM groups_forums_data WHERE forum_messages_count > 0 ORDER BY forum_messages_count DESC LIMIT @startIndex, @totalPerPage;");

						dbClient.AddParameter("startIndex", startIndex);
						dbClient.AddParameter("totalPerPage", TotalPerPage);

						DataTable table = dbClient.GetTable();

						groupList.AddRange(from DataRow rowGroupData in table.Rows
							select uint.Parse(rowGroupData["group_id"].ToString())
							into groupId
							select Yupi.GetGame().GetGroupManager().GetGroup(groupId));

						message.AppendInteger(qtdForums == 0 ? 1 : qtdForums);
						message.AppendInteger(startIndex);

						message.AppendInteger(table.Rows.Count);


						SerializeForumRoot(message, groupList);
					}
					break;

				case 2:
					groupList.AddRange(
						session.GetHabbo()
						.UserGroups.Select(groupUser => Yupi.GetGame().GetGroupManager().GetGroup(groupUser.GroupId))
						.Where(aGroup => aGroup != null && aGroup.Forum.Id != 0));

					message.AppendInteger(groupList.Count == 0 ? 1 : groupList.Count);

					groupList =
						groupList.OrderByDescending(x => x.Forum.ForumMessagesCount).Skip(startIndex).Take(20).ToList();

					message.AppendInteger(startIndex);
					message.AppendInteger(groupList.Count);

					SerializeForumRoot(message, groupList);
					break;

				default:
					message.AppendInteger(1);
					message.AppendInteger(startIndex);
					message.AppendInteger(0);
					break;
				}

				session.Send (message);
			}
		}


		private void SerializeForumRoot(ServerMessage message, List<Group> groupList) {
			foreach (Group group in groupList) {
				message.AppendInteger (group.Id);
				message.AppendString (group.Name);
				message.AppendString (string.Empty);
				message.AppendString (group.Badge);
				message.AppendInteger (0);
				message.AppendInteger ((int)Math.Round (group.Forum.ForumScore));
				message.AppendInteger (group.Forum.ForumMessagesCount);
				message.AppendInteger (0);
				message.AppendInteger (0);
				message.AppendInteger (group.Forum.ForumLastPosterId);
				message.AppendString (group.Forum.ForumLastPosterName);
				message.AppendInteger (group.Forum.ForumLastPostTime);
			}
		}
	}
}

