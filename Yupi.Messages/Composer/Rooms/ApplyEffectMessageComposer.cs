using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class ApplyEffectMessageComposer : Contracts.ApplyEffectMessageComposer
    {
        public override void Compose(ISender session, RoomEntity entity, AvatarEffect effect)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entity.Id);
                message.AppendInteger(effect.EffectId);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}