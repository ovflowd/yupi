using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LandingPromosMessageComposer : AbstractComposer<List<HotelLandingPromos>>
    {
        public override void Compose(ISender session, List<HotelLandingPromos> promos)
        {
            // Do nothing by default.
        }
    }
}