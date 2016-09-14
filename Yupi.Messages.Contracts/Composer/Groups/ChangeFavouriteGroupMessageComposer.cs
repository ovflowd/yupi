namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ChangeFavouriteGroupMessageComposer : AbstractComposer<Group, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Group group, int virtualId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}