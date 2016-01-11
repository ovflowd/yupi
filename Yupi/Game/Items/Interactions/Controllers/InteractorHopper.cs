using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorHopper : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            item.GetRoom().GetRoomItemHandler().HopperCount++;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "INSERT INTO items_hopper (hopper_id, room_id) VALUES (@hopperid, @roomid);");
                commitableQueryReactor.AddParameter("hopperid", item.Id);
                commitableQueryReactor.AddParameter("roomid", item.RoomId);
                commitableQueryReactor.RunQuery();
            }

            if (item.InteractingUser == 0u)
                return;

            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser);

            if (roomUserByHabbo != null)
            {
                roomUserByHabbo.ClearMovement();
                roomUserByHabbo.AllowOverride = false;
                roomUserByHabbo.CanWalk = true;
            }

            item.InteractingUser = 0u;
        }

        public override void OnRemove(GameClient session, RoomItem item)
        {
            item.GetRoom().GetRoomItemHandler().HopperCount--;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"DELETE FROM items_hopper WHERE item_id=@hid OR room_id={item.GetRoom().RoomId} LIMIT 1");
                commitableQueryReactor.AddParameter("hid", item.Id);
                commitableQueryReactor.RunQuery();
            }

            if (item.InteractingUser == 0u)
                return;

            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser);

            roomUserByHabbo?.UnlockWalking();

            item.InteractingUser = 0u;
        }

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (item?.GetRoom() == null || session == null || session.GetHabbo() == null)
                return;

            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            if (!(roomUserByHabbo.Coordinate == item.Coordinate) && !(roomUserByHabbo.Coordinate == item.SquareInFront))
            {
                if (roomUserByHabbo.CanWalk)
                    roomUserByHabbo.MoveTo(item.SquareInFront);
                return;
            }

            if (item.InteractingUser != 0u)
                return;

            roomUserByHabbo.TeleDelay = 2;
            item.InteractingUser = roomUserByHabbo.GetClient().GetHabbo().Id;
        }
    }
}