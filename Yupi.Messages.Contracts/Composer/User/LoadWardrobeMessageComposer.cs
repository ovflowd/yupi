using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadWardrobeMessageComposer : AbstractComposer<IList<WardrobeItem>>
    {
        public override void Compose(Yupi.Protocol.ISender session, IList<WardrobeItem> wardrobe)
        {
            // Do nothing by default.
        }
    }
}