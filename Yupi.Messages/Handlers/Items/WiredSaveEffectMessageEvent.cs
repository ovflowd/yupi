using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired;

namespace Yupi.Messages.Items
{
	public class WiredSaveEffectMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint itemId = request.GetUInt32();

			RoomItem item =
				Yupi.GetGame()
					.GetRoomManager()
					.GetRoom(session.GetHabbo().CurrentRoomId)
					.GetRoomItemHandler()
					.GetItem(itemId);

			WiredSaver.SaveWired(session, item, request);
		}
	}

	public class WiredSaveTriggerMessageEvent : WiredSaveEffectMessageEvent {

	}

	public class WiredSaveMatchingMessageEvent : WiredSaveEffectMessageEvent {

	}

	public class WiredSaveConditionMessageEvent : WiredSaveEffectMessageEvent {

	}
}

