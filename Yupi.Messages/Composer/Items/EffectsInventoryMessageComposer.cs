namespace Yupi.Messages.Items
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class EffectsInventoryMessageComposer : Yupi.Messages.Contracts.EffectsInventoryMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<AvatarEffect> effects)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(effects.Count);

                foreach (AvatarEffect current in effects)
                {
                    message.AppendInteger(current.EffectId);
                    message.AppendInteger(current.Type);
                    message.AppendInteger(current.TotalDuration);
                    message.AppendInteger(0);
                    message.AppendInteger(current.TimeLeft());
                    message.AppendBool(current.TotalDuration == -1);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}