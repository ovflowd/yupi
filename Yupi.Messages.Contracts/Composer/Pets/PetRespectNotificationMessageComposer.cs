using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class PetRespectNotificationMessageComposer : AbstractComposer<PetEntity>
    {
        public override void Compose(ISender session, PetEntity pet)
        {
            // Do nothing by default.
        }
    }
}