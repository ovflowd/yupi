namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class AddEffectToInventoryMessageComposer : Yupi.Messages.Contracts.AddEffectToInventoryMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Yupi.Model.Domain.AvatarEffect effect)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(effect.EffectId);
                message.AppendInteger(effect.Type);
                message.AppendInteger(effect.TotalDuration);
                message.AppendBool(effect.TotalDuration == -1); // TODO What does this mean actually?
                session.Send(message);
            }
        }

        #endregion Methods
    }
}