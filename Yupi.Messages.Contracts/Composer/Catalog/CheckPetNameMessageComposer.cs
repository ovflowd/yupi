using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CheckPetNameMessageComposer : AbstractComposer<int, string>
    {
        public override void Compose(ISender session, int status, string name)
        {
            // Do nothing by default.
        }
    }
}