namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public abstract class NewNavigatorSizeMessageComposer : AbstractComposer<UserPreferences>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserPreferences preferences)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}