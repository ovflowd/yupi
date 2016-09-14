namespace Yupi.Messages.Catalog
{
    using System;
    using System.Collections.Generic;

    public class GetGroupFurnitureMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            throw new NotImplementedException();
            //HashSet<GroupMember> userGroups = Yupi.GetGame().GetGroupManager().GetUserGroups(session.GetHabbo().Id);
        }

        #endregion Methods
    }
}