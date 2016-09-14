using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public class StopAvatarEffectMessageComposer : AbstractComposer<AvatarEffect>
    {
        public override void Compose(ISender session, AvatarEffect effect)
        {
        }
    }
}