using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
    internal class InteractorHopper : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            item.GetRoom().GetRoomItemHandler().HopperCount++;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "INSERT INTO items_hopper (hopper_id, room_id) VALUES (@hopperid, @roomid);");
                queryReactor.AddParameter("hopperid", item.Id);
                queryReactor.AddParameter("roomid", item.RoomId);
                queryReactor.RunQuery();
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

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"DELETE FROM items_hopper WHERE item_id=@hid OR room_id={item.GetRoom().RoomId} LIMIT 1");
                queryReactor.AddParameter("hid", item.Id);
                queryReactor.RunQuery();
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