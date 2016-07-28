using System;
using System.Text.RegularExpressions;



namespace Yupi.Messages.Items
{
	public class UpdateMoodlightMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
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

			if (!IsValidColor (color) || !IsValidIntensity (intensity)) {
				return;
			}

			room.MoodlightData.Enabled = true;

			room.MoodlightData.CurrentPreset = preset;
			room.MoodlightData.UpdatePreset(preset, color, intensity, bgOnly);

			item.ExtraData = room.MoodlightData.GenerateExtraData();
			item.UpdateState();
		}

		private bool IsValidIntensity(int intensity) {
			return 0 <= intensity && intensity <= 255;
		}

		private bool IsValidColor(string hexRGB) {
			return Regex.IsMatch (hexRGB, "^#[0-9A-F]{6}$", RegexOptions.IgnoreCase);
		}
	}
}

