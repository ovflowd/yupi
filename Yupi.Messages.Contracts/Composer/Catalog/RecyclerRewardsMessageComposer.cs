using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class RecyclerRewardsMessageComposer : AbstractComposer<EcotronLevel[]>
    {
        public override void Compose(Yupi.Protocol.ISender session, EcotronLevel[] levels)
        {
        }
    }
}