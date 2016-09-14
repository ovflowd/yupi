namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;

    public class GetGroupPurchaseBoxMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            List<RoomData> rooms = session.Info.UsersRooms.Where(x => x.Group == null).ToList();

            router.GetComposer<GroupPurchasePageMessageComposer>().Compose(session, rooms);
        }

        #endregion Methods
    }
}