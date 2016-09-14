using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomSettingsDataMessageComposer : AbstractComposer<RoomData>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData room)
        {
            // Do nothing by default.
        }
    }
}