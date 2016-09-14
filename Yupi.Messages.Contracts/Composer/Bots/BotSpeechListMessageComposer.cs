namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class BotSpeechListMessageComposer : AbstractComposer<int, BotInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int num, BotInfo bot)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}