namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class ApplyEffectMessageComposer : Yupi.Messages.Contracts.ApplyEffectMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Yupi.Model.Domain.RoomEntity entity,
            Yupi.Model.Domain.AvatarEffect effect)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entity.Id);
                message.AppendInteger(effect.EffectId);
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}