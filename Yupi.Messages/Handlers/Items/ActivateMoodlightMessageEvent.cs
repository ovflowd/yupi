using System;



namespace Yupi.Messages.Items
{
	public class ActivateMoodlightMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			if (room.MoodlightData == null)
			{
				foreach (
					RoomItem current in
					room.GetRoomItemHandler()
					.WallItems.Values.Where(
						current => current.GetBaseItem().InteractionType == Interaction.Dimmer))
					room.MoodlightData = new MoodlightData(current.Id);
			}

			if (room.MoodlightData == null)
				return;

			router.GetComposer<DimmerDataMessageComposer> ().Compose (session, room.MoodlightData);
		}
	}
}

