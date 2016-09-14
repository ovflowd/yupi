using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class ApplyEffectMessageComposer : AbstractComposer<RoomEntity, AvatarEffect>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomEntity entity, AvatarEffect effect)
        {
            // Do nothing by default.
        }
    }
}