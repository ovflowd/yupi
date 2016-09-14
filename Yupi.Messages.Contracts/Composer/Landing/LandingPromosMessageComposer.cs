using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class LandingPromosMessageComposer : AbstractComposer<List<HotelLandingPromos>>
    {
        public override void Compose(Yupi.Protocol.ISender session, List<HotelLandingPromos> promos)
        {
            // Do nothing by default.
        }
    }
}