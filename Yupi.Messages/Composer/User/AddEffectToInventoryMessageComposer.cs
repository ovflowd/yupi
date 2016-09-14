using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class AddEffectToInventoryMessageComposer : Contracts.AddEffectToInventoryMessageComposer
    {
        public override void Compose(ISender session, AvatarEffect effect)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(effect.EffectId);
                message.AppendInteger(effect.Type);
                message.AppendInteger(effect.TotalDuration);
                message.AppendBool(effect.TotalDuration == -1); // TODO What does this mean actually?
                session.Send(message);
            }
        }
    }
}