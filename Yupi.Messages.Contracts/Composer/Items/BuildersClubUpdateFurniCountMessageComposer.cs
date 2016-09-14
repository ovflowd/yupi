using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class BuildersClubUpdateFurniCountMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int itemsUsed)
        {
            // Do nothing by default.
        }
    }
}