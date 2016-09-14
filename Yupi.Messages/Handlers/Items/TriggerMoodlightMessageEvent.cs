using System;



namespace Yupi.Messages.Items
{
	public class TriggerMoodlightMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(Session, true) || room.MoodlightData == null)
				return;

			RoomItem item = room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId);

			if (item == null || item.GetBaseItem().InteractionType != Interaction.Dimmer)
				return;

			if (room.MoodlightData.Enabled)
				room.MoodlightData.Disable();
			else
				room.MoodlightData.Enable();

			item.ExtraData = room.MoodlightData.GenerateExtraData();
			item.UpdateState();
			*/
			throw new NotImplementedException ();
		}
	}
}

