using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Rooms.User.Path;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     class InteractorDice : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            if (item.ExtraData != "-1")
                return;

            item.ExtraData = "0";
            item.UpdateNeeded = true;
        }

        public override void OnRemove(GameClient session, RoomItem item)
        {
            if (item.ExtraData == "-1")
                item.ExtraData = "0";
        }

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            RoomUser roomUser = null;
            if (session != null)
                roomUser = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (roomUser == null)
                return;

            if (Gamemap.TilesTouching(item.X, item.Y, roomUser.X, roomUser.Y))
            {
                if (item.ExtraData == "-1")
                    return;

                if (request == -1)
                {
                    item.ExtraData = "0";
                    item.UpdateState();
                    return;
                }

                item.ExtraData = "-1";
                item.UpdateState(false, true);
                item.ReqUpdate(4, true);
                return;
            }

            roomUser.MoveTo(item.SquareInFront);
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            item.ExtraData = "-1";
            item.UpdateState(false, true);
            item.ReqUpdate(4, true);
        }
    }
}