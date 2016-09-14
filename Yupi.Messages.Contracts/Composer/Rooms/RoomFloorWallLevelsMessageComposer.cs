namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomFloorWallLevelsMessageComposer : AbstractComposer<RoomData>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData data)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}