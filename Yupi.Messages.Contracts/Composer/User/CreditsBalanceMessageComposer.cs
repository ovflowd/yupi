using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public class CreditsBalanceMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int credits)
        {
        }
    }
}