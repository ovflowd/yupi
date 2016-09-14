namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class UserUpdateNameInRoomMessageComposer : AbstractComposer<Habbo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, Habbo habbo)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}