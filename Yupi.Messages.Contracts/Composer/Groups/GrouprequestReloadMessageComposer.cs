using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GrouprequestReloadMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int groupId)
        {
            // Do nothing by default.
        }
    }
}