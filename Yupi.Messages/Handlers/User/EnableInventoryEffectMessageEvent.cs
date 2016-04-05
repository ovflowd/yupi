using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class EnableInventoryEffectMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			Room currentRoom = session.GetHabbo().CurrentRoom;
			RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;
			
			int effectId = message.GetInteger();

			if (roomUserByHabbo.RidingHorse)
				return;
			
			if (effectId == 0)
			{
				session.GetHabbo()
					.GetAvatarEffectsInventoryComponent()
					.StopEffect(session.GetHabbo().GetAvatarEffectsInventoryComponent().CurrentEffect);
				return;
			}
			session.GetHabbo().GetAvatarEffectsInventoryComponent().ActivateEffect(effectId);
		}
	}
}

