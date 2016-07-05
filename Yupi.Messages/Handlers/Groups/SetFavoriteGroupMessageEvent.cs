using System;



namespace Yupi.Messages.Groups
{
	public class SetFavoriteGroupMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint groupId = request.GetUInt32();

			Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (theGroup == null)
				return;

			if (!theGroup.Members.ContainsKey(session.GetHabbo().Id))
				return;

			session.GetHabbo().FavouriteGroup = theGroup.Id;
			router.GetComposer<GroupDataMessageComposer> ().Compose (session, theGroup, session.GetHabbo());

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE users_stats SET favourite_group = @group_id WHERE id = @user_id");
				queryReactor.AddParameter("group_id", theGroup.Id);
				queryReactor.AddParameter("user_id", session.GetHabbo ().Id);
				queryReactor.RunQuery ();
			}

			// TODO Why do we need to send the user id?
			router.GetComposer<FavouriteGroupMessageComposer> ().Compose (session, session.GetHabbo ().Id);


			if (session.GetHabbo().CurrentRoom != null && !session.GetHabbo().CurrentRoom.LoadedGroups.ContainsKey(theGroup.Id))
			{
					session.GetHabbo().CurrentRoom.LoadedGroups.Add(theGroup.Id, theGroup.Badge);
					router.GetComposer<RoomGroupMessageComposer> ().Compose (session.GetHabbo ().CurrentRoom);
			}

			router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (session, theGroup, 0);
		}
	}
}

