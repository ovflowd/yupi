namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Protocol.Buffers;

    public abstract class RoomLoadFilterMessageComposer : AbstractComposer<List<string>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, List<string> wordlist)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}