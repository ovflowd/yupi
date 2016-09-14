using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public class GiftErrorMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string username)
        {
        }
    }
}