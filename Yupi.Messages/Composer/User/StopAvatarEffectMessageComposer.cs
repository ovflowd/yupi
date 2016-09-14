using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class StopAvatarEffectMessageComposer : Contracts.StopAvatarEffectMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, Yupi.Model.Domain.AvatarEffect effect)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(effect.EffectId);
                session.Send(message);
            }
        }
    }
}