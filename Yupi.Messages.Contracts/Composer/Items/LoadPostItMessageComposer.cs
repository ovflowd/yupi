namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class LoadPostItMessageComposer : AbstractComposer<PostItItem>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, PostItItem item)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}