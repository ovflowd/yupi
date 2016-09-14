using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class OpenGiftMessageComposer : AbstractComposer<BaseItem, string>
    {
        public override void Compose(Yupi.Protocol.ISender session, BaseItem item, string text)
        {
            // Do nothing by default.
        }
    }
}