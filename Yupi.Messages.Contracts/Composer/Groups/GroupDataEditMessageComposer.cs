using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GroupDataEditMessageComposer : AbstractComposer<Group>
    {
        public override void Compose(ISender session, Group group)
        {
            // Do nothing by default.
        }
    }
}