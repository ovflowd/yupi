namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class ConsoleInvitationMessageComposer : AbstractComposer<int, string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int habboId, string content)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}