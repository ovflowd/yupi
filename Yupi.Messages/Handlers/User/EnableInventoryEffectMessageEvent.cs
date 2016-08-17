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

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			UserEntity userEntity = session.RoomEntity;

			if (userEntity == null) {
				return;
			}
			
			int effectId = message.GetInteger();

			if (session.IsRidingHorse)
				return;
			
			if (effectId == 0) {
				EffectController.StopEffect (session, session.Info.EffectComponent.ActiveEffect);
			} else {
				EffectController.ActivateEffect (userEntity, effectId);
			}
		}
	}
}

