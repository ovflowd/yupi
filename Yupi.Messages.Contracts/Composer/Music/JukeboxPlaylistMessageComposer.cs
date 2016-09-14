using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Contracts
{
    public abstract class JukeboxPlaylistMessageComposer : AbstractComposer<SongMachineComponent>
    {
        public override void Compose(Yupi.Protocol.ISender session, SongMachineComponent songMachine)
        {
            // Do nothing by default.
        }
    }
}