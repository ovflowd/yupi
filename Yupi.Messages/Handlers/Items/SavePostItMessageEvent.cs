namespace Yupi.Messages.Items
{
    using System;

    public class SavePostItMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            RoomItem item = room?.GetRoomItemHandler().GetItem(request.GetUInt32());

            if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
                return;

            string text = request.GetString();
            string text2 = request.GetString();

            if (!room.CheckRights(session) && !text2.StartsWith(item.ExtraData))
                return;

            string a;

            if ((a = text) == null || (a != "FFFF33" && a != "FF9CFF" && a != "9CCEFF" && a != "9CFF9C"))
                return;

            item.ExtraData = $"{text} {text2}";
            item.UpdateState(true, true);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}