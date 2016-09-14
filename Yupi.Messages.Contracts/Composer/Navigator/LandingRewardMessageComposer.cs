namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class LandingRewardMessageComposer : AbstractComposer<HotelLandingManager, UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, HotelLandingManager manager, UserInfo user)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}