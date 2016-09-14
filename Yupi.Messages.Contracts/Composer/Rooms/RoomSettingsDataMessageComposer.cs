namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomSettingsDataMessageComposer : AbstractComposer<RoomData>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}