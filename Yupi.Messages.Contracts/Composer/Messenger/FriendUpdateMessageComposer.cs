namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class FriendUpdateMessageComposer : AbstractComposer<Relationship>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Relationship relationship)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}