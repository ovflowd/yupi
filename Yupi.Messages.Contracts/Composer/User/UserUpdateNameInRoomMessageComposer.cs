using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UserUpdateNameInRoomMessageComposer : AbstractComposer<Habbo>
    {
        public override void Compose(ISender room, Habbo habbo)
        {
            // Do nothing by default.
        }
    }
}