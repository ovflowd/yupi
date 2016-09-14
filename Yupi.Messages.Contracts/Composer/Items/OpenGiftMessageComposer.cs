using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OpenGiftMessageComposer : AbstractComposer<BaseItem, string>
    {
        public override void Compose(ISender session, BaseItem item, string text)
        {
            // Do nothing by default.
        }
    }
}