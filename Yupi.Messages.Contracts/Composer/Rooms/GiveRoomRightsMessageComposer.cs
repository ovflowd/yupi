namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class GiveRoomRightsMessageComposer : AbstractComposer<int, UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, UserInfo habbo)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}