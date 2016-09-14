using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateUsernameMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string newName)
        {
            // Do nothing by default.
        }
    }
}