using System;



namespace Yupi.Messages.Groups
{
	public class GroupUpdateColoursMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();

			int color1 = request.GetInteger();
			int color2 = request.GetInteger();

			Group theGroup = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			if (theGroup?.CreatorId != session.GetHabbo().Id)
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				// TODO Fix Colour to Color
				queryReactor.SetQuery("UPDATE groups_data Set colour1 = @color1, colour2 = @color2 WHERE id = @id");
				queryReactor.AddParameter("color1", color1);
				queryReactor.AddParameter("color2", color2);
				queryReactor.AddParameter("id", theGroup.Id);
				queryReactor.RunQuery ();
			}
			// TODO Refactor assign numbers earlier and implement save method on group!
			theGroup.Colour1 = color1;
			theGroup.Colour2 = color2;

			router.GetComposer<GroupDataMessageComposer> ().Compose (session.GetHabbo().CurrentRoom, theGroup, session.GetHabbo());
		}
	}
}

