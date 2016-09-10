﻿using System;

using Yupi.Protocol.Buffers;

using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
	public class GroupMembersMessageComposer : Yupi.Messages.Contracts.GroupMembersMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, UserInfo user, Group group)
		{
			Compose (session, group, 0u, user);
		}

		// TODO Refactor?
		public override void Compose ( Yupi.Protocol.ISender session, Group group, uint reqType, UserInfo user, string searchVal = "", int page = 0) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(group.Id);
				message.AppendString(group.Name);
				message.AppendInteger(group.Room.Id);
				message.AppendString(group.Badge);

				List<UserInfo> groupList = GetGroupUsersByString(group, searchVal, reqType);
				/*
				if(groupList != null)
				{
					List<List<UserInfo>> list = Split(groupList);

					if(list != null)
					{
						if (reqType == 0)
						{
							message.AppendInteger(list.Count);

							if (group.Members.Count > 0 && list.Count > 0 && list[page] != null)
							{
								message.AppendInteger(list[page].Count);

								using (List<UserInfo>.Enumerator enumerator = list[page].GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										UserInfo current = enumerator.Current;

										AddGroupMemberIntoResponse(message, current);
									}
								}
							}
							else
								message.AppendInteger(0);
						}
						else if (reqType == 1)
						{
							message.AppendInteger(group.Admins.Count);

							List<UserInfo> paging = page <= list.Count ? list[page] : null;

							if ((group.Admins.Count > 0) && (list.Count > 0) && paging != null)
							{
								message.AppendInteger(list[page].Count);

								using (List<UserInfo>.Enumerator enumerator = list[page].GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										UserInfo current = enumerator.Current;

										AddGroupMemberIntoResponse(message, current);
									}
								}
							}
							else
								message.AppendInteger(0);
						}
						else if (reqType == 2)
						{
							message.AppendInteger(group.Requests.Count);

							if (group.Requests.Count > 0 && list.Count > 0 && list[page] != null)
							{
								message.AppendInteger(list[page].Count);

								using (List<UserInfo>.Enumerator enumerator = list[page].GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										UserInfo current = enumerator.Current;

										message.AppendInteger(3);

										if (current != null)
										{
											message.AppendInteger(current.Id);
											message.AppendString(current.Name);
											message.AppendString(current.Look);
										}

										message.AppendString(string.Empty);
									}
								}
							}
							else
								message.AppendInteger(0);
						}
					}
					else
						message.AppendInteger(0);
				}
				else
					message.AppendInteger(0);

				message.AppendBool(user == group.Creator);
				message.AppendInteger(14);
				message.AppendInteger(page);
				message.AppendInteger(reqType);
				message.AppendString(searchVal);
				session.Send (message);
				*/
				throw new NotImplementedException ();
			}
		}

		private void AddGroupMemberIntoResponse(ServerMessage response, UserInfo member)
		{
			response.AppendInteger(member.Rank == 2 ? 0 : member.Rank == 1 ? 1 : 2);
			response.AppendInteger(member.Id);
			response.AppendString(member.Name);
			response.AppendString(member.Look);
			throw new NotImplementedException ();
			//response.AppendString(Yupi.GetGroupDateJoinString(member.DateJoin));
		}

		// TODO Useless copy to list?
		private List<UserInfo> GetGroupUsersByString(Group theGroup, string searchVal, uint req)
		{
			/*
			List<UserInfo> list = new List<UserInfo>();

			switch (req)
			{
			case 0:

				list = theGroup.Members.Values.ToList();

				break;

			case 1:
				list = theGroup.Admins.Values.ToList();
				break;

			case 2:
				list = GetGroupRequestsByString(theGroup, searchVal);
				break;
			}

			if (!string.IsNullOrWhiteSpace(searchVal))
				list = list.Where(member => member.Name.ToLower().Contains(searchVal.ToLower())).ToList();

			return list;*/
			throw new NotImplementedException ();
		}

		private  List<UserInfo> GetGroupRequestsByString(Group theGroup, string searchVal)
		{
			if (string.IsNullOrWhiteSpace (searchVal)) {
				return theGroup.Requests.ToList ();
			} else {
				return theGroup.Requests.Where (request => request.Name.ToLower ().Contains (searchVal.ToLower ()))
					.ToList ();
			}
		}
	}
}

