using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model;

namespace Yupi.Messages.Contracts
{
    public abstract class LoadModerationToolMessageComposer : AbstractComposer
    {
        public virtual void Compose(Yupi.Protocol.ISender session, IList<SupportTicket> Tickets,
            IList<ModerationTemplate> Templates, IList<string> UserMessagePresets, IList<string> RoomMessagePresets,
            UserInfo user)
        {
            // Do nothing by default.
        }
    }
}