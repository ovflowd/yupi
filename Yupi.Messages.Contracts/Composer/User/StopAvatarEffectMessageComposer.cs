namespace Yupi.Messages.Contracts
{
    using System;

    using Yupi.Model.Domain;

    public class StopAvatarEffectMessageComposer : AbstractComposer<AvatarEffect>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, AvatarEffect effect)
        {
        }

        #endregion Methods
    }
}