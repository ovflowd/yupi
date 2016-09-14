namespace Yupi.Messages.User
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class EnableInventoryEffectMessageEvent : AbstractHandler
    {
        #region Fields

        private AvatarEffectController EffectController;

        #endregion Fields

        #region Constructors

        public EnableInventoryEffectMessageEvent()
        {
            EffectController = DependencyFactory.Resolve<AvatarEffectController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            UserEntity userEntity = session.RoomEntity;

            if (userEntity == null)
            {
                return;
            }

            int effectId = message.GetInteger();

            if (session.IsRidingHorse)
                return;

            if (effectId == 0)
            {
                EffectController.StopEffect(session, session.Info.EffectComponent.ActiveEffect);
            }
            else
            {
                EffectController.ActivateEffect(userEntity, effectId);
            }
        }

        #endregion Methods
    }
}