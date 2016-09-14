namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomDataMessageComposer : AbstractComposer<RoomData, UserInfo, bool, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room, UserInfo user, bool show,
            bool isNotReload)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}