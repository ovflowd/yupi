using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class StopAvatarEffectMessageComposer : Contracts.StopAvatarEffectMessageComposer
    {
        public override void Compose(ISender session, AvatarEffect effect)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(effect.EffectId);
                session.Send(message);
            }
        }
    }
}