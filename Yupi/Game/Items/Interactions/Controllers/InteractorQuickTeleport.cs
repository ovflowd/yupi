using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorQuickTeleport : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            item.ExtraData = "0";

            if (item.InteractingUser != 0u)
            {
                RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser);

                if (roomUserByHabbo != null)
                {
                    roomUserByHabbo.ClearMovement();
                    roomUserByHabbo.AllowOverride = false;
                    roomUserByHabbo.CanWalk = true;
                }

                item.InteractingUser = 0u;
            }

            if (item.InteractingUser2 == 0u)
                return;

            RoomUser roomUserByHabbo2 = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser2);

            if (roomUserByHabbo2 != null)
            {
                roomUserByHabbo2.ClearMovement();
                roomUserByHabbo2.AllowOverride = false;
                roomUserByHabbo2.CanWalk = true;
            }

            item.InteractingUser2 = 0u;
        }

        public override void OnRemove(GameClient session, RoomItem item)
        {
            item.ExtraData = "0";

            if (item.InteractingUser != 0u)
            {
                RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser);

                roomUserByHabbo?.UnlockWalking();

                item.InteractingUser = 0u;
            }

            if (item.InteractingUser2 == 0u)
                return;

            RoomUser roomUserByHabbo2 = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(item.InteractingUser2);

            roomUserByHabbo2?.UnlockWalking();

            item.InteractingUser2 = 0u;
        }

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (item == null || item.GetRoom() == null || session == null || session.GetHabbo() == null)
                return;

            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            if (!(roomUserByHabbo.Coordinate == item.Coordinate) && !(roomUserByHabbo.Coordinate == item.SquareInFront))
            {
                roomUserByHabbo.MoveTo(item.SquareInFront);
                return;
            }

            if (item.InteractingUser != 0)
                return;

            item.InteractingUser = roomUserByHabbo.GetClient().GetHabbo().Id;
        }

        public override void OnUserWalk(GameClient session, RoomItem item, RoomUser user)
        {
            if (item == null || item.GetRoom() == null || session == null || session.GetHabbo() == null)
                return;

            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            if (!(roomUserByHabbo.Coordinate == item.Coordinate) && !(roomUserByHabbo.Coordinate == item.SquareInFront))
            {
                roomUserByHabbo.MoveTo(item.SquareInFront);
                return;
            }

            if (item.InteractingUser != 0)
                return;

            item.InteractingUser = roomUserByHabbo.GetClient().GetHabbo().Id;
        }
    }
}