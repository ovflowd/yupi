namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SongsMessageComposer : AbstractComposer<List<SongData>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, List<SongData> songs)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}