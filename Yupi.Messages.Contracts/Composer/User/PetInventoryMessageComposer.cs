using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public class PetInventoryMessageComposer : AbstractComposer<IList<PetItem>>
    {
        public override void Compose(ISender session, IList<PetItem> pets)
        {
        }
    }
}