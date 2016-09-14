using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RecyclerRewardsMessageComposer : AbstractComposer<EcotronLevel[]>
    {
        public override void Compose(ISender session, EcotronLevel[] levels)
        {
        }
    }
}