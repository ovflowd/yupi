namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class LoadModerationToolMessageComposer : AbstractComposer
    {
        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, IList<SupportTicket> Tickets,
            IList<ModerationTemplate> Templates, IList<string> UserMessagePresets, IList<string> RoomMessagePresets,
            UserInfo user)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}