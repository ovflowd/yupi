using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadPostItMessageComposer : AbstractComposer<PostItItem>
    {
        public override void Compose(ISender session, PostItItem item)
        {
            // Do nothing by default.
        }
    }
}