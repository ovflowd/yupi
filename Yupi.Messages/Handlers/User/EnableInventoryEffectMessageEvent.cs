using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class EnableInventoryEffectMessageEvent : AbstractHandler
    {
        private readonly AvatarEffectController EffectController;

        public EnableInventoryEffectMessageEvent()
        {
            EffectController = DependencyFactory.Resolve<AvatarEffectController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var userEntity = session.RoomEntity;

            if (userEntity == null) return;

            var effectId = message.GetInteger();

            if (session.IsRidingHorse)
                return;

            if (effectId == 0) EffectController.StopEffect(session, session.Info.EffectComponent.ActiveEffect);
            else EffectController.ActivateEffect(userEntity, effectId);
        }
    }
}