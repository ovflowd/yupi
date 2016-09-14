using System;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public class StopAvatarEffectMessageComposer : AbstractComposer<AvatarEffect>
    {
        public override void Compose(Yupi.Protocol.ISender session, AvatarEffect effect)
        {
        }
    }
}