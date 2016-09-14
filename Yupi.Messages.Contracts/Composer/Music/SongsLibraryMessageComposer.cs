namespace Yupi.Messages.Contracts
{
    using System.Collections.Specialized;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SongsLibraryMessageComposer : AbstractComposer<SongItem[]>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, SongItem[] songs)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}