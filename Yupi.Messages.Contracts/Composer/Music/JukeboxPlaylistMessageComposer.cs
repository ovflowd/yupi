namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public abstract class JukeboxPlaylistMessageComposer : AbstractComposer<SongMachineComponent>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, SongMachineComponent songMachine)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}