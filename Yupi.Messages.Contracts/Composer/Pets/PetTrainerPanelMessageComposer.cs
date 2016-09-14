using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class PetTrainerPanelMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int petId)
        {
            // Do nothing by default.
        }
    }
}