namespace Yupi.Messages.Contracts
{
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class LoadRoomRightsListMessageComposer : AbstractComposer<RoomData>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}