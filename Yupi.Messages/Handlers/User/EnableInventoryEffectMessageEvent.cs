using System;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.User
{
	public class EnableInventoryEffectMessageEvent : AbstractHandler
	{
		private AvatarEffectController EffectController;

		public EnableInventoryEffectMessageEvent ()
		{
			EffectController = DependencyFactory.Resolve<AvatarEffectController> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			UserEntity userEntity = session.UserData.RoomEntity;

			if (userEntity == null) {
				return;
			}
			
			int effectId = message.GetInteger();

			if (session.UserData.IsRidingHorse)
				return;
			
			if (effectId == 0) {
				EffectController.StopEffect (session.UserData, session.UserData.Info.EffectComponent.ActiveEffect);
			} else {
				EffectController.ActivateEffect (userEntity, effectId);
			}
		}
	}
}

