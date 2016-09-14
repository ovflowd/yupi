using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class JukeboxPlaylistMessageComposer : AbstractComposer<SongMachineComponent>
    {
        public override void Compose(ISender session, SongMachineComponent songMachine)
        {
            // Do nothing by default.
        }
    }
}