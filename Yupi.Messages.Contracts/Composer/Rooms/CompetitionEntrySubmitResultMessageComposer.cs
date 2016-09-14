using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CompetitionEntrySubmitResultMessageComposer : AbstractComposer<RoomCompetition, int, RoomData>
    {
        public override void Compose(ISender session, RoomCompetition competition, int status, RoomData room = null)
        {
            // Do nothing by default.
        }
    }
}