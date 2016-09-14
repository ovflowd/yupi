using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadVolumeMessageComposer : AbstractComposer<UserPreferences>
    {
        public override void Compose(ISender session, UserPreferences preferences)
        {
            // Do nothing by default.
        }
    }
}