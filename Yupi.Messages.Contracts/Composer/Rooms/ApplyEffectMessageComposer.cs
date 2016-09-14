using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ApplyEffectMessageComposer : AbstractComposer<RoomEntity, AvatarEffect>
    {
        public override void Compose(ISender session, RoomEntity entity, AvatarEffect effect)
        {
            // Do nothing by default.
        }
    }
}