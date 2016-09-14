using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadWardrobeMessageComposer : AbstractComposer<IList<WardrobeItem>>
    {
        public override void Compose(ISender session, IList<WardrobeItem> wardrobe)
        {
            // Do nothing by default.
        }
    }
}