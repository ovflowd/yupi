using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class CompetitionEntrySubmitResultMessageComposer : AbstractComposer<RoomCompetition, int, RoomData>
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomCompetition competition, int status,
            RoomData room = null)
        {
            // Do nothing by default.
        }
    }
}