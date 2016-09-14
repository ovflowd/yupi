namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class OnGuideSessionMsgMessageComposer : AbstractComposer<string, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string content, int userId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}