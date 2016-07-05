using System;



namespace Yupi.Messages.Items
{
	public class UpdateMoodlightMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true) || room.MoodlightData == null)
				return;

			RoomItem item = room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId);

			if (item == null || item.GetBaseItem().InteractionType != Interaction.Dimmer)
				return;

			int preset = request.GetInteger();

			// TODO Meaning?
			int num2 = request.GetInteger();

			string color = request.GetString();
			int intensity = request.GetInteger();
			bool bgOnly = num2 >= 2;

			room.MoodlightData.Enabled = true;

			room.MoodlightData.CurrentPreset = preset;
			room.MoodlightData.UpdatePreset(preset, color, intensity, bgOnly);

			item.ExtraData = room.MoodlightData.GenerateExtraData();
			item.UpdateState();
		}
	}
}

