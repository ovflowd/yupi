using System;



using Yupi.Messages.Rooms;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;

namespace Yupi.Messages.Groups
{
	public class GroupUpdateSettingsMessageEvent : AbstractHandler
	{
		private IRepository<Group> GroupRepository;
		private RoomManager RoomManager;

		public GroupUpdateSettingsMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<IRepository<Group>> ();
			RoomManager = DependencyFactory.Resolve<RoomManager> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger ();
			uint state = request.GetUInt32();
			uint admindeco = request.GetUInt32();

			Group group = GroupRepository.FindBy (groupId);

			if (group?.Creator != session.Info)
				return;

			group.State = state;
			group.AdminOnlyDeco = admindeco;

			GroupRepository.Save (group);

			Room room = RoomManager.GetIfLoaded(group.Room);
			/*
			if (room != null)
			{
				foreach (RoomUser current in room.GetRoomUserManager().GetRoomUsers())
				{
					if (room.Data.Owner != current.UserId && !group.Admins.ContainsKey(current.UserId) &&
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
				*/
			throw new NotImplementedException ();

		}
	}
}

