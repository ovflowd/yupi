namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Protocol.Buffers;

    public abstract class NameChangedUpdatesMessageComposer : AbstractComposer<NameChangedUpdatesMessageComposer.Status, string, IList<string>>
    {
        #region Enumerations

        public enum Status
        {
            OK = 0,
            // TODO What is 1 ?
            TOO_SHORT = 2,
            TOO_LONG = 3,
            INVALID_CHARS = 4,
            IS_TAKEN = 5
        }

        #endregion Enumerations

        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Status status, string newName,
            IList<string> alternatives = null)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}