using System;



using Yupi.Messages.Rooms;

namespace Yupi.Messages.Groups
{
	public class GroupUpdateSettingsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();
			uint state = request.GetUInt32();
			uint admindeco = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (group?.CreatorId != session.GetHabbo().Id)
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE groups_data SET state = @state, admindeco = @admindeco WHERE id = @id");
				queryReactor.AddParameter("state", state);
				queryReactor.AddParameter("admindeco", admindeco);
				queryReactor.AddParameter("id", group.Id);
				queryReactor.RunQuery ();
			}

			group.State = state;
			group.AdminOnlyDeco = admindeco;

			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(group.RoomId);

			if (room != null)
			{
				foreach (RoomUser current in room.GetRoomUserManager().GetRoomUsers())
				{
					if (room.RoomData.OwnerId != current.UserId && !group.Admins.ContainsKey(current.UserId) &&
						group.Members.ContainsKey(current.UserId))
					{
						// TODO USE F*KING ENUMS!
						if (admindeco == 1u)
						{
							current.RemoveStatus("flatctrl 1");
							router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (current.GetClient(), 0);
						}
						else
						{
							if (admindeco == 0u && !current.Statusses.ContainsKey("flatctrl 1"))
							{
								current.AddStatus("flatctrl 1", string.Empty);
								router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (current.GetClient(), 1);
							}
						}

						current.UpdateNeeded = true;
					}
				}
			}
				
			router.GetComposer<GroupDataMessageComposer> ().Compose (session.GetHabbo().CurrentRoom, group, session.GetHabbo());
		}
	}
}

