namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class RoomSettingsSavedMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}