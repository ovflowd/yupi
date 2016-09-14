using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class EffectsInventoryMessageComposer : Contracts.EffectsInventoryMessageComposer
    {
        public override void Compose(ISender session, IList<AvatarEffect> effects)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(effects.Count);

                foreach (var current in effects)
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
    }
}