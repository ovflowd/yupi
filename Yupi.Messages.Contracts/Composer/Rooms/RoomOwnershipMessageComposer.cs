namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomOwnershipMessageComposer : AbstractComposer<RoomData, UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room, UserInfo user)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}