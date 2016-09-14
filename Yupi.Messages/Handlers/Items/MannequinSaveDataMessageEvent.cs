using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class MannequinSaveDataMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var pId = request.GetUInt32();
            var text = request.GetString();

            /*
            RoomItem item = Session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(pId);

            if (item == null)
                return;

            if (!item.ExtraData.Contains(Convert.ToChar(5)))
                return;

            if (!session.GetHabbo().CurrentRoom.CheckRights(Session, true))
                return;
            // TODO Rename
            string[] array = item.ExtraData.Split(Convert.ToChar(5));

            array[2] = text;

            item.ExtraData = String.Join(Convert.ToChar(5).ToString(), array);
            item.Serialize(response);
            item.UpdateState(true, true);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
                queryReactor.AddParameter("extraData", item.ExtraData);
                queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }
            */
            throw new NotImplementedException();
        }
    }
}