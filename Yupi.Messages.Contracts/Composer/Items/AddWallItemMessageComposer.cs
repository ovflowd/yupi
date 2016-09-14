namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class AddWallItemMessageComposer : AbstractComposer<WallItem, UserInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, WallItem item, UserInfo user)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}