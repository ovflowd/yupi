namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class UpdateIgnoreStatusMessageComposer : AbstractComposer<UpdateIgnoreStatusMessageComposer.State, string>
    {
        #region Enumerations

        public enum State
        {
            IGNORE = 1,
            LISTEN = 3 // TODO Any other valid values?
        }

        #endregion Enumerations

        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, State state, string username)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}