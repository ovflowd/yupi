using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RetrieveSongIDMessageComposer : AbstractComposer<string, int>
    {
        public override void Compose(ISender session, string name, int songId)
        {
            // Do nothing by default.
        }
    }
}