namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class LandingPromosMessageComposer : AbstractComposer<List<HotelLandingPromos>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, List<HotelLandingPromos> promos)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}