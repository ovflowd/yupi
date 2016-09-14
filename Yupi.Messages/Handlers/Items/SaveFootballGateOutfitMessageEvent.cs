using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class SaveFootballGateOutfitMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var pId = request.GetUInt32();
            var gender = request.GetString();
            var look = request.GetString();

            /*

            Yupi.Messages.Rooms room = session.GetHabbo().CurrentRoom;

            RoomItem item = room?.GetRoomItemHandler().GetItem(pId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.FootballGate)
                return;

            string[] figures = item.ExtraData.Split(',');
            string[] newFigures = new string[2];

            switch (gender.ToUpper())
            {
            case "M":
                {
                    newFigures[0] = look;

                    newFigures[1] = figures.Length > 1 ? figures[1] : "hd-99999-99999.ch-630-62.lg-695-62";

                    item.ExtraData = string.Join(",", newFigures);
                }
                break;

            case "F":
                {
                    newFigures[0] = !string.IsNullOrWhiteSpace(figures[0]) ? figures[0] : "hd-99999-99999.lg-270-62";

                    newFigures[1] = look;

                    item.ExtraData = string.Join(",", newFigures);
                }
                break;
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = @id");
                queryReactor.AddParameter("extraData", item.ExtraData);
                queryReactor.AddParameter("id", item.Id);
                queryReactor.RunQuery();
            }

            router.GetComposer<UpdateRoomItemMessageComposer> ().Compose (session, item);
            */
            throw new NotImplementedException();
        }
    }
}