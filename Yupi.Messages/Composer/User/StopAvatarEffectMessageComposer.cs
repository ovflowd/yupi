namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class StopAvatarEffectMessageComposer : Contracts.StopAvatarEffectMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Yupi.Model.Domain.AvatarEffect effect)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(effect.EffectId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}