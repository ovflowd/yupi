using System;




namespace Yupi.Messages.User
{
	public class EnableInventoryEffectMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms currentRoom = session.GetHabbo().CurrentRoom;
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

