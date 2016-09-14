using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadModerationToolMessageComposer : AbstractComposer
    {
        public virtual void Compose(ISender session, IList<SupportTicket> Tickets, IList<ModerationTemplate> Templates,
            IList<string> UserMessagePresets, IList<string> RoomMessagePresets, UserInfo user)
        {
            // Do nothing by default.
        }
    }
}