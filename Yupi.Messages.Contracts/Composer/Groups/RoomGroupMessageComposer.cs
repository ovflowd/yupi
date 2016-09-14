using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomGroupMessageComposer : AbstractComposer<ISet<Group>>
    {
        public override void Compose(ISender room, ISet<Group> groups)
        {
            // Do nothing by default.
        }
    }
}